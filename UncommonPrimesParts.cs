// Shamelessly stolen from many sources (Complicated Elements, RM helper functions, etc)
using MonoMod.Utils;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading;
using UncommonPrimes;
using AtomTypes = class_175;
using BondSite = class_222;
using BondType = enum_126;
using PartType = class_139;
using PartTypes = class_191;
using Permissions = enum_149;
using Texture = class_256;

namespace UncommonPrimes;

internal static class UncommonPrimesParts
{
    #region drawingHelpers
    private static Vector2 hexGraphicalOffset(HexIndex hex) => UncommonPrimes.hexGraphicalOffset(hex);
    private static Vector2 textureDimensions(Texture tex) => tex.field_2056.ToVector2();
    private static Vector2 textureCenter(Texture tex) => (textureDimensions(tex) / 2).Rounded();
    private static void drawPartGraphic(class_195 renderer, Texture tex, Vector2 graphicPivot, float graphicAngle, Vector2 graphicTranslation, Vector2 screenTranslation)
    {
        drawPartGraphicScaled(renderer, tex, graphicPivot, graphicAngle, graphicTranslation, screenTranslation, new Vector2(1f, 1f));
    }

    private static void drawPartGraphicScaled(class_195 renderer, Texture tex, Vector2 graphicPivot, float graphicAngle, Vector2 graphicTranslation, Vector2 screenTranslation, Vector2 scaling)
    {
        //for graphicPivot and graphicTranslation, rightwards is the positive-x direction and upwards is the positive-y direction
        //graphicPivot is an absolute position, with (0,0) denoting the bottom-left corner of the texture
        //graphicTranslation is a translation, so (5,-3) means "translate 5 pixels right and 3 pixels down"
        //graphicAngle is measured in radians, and counterclockwise is the positive-angle direction
        //screenTranslation is the final translation applied, so it is not affected by rotations
        Matrix4 matrixScreenPosition = Matrix4.method_1070(renderer.field_1797.ToVector3(0f));
        Matrix4 matrixTranslateOnScreen = Matrix4.method_1070(screenTranslation.ToVector3(0f));
        Matrix4 matrixRotatePart = Matrix4.method_1073(renderer.field_1798);
        Matrix4 matrixTranslateGraphic = Matrix4.method_1070(graphicTranslation.ToVector3(0f));
        Matrix4 matrixRotateGraphic = Matrix4.method_1073(graphicAngle);
        Matrix4 matrixPivotOffset = Matrix4.method_1070(-graphicPivot.ToVector3(0f));
        Matrix4 matrixScaling = Matrix4.method_1074(scaling.ToVector3(0f));
        Matrix4 matrixTextureSize = Matrix4.method_1074(tex.field_2056.ToVector3(0f));

        Matrix4 matrix4 = matrixScreenPosition * matrixTranslateOnScreen * matrixRotatePart * matrixTranslateGraphic * matrixRotateGraphic * matrixPivotOffset * matrixScaling * matrixTextureSize;
        class_135.method_262(tex, Color.White, matrix4);
    }

    private static void drawPartGraphicSpecular(class_195 renderer, Texture tex, Vector2 graphicPivot, float graphicAngle, Vector2 graphicTranslation, Vector2 screenTranslation)
    {
        float specularAngle = (renderer.field_1799 - (renderer.field_1797 + graphicTranslation.Rotated(renderer.field_1798))).Angle() - 1.570796f - renderer.field_1798;
        drawPartGraphic(renderer, tex, graphicPivot, graphicAngle + specularAngle, graphicTranslation, screenTranslation);
    }

    private static void drawPartGloss(class_195 renderer, Texture gloss, Texture glossMask, Vector2 offset)
    {
        drawPartGloss(renderer, gloss, glossMask, offset, new HexIndex(0, 0), 0f);
    }
    private static void drawPartGloss(class_195 renderer, Texture gloss, Texture glossMask, Vector2 offset, HexIndex hexOffset, float angle)
    {
        class_135.method_257().field_1692 = class_238.field_1995.field_1757; // MaskedGlossPS shader
        class_135.method_257().field_1693[1] = gloss;
        var hex = new HexIndex(0, 0);
        Vector2 method2001 = 0.0001f * (renderer.field_1797 + hexGraphicalOffset(hex).Rotated(renderer.field_1798) - 0.5f * class_115.field_1433);
        class_135.method_257().field_1695 = method2001;
        drawPartGraphic(renderer, glossMask, offset, angle, hexGraphicalOffset(hexOffset), Vector2.Zero);
        class_135.method_257().field_1692 = class_135.method_257().field_1696; // previous shader
        class_135.method_257().field_1693[1] = class_238.field_1989.field_71;
        class_135.method_257().field_1695 = Vector2.Zero;
    }
    private static void drawAtomIO(class_195 renderer, AtomType atomType, HexIndex hex, float num)
    {
        Molecule molecule = Molecule.method_1121(atomType);
        Vector2 method1999 = renderer.field_1797 + hexGraphicalOffset(hex).Rotated(renderer.field_1798);
        Editor.method_925(molecule, method1999, new HexIndex(0, 0), 0f, 1f, num, 1f, false, null);
    }
    #endregion




    public static PartType Similarity, Osmosis, Dissolution;

    public static Texture bowl = class_238.field_1989.field_90.field_170;
    public static Texture metalBowl = class_238.field_1989.field_90.field_255.field_292;
    public static Texture[] glyphFlashAnimation = Brimstone.API.GetAnimation("textures/parts/UncommonPrimes/glyph_flash.array", "flash", 10);
    public static Texture calcSymbol = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/similarity/symbols");

    //Similarity
    public static Texture similarityBase = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/similarity/base");
    public static Texture similarityTop = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/similarity/top");
    public static Texture[] similarityFlashAnimation = Brimstone.API.GetAnimation("textures/parts/UncommonPrimes/similarity_glyph_flash.array", "projection_glyph", 10);

    public static Texture similarityIcon = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/similarity/base");
    public static Texture similarityHover = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/similarity/base");

    public static readonly HexIndex similarityInput1 = new(0, 0);
    public static readonly HexIndex similarityInput2 = new(1, 0);
    public static readonly HexIndex similarityOutput = new(0, 1);

    //Osmosis
    public static Texture osmosisBase = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/base");
    public static Texture osmosisShadow = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/shadow");
    public static Texture osmosisBottom = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/bottom");
    public static Texture osmosisTop = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/top");
    public static Texture osmosisGlossmask = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/gloss_mask");
    public static Texture osmosisGloss = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/gloss");
    public static Texture metalSymbolUp = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/metal_symbol_up");
    public static Texture metalSymbolDown = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/osmosis/metal_symbol_down");

    public static Texture osmosisGlow = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/select/osmosis_glow");
    public static Texture osmosisStroke = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/select/osmosis_stroke");

    public static Texture osmosisIcon = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/icons/osmosis");
    public static Texture osmosisHover = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/icons/osmosis_hover");

    public static readonly HexIndex osmosisInputHigh = new(0, 1);
    public static readonly HexIndex osmosisInputLow = new(1, -1);
    public static readonly HexIndex osmosisOutput1 = new(0, 0);
    public static readonly HexIndex osmosisOutput2 = new(1, 0);

    //Dissolution
    public static Texture dissolutionBase = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/base");
    public static Texture dissolutionShadow = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/shadow");
    public static Texture dissolutionBottom = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/bottom");
    public static Texture dissolutionTop = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/top");
    public static Texture dissolutionGlossmask = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/gloss_mask");
    public static Texture dissolutionGloss = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/gloss");
    public static Texture metalSymbol = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/dissolution/metal_symbol");

    public static Texture dissolutionGlow = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/select/dissolution_glow");
    public static Texture dissolutionStroke = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/select/dissolution_stroke");

    public static Texture dissolutionIcon = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/icons/dissolution");
    public static Texture dissolutionHover = Brimstone.API.GetTexture("textures/parts/UncommonPrimes/icons/dissolution_hover");

    public static Texture[] irisUp = Brimstone.API.GetAnimation("textures/parts/UncommonPrimes/iris_full_division_high.array", "iris_up", 16);
    public static Texture[] irisDown = Brimstone.API.GetAnimation("textures/parts/UncommonPrimes/iris_full_division_low.array", "iris_down", 16);

    public static readonly HexIndex dissolutionInput1 = new(0, 0);
    public static readonly HexIndex dissolutionInput2 = new(1, 0);
    public static readonly HexIndex dissolutionOutputHigh = new(1, 1);
    public static readonly HexIndex dissolutionOutputLow = new(-1, 1);

    public static void AddPartTypes()
    {
        Similarity = new()
        {
            field_1528 = "uncommon-primes-similarity", // ID
            field_1529 = class_134.method_253("Glyph of Similarity", string.Empty), // Name
            field_1530 = class_134.method_253("The glyph of Similarity transmutes a salt atom into any of the ordinals, based on the two input cardinals.", string.Empty), // Description
            field_1531 = 20, // Cost
            field_1539 = true, // Is a glyph
            field_1549 = class_238.field_1989.field_97.field_386,// triple_glow
            field_1550 = class_238.field_1989.field_97.field_387, // triple_stroke
            field_1547 = similarityIcon, // Panel icon
            field_1548 = similarityHover, // Hovered panel icon
            field_1540 = new HexIndex[]
            {
                similarityInput1,
                similarityInput2,
                similarityOutput
            },
            field_1551 = Permissions.None,
            CustomPermissionCheck = perms => perms.Contains("UncommonPrimes: Similarity")
        };
        QApi.AddPartType(Similarity, static (part, pos, editor, renderer) =>
        {
            Vector2 offset = new(42f, 49f);
            renderer.method_523(similarityBase, Vector2.Zero, offset, 0f);
            renderer.method_528(bowl, similarityInput1, Vector2.Zero);
            renderer.method_528(bowl, similarityInput2, Vector2.Zero);
            renderer.method_529(calcSymbol, similarityInput1, Vector2.Zero);
            renderer.method_529(calcSymbol, similarityInput2, Vector2.Zero);
            renderer.method_529(metalBowl, similarityOutput, Vector2.Zero);

            renderer.method_523(similarityTop, Vector2.Zero, offset, 0f);
        });
        Osmosis = new()
        {
            field_1528 = "uncommon-primes-osmosis", // ID
            field_1529 = class_134.method_253("Glyph of Osmosis", string.Empty), // Name
            field_1530 = class_134.method_253("The glyph of Osmosis accepts two adjacent metals, equalizing them to their opposite-order product. ", string.Empty), // Description
            field_1531 = 20, // Cost
            field_1539 = true, // Is a glyph
            field_1549 = osmosisGlow,
            field_1550 = osmosisStroke,
            field_1547 = osmosisIcon, // Panel icon
            field_1548 = osmosisHover, // Hovered panel icon
            field_1540 = new HexIndex[]
{
                osmosisInputHigh,
                osmosisInputLow,
                osmosisOutput1,
                osmosisOutput2
},
            field_1551 = Permissions.None,
            CustomPermissionCheck = perms => perms.Contains("UncommonPrimes: Osmosis")
        };
        QApi.AddPartType(Osmosis, static (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.method_507().method_481(part);
            class_236 uco = editor.method_1989(part, pos);
            float time = editor.method_504();

            Vector2 offset = new(41f, 120f);
            renderer.method_523(osmosisBase, Vector2.Zero, offset, 0f);
            renderer.method_523(osmosisShadow, Vector2.Zero, offset, 0f);
            renderer.method_523(osmosisBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.field_1984 + class_187.field_1742.method_492(osmosisOutput1).Rotated(uco.field_1985);
            Vector2 risingOffset2 = uco.field_1984 + class_187.field_1742.method_492(osmosisOutput2).Rotated(uco.field_1985);
            if (pss.field_2743)
            {
                irisFrame = class_162.method_404((int)(class_162.method_411(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.method_1121(pss.field_2744[0]);
                risingMetal2 = Molecule.method_1121(pss.field_2744[0]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.method_925(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.method_925(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.method_529(class_238.field_1989.field_90.field_246[irisFrame], osmosisOutput1, Vector2.Zero); //Render Iris
            renderer.method_529(class_238.field_1989.field_90.field_246[irisFrame], osmosisOutput2, Vector2.Zero); //Render Iris
            renderer.method_523(osmosisTop, Vector2.Zero, offset, 0f);
            renderer.method_529(metalSymbolUp, osmosisInputHigh, Vector2.Zero);
            renderer.method_529(metalSymbolDown, osmosisInputLow, Vector2.Zero);
            drawPartGloss(renderer, osmosisGloss, osmosisGlossmask, offset);
            if (pss.field_2743 && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.method_925(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.method_925(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        });
        Dissolution = new()
        {
            field_1528 = "uncommon-primes-dissolution", // ID
            field_1529 = class_134.method_253("Glyph of Dissolution", string.Empty), // Name
            field_1530 = class_134.method_253("The glyph of Dissolution accepts two of the same metal, diverging them to their nearest opposite-order pair. ", string.Empty), // Description
            field_1531 = 20, // Cost
            field_1539 = true, // Is a glyph
            field_1549 = dissolutionGlow,
            field_1550 = dissolutionStroke,
            field_1547 = dissolutionIcon, // Panel icon
            field_1548 = dissolutionHover, // Hovered panel icon
            field_1540 = new HexIndex[]
        {
                dissolutionInput1,
                dissolutionInput2,
                dissolutionOutputHigh,
                dissolutionOutputLow
        },
            field_1551 = Permissions.None,
            CustomPermissionCheck = perms => perms.Contains("UncommonPrimes: Dissolution")
        };
        QApi.AddPartType(Dissolution, static (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.method_507().method_481(part);
            class_236 uco = editor.method_1989(part, pos);
            float time = editor.method_504();

            Vector2 offset = new(91f, 54f);
            renderer.method_523(dissolutionBase, Vector2.Zero, offset, 0f);
            renderer.method_523(dissolutionShadow, Vector2.Zero, offset, 0f);
            renderer.method_523(dissolutionBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.field_1984 + class_187.field_1742.method_492(dissolutionOutputLow).Rotated(uco.field_1985);
            Vector2 risingOffset2 = uco.field_1984 + class_187.field_1742.method_492(dissolutionOutputHigh).Rotated(uco.field_1985);
            if (pss.field_2743)
            {
                irisFrame = class_162.method_404((int)(class_162.method_411(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.method_1121(pss.field_2744[0]);
                risingMetal2 = Molecule.method_1121(pss.field_2744[1]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.method_925(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.method_925(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.method_529(irisDown[irisFrame], dissolutionOutputLow, Vector2.Zero); //Render Iris
            renderer.method_529(irisUp[irisFrame], dissolutionOutputHigh, Vector2.Zero); //Render Iris
            renderer.method_523(dissolutionTop, Vector2.Zero, offset, 0f);
            renderer.method_529(metalSymbol, dissolutionInput1, Vector2.Zero);
            renderer.method_529(metalSymbol, dissolutionInput2, Vector2.Zero);
            drawPartGloss(renderer, dissolutionGloss, dissolutionGlossmask, offset);
            if (pss.field_2743 && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.method_925(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.method_925(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        });


        QApi.AddPartTypeToPanel(Similarity, false);
        QApi.AddPartTypeToPanel(Osmosis, false);
        QApi.AddPartTypeToPanel(Dissolution, false);

        QApi.RunAfterCycle((sim, first) => {
            var seb = sim.field_3818;
            List<Part> allParts = seb.method_502().field_3919;
            var simStates = sim.field_3821;
            Maybe<AtomReference> maybeFindAtom(Part part, HexIndex hex, List<Part> list, bool checkWheels = false)
            {
                return (Maybe<AtomReference>)API.PrivateMethod<Sim>("method_1850").Invoke(sim, new object[] { part, hex, list, checkWheels });
            }



            foreach (var part in allParts)
            {
                class_236 partInfo = seb.method_1989(part, Vector2.Zero);
                PartSimState pss = simStates[part];
                var type = part.method_1159();
                if (type == Similarity)
                {
                    bool left = maybeFindAtom(part, similarityInput1, new List<Part>(), true).method_99(out AtomReference atomInputLeft);
                    bool right = maybeFindAtom(part, similarityInput2, new List<Part>(), true).method_99(out AtomReference atomInputRight);
                    bool output = maybeFindAtom(part, similarityOutput, new List<Part>()).method_99(out AtomReference atomOutput);
                    bool didfindwheelatom = Wheel_MutableBerlos.MaybeFindMutableBerlosWheelAtom(sim, part, similarityOutput).method_99(out AtomReference atomOutputWheel);
                    if (left && right && (output || didfindwheelatom) && (atomOutput.field_2280 == Brimstone.API.VanillaAtoms.salt))
                    {
                        //check atom type
                        foreach (API.SimilarityRecipe recipe in API.SimilarityTransmutation)
                        {
                            if ((recipe.leftinput == (atomInputLeft.field_2280)) && (recipe.rightinput == (atomInputRight.field_2280)))
                            {
                                Brimstone.API.ChangeAtom(atomOutput, recipe.output);
                                atomOutput.field_2279.field_2276 = new class_168(seb, 0, (enum_132)1, atomOutput.field_2280, class_238.field_1989.field_81.field_614, 30f);
                                seb.field_3935.Add(new class_228(seb, (enum_7)1, class_187.field_1742.method_492(part.method_1184(similarityOutput)), similarityFlashAnimation, 30f, Vector2.Zero, partInfo.field_1985));
                                Brimstone.API.PlaySound(sim, Sounds.Similarity);
                                break;

                            }
                        }
                    }
                }
                else if (type == Osmosis)
                {
                    if (first)
                    {
                        // Do atoms exist
                        if ((sim.FindAtomRelative(part, osmosisInputLow).method_99(out AtomReference LowMetal)) && (sim.FindAtomRelative(part, osmosisInputHigh).method_99(out AtomReference HighMetal)))
                        {
                            if ((!sim.FindAtomRelative(part, osmosisOutput1).method_1085()) && (!sim.FindAtomRelative(part, osmosisOutput2).method_1085()))
                            {
                                // if atom isn't being held or consumed
                                if (!LowMetal.field_2281 && !LowMetal.field_2282 && !HighMetal.field_2281 && !HighMetal.field_2282)
                                {

                                    //check atom type
                                    foreach (API.OsmosisRecipe recipe in API.OsmosisTransmutation)
                                    {
                                        if (recipe.lowinput == (LowMetal.field_2280) && recipe.highinput == (HighMetal.field_2280))
                                        {
                                            Brimstone.API.RemoveAtom(LowMetal);
                                            Brimstone.API.RemoveAtom(HighMetal);
                                            seb.field_3937.Add(new(seb, LowMetal.field_2278, recipe.lowinput));
                                            seb.field_3937.Add(new(seb, HighMetal.field_2278, recipe.highinput));
                                            Brimstone.API.DrawFallingAtom(seb, LowMetal);
                                            Brimstone.API.DrawFallingAtom(seb, HighMetal);
                                            pss.field_2743 = true;
                                            pss.field_2744 = new AtomType[1] { recipe.output };
                                            Brimstone.API.PlaySound(sim, Sounds.Osmosis);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (pss.field_2743)
                    {
                        Brimstone.API.AddAtom(sim, part, osmosisOutput1, pss.field_2744[0]);
                        Brimstone.API.AddAtom(sim, part, osmosisOutput2, pss.field_2744[0]);
                    }
                }
                else if (type == Dissolution)
                {
                    if (first)
                    {
                        // Do atoms exist
                        if ((sim.FindAtomRelative(part, dissolutionInput1).method_99(out AtomReference InputMetal1)) && (sim.FindAtomRelative(part, dissolutionInput2).method_99(out AtomReference InputMetal2)))
                        {
                            if ((!sim.FindAtomRelative(part, dissolutionOutputHigh).method_1085()) && (!sim.FindAtomRelative(part, dissolutionOutputLow).method_1085()))
                            {
                                // if atom isn't being held or consumed
                                if (!InputMetal1.field_2281 && !InputMetal1.field_2282 && !InputMetal2.field_2281 && !InputMetal2.field_2282)
                                {

                                    //check atom type
                                    foreach (API.OsmosisRecipe recipe in API.OsmosisTransmutation)
                                    {
                                        if (recipe.output == (InputMetal1.field_2280) && recipe.output == (InputMetal2.field_2280))
                                        {
                                            Brimstone.API.RemoveAtom(InputMetal1);
                                            Brimstone.API.RemoveAtom(InputMetal2);
                                            seb.field_3937.Add(new(seb, InputMetal1.field_2278, recipe.lowinput));
                                            seb.field_3937.Add(new(seb, InputMetal2.field_2278, recipe.highinput));
                                            Brimstone.API.DrawFallingAtom(seb, InputMetal1);
                                            Brimstone.API.DrawFallingAtom(seb, InputMetal2);
                                            pss.field_2743 = true;
                                            pss.field_2744 = new AtomType[2] { recipe.lowinput, recipe.highinput };
                                            Brimstone.API.PlaySound(sim, Sounds.Dissolution);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (pss.field_2743)
                    {
                        Brimstone.API.AddAtom(sim, part, dissolutionOutputLow, pss.field_2744[0]);
                        Brimstone.API.AddAtom(sim, part, dissolutionOutputHigh, pss.field_2744[1]);
                    }
                }
            }
        });
    }
}