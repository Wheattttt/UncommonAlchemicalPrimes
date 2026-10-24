using MonoMod.Utils;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static Quintessential.PartCycleDelegate;

namespace UncommonPrimes;

internal static class Parts
{
    public static PartType Similarity, Stability, Osmosis, Exchange, Dissolution, Fluxismus;
    public static Texture bowl = Assets.textures.parts.calcinator_bowl;
    public static Texture metalBowl = Assets.textures.parts.projection_glyph.metal_bowl;
    public static Texture[] glyphFlashAnimation = API.LoadAnimation("textures/primamateria/parts/glyph_flash.array", "flash", 10);
    public static Texture[] AtomFlashAnimation = API.LoadAnimation("textures/primamateria/parts/atom_flash.array", "flash", 10);
    const float sixtyDegrees = 60f * (float)Math.PI / 180f;
    public static Texture calcSymbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/similarity/symbols");
    public static Texture ordinalSymbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/stability/ordinals_symbol");

    //Similarity
    public static Texture similarityBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/similarity/base");
    public static Texture similarityTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/similarity/top");
    public static Texture[] similarityFlashAnimation = API.LoadAnimation("textures/uncommonprimes/parts/similarity_glyph_flash.array", "projection_glyph", 10);

    public static Texture similarityIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/similarity");
    public static Texture similarityHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/similarity_hover");

    public static readonly HexIndex similarityInput1 = new(0, 0);
    public static readonly HexIndex similarityInput2 = new(1, 0);
    public static readonly HexIndex similarityOutput = new(0, 1);

    //Stability
    public static Texture stabilityBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/stability/base");
    public static Texture stabilityTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/stability/top");

    public static Texture stabilityIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/stability");
    public static Texture stabilityHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/stability_hover");

    public static readonly HexIndex stabilityOrdinal1Hex = new(0, 0);
    public static readonly HexIndex stabilityOrdinal2Hex = new(1, 0);

    //Osmosis
    public static Texture osmosisBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/base");
    public static Texture osmosisShadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/shadow");
    public static Texture osmosisBottom = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/bottom");
    public static Texture osmosisTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/top");
    public static Texture osmosisGlossmask = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/gloss_mask");
    public static Texture osmosisGloss = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/gloss");
    public static Texture metalSymbolUp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/metal_symbol_up");
    public static Texture metalSymbolDown = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/osmosis/metal_symbol_down");

    public static Texture osmosisGlow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/osmosis_glow");
    public static Texture osmosisStroke = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/osmosis_stroke");

    public static Texture osmosisIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/osmosis");
    public static Texture osmosisHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/osmosis_hover");

    public static readonly HexIndex osmosisInputHigh = new(0, 1);
    public static readonly HexIndex osmosisInputLow = new(1, -1);
    public static readonly HexIndex osmosisOutput1 = new(0, 0);
    public static readonly HexIndex osmosisOutput2 = new(1, 0);

    //Dissolution
    public static Texture dissolutionBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/base");
    public static Texture dissolutionShadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/shadow");
    public static Texture dissolutionBottom = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/bottom");
    public static Texture dissolutionTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/top");
    public static Texture dissolutionGlossmask = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/gloss_mask");
    public static Texture dissolutionGloss = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/gloss");
    public static Texture metalSymbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/dissolution/metal_symbol");

    public static Texture dissolutionGlow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/dissolution_glow");
    public static Texture dissolutionStroke = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/dissolution_stroke");

    public static Texture dissolutionIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/dissolution");
    public static Texture dissolutionHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/dissolution_hover");

    public static Texture[] irisUp = API.LoadAnimation("textures/uncommonprimes/parts/iris_full_division_high.array", "iris_up", 16);
    public static Texture[] irisDown = API.LoadAnimation("textures/uncommonprimes/parts/iris_full_division_low.array", "iris_down", 16);

    public static readonly HexIndex dissolutionInput1 = new(0, 0);
    public static readonly HexIndex dissolutionInput2 = new(1, 0);
    public static readonly HexIndex dissolutionOutputHigh = new(1, 1);
    public static readonly HexIndex dissolutionOutputLow = new(-1, 1);

    //Exchange
    public static Texture exchangeBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/base");
    public static Texture exchangeBottom = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/bottom");
    public static Texture exchangeTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/top");
    public static Texture exchangeGlossmask = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/gloss_mask");
    public static Texture exchangeGloss = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/gloss");
    public static Texture exchangeDemoteSymbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/exchange/lead_symbol");

    public static Texture exchangeGlow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/exchange_glow");
    public static Texture exchangeStroke = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/exchange_stroke");

    public static Texture exchangeIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/exchange");
    public static Texture exchangeHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/exchange_hover");

    public static readonly HexIndex exchangeBowl = new(0, 0);
    public static readonly HexIndex exchangeInputLeft = new(-1, -1);
    public static readonly HexIndex exchangeOutputLeft = new(0, -1);
    public static readonly HexIndex exchangeOutputRight = new(1, -1);
    public static readonly HexIndex exchangeInputRight = new(2, -1);


    //Fluxismus
    public static Texture fluxismusBase = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/base");
    public static Texture fluxismusBottom = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/bottom");
    public static Texture fluxismusTop = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/top");
    public static Texture fluxismusGlossmask = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/gloss_mask");
    public static Texture fluxismusGloss = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/gloss");

    public static Texture fluxismusGlow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/fluxismus_glow");
    public static Texture fluxismusStroke = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/select/fluxismus_stroke");
    public static Texture qsSymbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/fluxismus/quicksilver_symbol");

    public static Texture fluxismusIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/fluxismus");
    public static Texture fluxismusHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/fluxismus_hover");

    public static Texture[] irisMuto = API.LoadAnimation("textures/uncommonprimes/parts/iris_full_muto.array", "iris", 16);
    public static Texture[] irisFixus = API.LoadAnimation("textures/uncommonprimes/parts/iris_full_fixus.array", "iris", 16);

    public static readonly HexIndex fluxismusInput1 = new(0, 0);
    public static readonly HexIndex fluxismusInput2 = new(1, 0);
    public static readonly HexIndex fluxismusOutputMuto = new(1, 1);
    public static readonly HexIndex fluxismusOutputFixus = new(0, -1);


    static Sound projectionActivate => Assets.sounds.glyph_projection;
    public static readonly HexIndex ProjectionInput = new(0, 0);
    public static readonly HexIndex ProjectionOutput = new(1, 0);


    static Sound purificationActivate => Assets.sounds.glyph_purification;
    public static readonly HexIndex PurificationInput1 = new(0, 0);
    public static readonly HexIndex PurificationInput2 = new(1, 0);
    public static readonly HexIndex PurificationOutput = new(0, 1);


    public static void AddPartTypes()
    {
        Similarity = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = Assets.textures.select.triple_glow,
            strokeTexture = Assets.textures.select.triple_stroke,
            baseTexture = similarityIcon, // Panel icon
            hoverTexture = similarityHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                similarityInput1,
                similarityInput2,
                similarityOutput
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("similarity"))
        };
        UncommonPrimes.Instance.AddPartType(Similarity, "similarity", (part, pos, editor, renderer) =>
        {
            Vector2 offset = new(42f, 49f);
            renderer.RenderBase(similarityBase, Vector2.Zero, offset, 0f);
            renderer.RenderRotating(bowl, similarityInput1, Vector2.Zero);
            renderer.RenderRotating(bowl, similarityInput2, Vector2.Zero);
            renderer.RenderUpright(calcSymbol, similarityInput1, Vector2.Zero);
            renderer.RenderUpright(calcSymbol, similarityInput2, Vector2.Zero);
            renderer.RenderUpright(metalBowl, similarityOutput, Vector2.Zero);

            renderer.RenderBase(similarityTop, Vector2.Zero, offset, 0f);
        });

        Stability = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = Assets.textures.select.double_glow,
            strokeTexture = Assets.textures.select.double_stroke,
            baseTexture = stabilityIcon, // Panel icon
            hoverTexture = stabilityHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                stabilityOrdinal1Hex,
                stabilityOrdinal2Hex
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("stability"))
        };
        UncommonPrimes.Instance.AddPartType(Stability, "stability", (part, pos, editor, renderer) =>
        {
            Vector2 offset = new(42f, 48f);
            renderer.RenderBase(stabilityBase, Vector2.Zero, offset, 0f);
            renderer.RenderRotating(bowl, stabilityOrdinal1Hex, Vector2.Zero);
            renderer.RenderRotating(bowl, stabilityOrdinal2Hex, Vector2.Zero);
            renderer.RenderUpright(ordinalSymbol, stabilityOrdinal1Hex, Vector2.Zero);
            renderer.RenderUpright(ordinalSymbol, stabilityOrdinal2Hex, Vector2.Zero);

            renderer.RenderBase(stabilityTop, Vector2.Zero, offset, 0f);
        });

        Osmosis = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = osmosisGlow,
            strokeTexture = osmosisStroke,
            baseTexture = osmosisIcon, // Panel icon
            hoverTexture = osmosisHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                osmosisInputHigh,
                osmosisInputLow,
                osmosisOutput1,
                osmosisOutput2
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("osmosis"))
        };
        UncommonPrimes.Instance.AddPartType(Osmosis, "osmosis", (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 offset = new(41f, 120f);
            renderer.RenderBase(osmosisBase, Vector2.Zero, offset, 0f);
            renderer.RenderBase(osmosisShadow, Vector2.Zero, offset, 0f);
            renderer.RenderBase(osmosisBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.pos + HexGrid.standardGrid.ToPixelCoords(osmosisOutput1).Rotated(uco.rotation);
            Vector2 risingOffset2 = uco.pos + HexGrid.standardGrid.ToPixelCoords(osmosisOutput2).Rotated(uco.rotation);
            if (pss.isProcessing)
            {
                irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                risingMetal2 = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.RenderUpright(Assets.textures.parts.iris_full[irisFrame], osmosisOutput1, Vector2.Zero); //Render Iris
            renderer.RenderUpright(Assets.textures.parts.iris_full[irisFrame], osmosisOutput2, Vector2.Zero); //Render Iris
            renderer.RenderBase(osmosisTop, Vector2.Zero, offset, 0f);
            renderer.RenderUpright(metalSymbolUp, osmosisInputHigh, Vector2.Zero);
            renderer.RenderUpright(metalSymbolDown, osmosisInputLow, Vector2.Zero);
            API.drawPartGloss(renderer, osmosisGloss, osmosisGlossmask, offset);
            if (pss.isProcessing && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
            });
        Dissolution = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = dissolutionGlow,
            strokeTexture = dissolutionStroke,
            baseTexture = dissolutionIcon, // Panel icon
            hoverTexture = dissolutionHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                    dissolutionInput1,
                    dissolutionInput2,
                    dissolutionOutputHigh,
                    dissolutionOutputLow
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("dissolution"))
        };
        UncommonPrimes.Instance.AddPartType(Dissolution, "dissolution", (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 offset = new(91f, 54f);
            renderer.RenderBase(dissolutionBase, Vector2.Zero, offset, 0f);
            renderer.RenderBase(dissolutionShadow, Vector2.Zero, offset, 0f);
            renderer.RenderBase(dissolutionBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.pos + HexGrid.standardGrid.ToPixelCoords(dissolutionOutputLow).Rotated(uco.rotation);
            Vector2 risingOffset2 = uco.pos + HexGrid.standardGrid.ToPixelCoords(dissolutionOutputHigh).Rotated(uco.rotation);
            if (pss.isProcessing)
            {
                irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                risingMetal2 = Molecule.CreateMonoatomic(pss.processingAtoms[1]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.RenderUpright(irisDown[irisFrame], dissolutionOutputLow, Vector2.Zero); //Render Iris
            renderer.RenderUpright(irisUp[irisFrame], dissolutionOutputHigh, Vector2.Zero); //Render Iris
            renderer.RenderBase(dissolutionTop, Vector2.Zero, offset, 0f);
            renderer.RenderUpright(metalSymbol, dissolutionInput1, Vector2.Zero);
            renderer.RenderUpright(metalSymbol, dissolutionInput2, Vector2.Zero);
            API.drawPartGloss(renderer, dissolutionGloss, dissolutionGlossmask, offset);
            if (pss.isProcessing && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        });

        Exchange = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = exchangeGlow,
            strokeTexture = exchangeStroke,
            baseTexture = exchangeIcon, // Panel icon
            hoverTexture = exchangeHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                    exchangeBowl,
                    exchangeInputLeft,
                    exchangeOutputLeft,
                    exchangeOutputRight,
                    exchangeInputRight
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("exchange"))
        };
        UncommonPrimes.Instance.AddPartType(Exchange, "exchange", (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 offset = new(165f, 119f);
            renderer.RenderBase(exchangeBase, Vector2.Zero, offset, 0f);
            renderer.RenderBase(exchangeBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.pos + HexGrid.standardGrid.ToPixelCoords(exchangeOutputLeft).Rotated(uco.rotation);
            Vector2 risingOffset2 = uco.pos + HexGrid.standardGrid.ToPixelCoords(exchangeOutputRight).Rotated(uco.rotation);
            if (pss.isProcessing)
            {
                irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                risingMetal2 = Molecule.CreateMonoatomic(pss.processingAtoms[1]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.RenderUpright(Assets.textures.parts.iris_full[irisFrame], exchangeOutputLeft, Vector2.Zero); //Render Iris
            renderer.RenderUpright(Assets.textures.parts.iris_full[irisFrame], exchangeOutputRight, Vector2.Zero); //Render Iris
            renderer.RenderBase(exchangeTop, Vector2.Zero, offset, 0f);
            renderer.RenderRotating(metalBowl, exchangeBowl, Vector2.Zero); // metal bowl
            renderer.RenderUpright(Assets.textures.parts.projection_glyph.lead_symbol, exchangeBowl, Vector2.Zero); // metal promotion symbol
            renderer.RenderUpright(exchangeDemoteSymbol, exchangeInputLeft, Vector2.Zero); //
            renderer.RenderUpright(exchangeDemoteSymbol, exchangeInputRight, Vector2.Zero); // metal demotion symbols
            API.drawPartGloss(renderer, exchangeGloss, exchangeGlossmask, offset);
            if (pss.isProcessing && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        });

        Fluxismus = new()
        {
            cost = 20, // Cost
            isFullHexCover = true, // Is a glyph
            glowTexture = fluxismusGlow,
            strokeTexture = fluxismusStroke,
            baseTexture = fluxismusIcon, // Panel icon
            hoverTexture = fluxismusHover, // Hovered panel icon
            glyphHexes = new HexIndex[]
            {
                    fluxismusInput1,
                    fluxismusInput2,
                    fluxismusOutputMuto,
                    fluxismusOutputFixus
            },
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("fluxismus"))
        };
        UncommonPrimes.Instance.AddPartType(Fluxismus, "fluxismus", (part, pos, editor, renderer) =>
        {
            PartSimState pss = editor.GetSimulation().GetSimState(part);
            IntermediatePartState uco = editor.GetIntermState(part, pos);
            float time = editor.GetCycleTime();

            Vector2 offset = new(164f, 120f);
            renderer.RenderBase(fluxismusBase, Vector2.Zero, offset, 0f);
            renderer.RenderBase(fluxismusBottom, Vector2.Zero, offset, 0f);

            int irisFrame = 15;
            bool afterIrisOpens = false;
            Molecule risingMetal1 = null;
            Molecule risingMetal2 = null;
            Vector2 risingOffset1 = uco.pos + HexGrid.standardGrid.ToPixelCoords(fluxismusOutputFixus).Rotated(uco.rotation);
            Vector2 risingOffset2 = uco.pos + HexGrid.standardGrid.ToPixelCoords(fluxismusOutputMuto).Rotated(uco.rotation);
            if (pss.isProcessing)
            {
                irisFrame = Utils.Clamp((int)(Utils.InterpolateLinear(1f, -1f, time) * 16f), 0, 15);
                afterIrisOpens = time > 0.5f;
                risingMetal1 = Molecule.CreateMonoatomic(pss.processingAtoms[0]);
                risingMetal2 = Molecule.CreateMonoatomic(pss.processingAtoms[1]);
                if (!afterIrisOpens)
                {
                    // show atom rising behind iris
                    Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                    Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                }
            }
            renderer.RenderUpright(irisMuto[irisFrame], fluxismusOutputMuto, Vector2.Zero); //Render Iris
            renderer.RenderUpright(irisFixus[irisFrame], fluxismusOutputFixus, Vector2.Zero); //Render Iris
            renderer.RenderBase(fluxismusTop, Vector2.Zero, offset, 0f);
            renderer.RenderUpright(qsSymbol, fluxismusInput1, Vector2.Zero);
            renderer.RenderUpright(qsSymbol, fluxismusInput2, Vector2.Zero);
            API.drawPartGloss(renderer, fluxismusGloss, fluxismusGlossmask, offset);
            if (pss.isProcessing && afterIrisOpens)
            {
                // show atom rising infront of iris
                Editor.RenderMolecule(risingMetal1, risingOffset1, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
                Editor.RenderMolecule(risingMetal2, risingOffset2, new HexIndex(0, 0), 0f, 1f, time, 1f, false, null);
            }
        });
        QApi.AddPartTypeToPanel(Similarity, false);
        QApi.AddPartTypeToPanel(Stability, false);
        QApi.AddPartTypeToPanel(Osmosis, false);
        QApi.AddPartTypeToPanel(Dissolution, false);
        QApi.AddPartTypeToPanel(Exchange, false);
        QApi.AddPartTypeToPanel(Fluxismus, false);

        if (API.OrdinalsEnabled)
        {
            UncommonPrimes.Instance.AddPuzzlePermission("similarity");
            UncommonPrimes.Instance.AddPuzzlePermission("stability");
        }
        if (API.SecondOrderMetalsEnabled)
        {
            UncommonPrimes.Instance.AddPuzzlePermission("osmosis");
            UncommonPrimes.Instance.AddPuzzlePermission("dissolution");
            UncommonPrimes.Instance.AddPuzzlePermission("exchange");
        }
        if (API.FluxismusEnabled)
        {
            UncommonPrimes.Instance.AddPuzzlePermission("fluxismus");
        }

        QApi.BindGlyphCycle(Similarity, new(PartCycleExecutionType.Normal, PartsCycle.SimilarityCycle));
        QApi.BindGlyphCycle(Stability, new(PartCycleExecutionType.Normal, PartsCycle.StabilityCycle));
        QApi.BindGlyphCycle(Osmosis, new(PartCycleExecutionType.Normal, PartsCycle.OsmosisCycle));
        QApi.BindGlyphCycle(Dissolution, new(PartCycleExecutionType.Normal, PartsCycle.DissolutionCycle));
        QApi.BindGlyphCycle(Exchange, new(PartCycleExecutionType.Normal, PartsCycle.ExchangeCycle));
        QApi.BindGlyphCycle(Fluxismus, new(PartCycleExecutionType.Normal, PartsCycle.FluxismusCycle));

        UncommonPrimes.Instance.AddRecipe(new(), Similarity.Id, Similarity.Id);
        UncommonPrimes.Instance.AddRecipe(new(), Stability.Id, Stability.Id);
        UncommonPrimes.Instance.AddRecipe(new(), Osmosis.Id, Osmosis.Id);
        UncommonPrimes.Instance.AddRecipe(new(), Dissolution.Id, Dissolution.Id);
        UncommonPrimes.Instance.AddRecipe(new(), Exchange.Id, Exchange.Id);
        UncommonPrimes.Instance.AddRecipe(new(), Fluxismus.Id, Fluxismus.Id);


    }
}