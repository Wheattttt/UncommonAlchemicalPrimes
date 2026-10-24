using Quintessential;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UncommonPrimes;

public static class API
{
    public static bool OrdinalsEnabled = true;
    public static bool SecondOrderMetalsEnabled = true;
    public static bool FluxismusEnabled = true;
    public static Texture[] LoadAnimation(string containingFolder, string frameBaseName, int frameCount, int padding = 4)
    {
        Texture[] anim = new Texture[frameCount];
        for (int i = 0; i < frameCount; i++)
        {
            anim[i] = AssetLoaderHelper.LoadTexture(Path.Combine(containingFolder, frameBaseName) + "_" + (i + 1).ToString().PadLeft(padding, '0'));
        }
        return anim;
    }

    public static Vector2 hexGraphicalOffset(HexIndex hex) => HexGrid.standardGrid.ToPixelCoords(hex);
    public static void drawPartGraphic(PartRenderer renderer, Texture tex, Vector2 graphicPivot, float graphicAngle, Vector2 graphicTranslation, Vector2 screenTranslation)
    {
        drawPartGraphicScaled(renderer, tex, graphicPivot, graphicAngle, graphicTranslation, screenTranslation, new Vector2(1f, 1f));
    }

    public static void drawPartGraphicScaled(PartRenderer renderer, Texture tex, Vector2 graphicPivot, float graphicAngle, Vector2 graphicTranslation, Vector2 screenTranslation, Vector2 scaling)
    {
        //for graphicPivot and graphicTranslation, rightwards is the positive-x direction and upwards is the positive-y direction
        //graphicPivot is an absolute position, with (0,0) denoting the bottom-left corner of the texture
        //graphicTranslation is a translation, so (5,-3) means "translate 5 pixels right and 3 pixels down"
        //graphicAngle is measured in radians, and counterclockwise is the positive-angle direction
        //screenTranslation is the final translation applied, so it is not affected by rotations
        Matrix4 matrixScreenPosition = Matrix4.GetTranslation(renderer.partPos.ToVector3(0f));
        Matrix4 matrixTranslateOnScreen = Matrix4.GetTranslation(screenTranslation.ToVector3(0f));
        Matrix4 matrixRotatePart = Matrix4.RotXY(renderer.partRotation);
        Matrix4 matrixTranslateGraphic = Matrix4.GetTranslation(graphicTranslation.ToVector3(0f));
        Matrix4 matrixRotateGraphic = Matrix4.RotXY(graphicAngle);
        Matrix4 matrixPivotOffset = Matrix4.GetTranslation(-graphicPivot.ToVector3(0f));
        Matrix4 matrixScaling = Matrix4.GetScale(scaling.ToVector3(0f));
        Matrix4 matrixTextureSize = Matrix4.GetScale(tex.size.ToVector3(0f));

        Matrix4 matrix4 = matrixScreenPosition * matrixTranslateOnScreen * matrixRotatePart * matrixTranslateGraphic * matrixRotateGraphic * matrixPivotOffset * matrixScaling * matrixTextureSize;
        TextureRenderer.Render(tex, Color.White, matrix4);
    }
    public static void drawPartGloss(PartRenderer renderer, Texture gloss, Texture glossMask, Vector2 offset)
    {
        drawPartGloss(renderer, gloss, glossMask, offset, new HexIndex(0, 0), 0f);
    }
    public static void drawPartGloss(PartRenderer renderer, Texture gloss, Texture glossMask, Vector2 offset, HexIndex hexOffset, float angle)
    {
        TextureRenderer.GetBatcher().shader = Assets.shaderAssets.maskedGloss;
        TextureRenderer.GetBatcher().textures[1] = gloss;
        var hex = new HexIndex(0, 0);
        Vector2 method2001 = 0.0001f * (renderer.partPos + hexGraphicalOffset(hex).Rotated(renderer.partRotation) - 0.5f * InputManager.screenSize);
        TextureRenderer.GetBatcher().texCoords = method2001;
        drawPartGraphic(renderer, glossMask, offset, angle, hexGraphicalOffset(hexOffset), Vector2.Zero);
        TextureRenderer.GetBatcher().shader = TextureRenderer.GetBatcher().defaultSpriteShader;
        TextureRenderer.GetBatcher().textures[1] = Assets.textures.white;
        TextureRenderer.GetBatcher().texCoords = Vector2.Zero;
    }

    public static MethodInfo PrivateMethod<T>(string method) => typeof(T).GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
    public struct SimilarityRecipe
    {
        public SimilarityRecipe(AtomType leftinput, AtomType rightinput, AtomType output)
        {
            this.leftinput = leftinput;
            this.rightinput = rightinput;
            this.output = output;
        }

        public AtomType leftinput;
        public AtomType rightinput;
        public AtomType output;
    }

    public struct OsmosisRecipe
    {
        public OsmosisRecipe(AtomType lowinput, AtomType highinput, AtomType output)
        {
            this.lowinput = lowinput;
            this.highinput = highinput;
            this.output = output;
        }
        public AtomType lowinput;
        public AtomType highinput;
        public AtomType output;
    }

    public struct HalfDemotionRecipe
    {
        public HalfDemotionRecipe(AtomType metalinput, AtomType metaloutput)
        {
            this.metalinput = metalinput;
            this.metaloutput = metaloutput;
        }
        public AtomType metalinput;
        public AtomType metaloutput;
    }

    public struct FluxismusRecipe
    {
        public FluxismusRecipe(AtomType input, AtomType output_hi, AtomType output_lo)
        {
            this.input = input;
            this.output_hi = output_hi;
            this.output_lo = output_lo;
        }
        public AtomType input;
        public AtomType output_hi;
        public AtomType output_lo;
    }

    public struct StabilityRecipe
    {
        public StabilityRecipe(AtomType ordinalinput1, AtomType ordinalinput2, AtomType output)
        {
            this.ordinalinput1 = ordinalinput1;
            this.ordinalinput2 = ordinalinput2;
            this.output = output;
        }
        public AtomType ordinalinput1;
        public AtomType ordinalinput2;
        public AtomType output;
    }

    public static List<SimilarityRecipe> SimilarityTransmutation = new(); // Left input, Right input, Output
    public static List<StabilityRecipe> StabilityTransmutation = new(); // Ordinal Input, Ordinal Input 2, Output
    public static List<OsmosisRecipe> OsmosisTransmutation = new(); // Low metal input, High metal input, Output
    public static List<HalfDemotionRecipe> HalfDemotionTransmutation = new(); // Metal input, Metal output
    public static List<FluxismusRecipe> FluxismusTransmutation = new(); // Input atom, Upper atom, Lower atom

    public static void AddTransmutations()
    {
        // Similarity recipes
        SimilarityTransmutation.Add(new(AtomTypes.fire, AtomTypes.air, Atoms.Bellum));
        SimilarityTransmutation.Add(new(AtomTypes.air, AtomTypes.water, Atoms.Obscurum));
        SimilarityTransmutation.Add(new(AtomTypes.water, AtomTypes.earth, Atoms.Pax));
        SimilarityTransmutation.Add(new(AtomTypes.earth, AtomTypes.fire, Atoms.Lux));
        // ...And the inverse, for now
        SimilarityTransmutation.Add(new(AtomTypes.air, AtomTypes.fire, Atoms.Bellum));
        SimilarityTransmutation.Add(new(AtomTypes.water, AtomTypes.air, Atoms.Obscurum));
        SimilarityTransmutation.Add(new(AtomTypes.earth, AtomTypes.water, Atoms.Pax));
        SimilarityTransmutation.Add(new(AtomTypes.fire, AtomTypes.earth, Atoms.Lux));

        // Stability recipes
        StabilityTransmutation.Add(new(Atoms.Lux, Atoms.Bellum, AtomTypes.fire));
        StabilityTransmutation.Add(new(Atoms.Bellum, Atoms.Obscurum, AtomTypes.air));
        StabilityTransmutation.Add(new(Atoms.Obscurum, Atoms.Pax, AtomTypes.water));
        StabilityTransmutation.Add(new(Atoms.Pax, Atoms.Lux, AtomTypes.earth));
        // Stability recipes, inverse
        StabilityTransmutation.Add(new(Atoms.Bellum, Atoms.Lux, AtomTypes.fire));
        StabilityTransmutation.Add(new(Atoms.Obscurum, Atoms.Bellum, AtomTypes.air));
        StabilityTransmutation.Add(new(Atoms.Pax, Atoms.Obscurum, AtomTypes.water));
        StabilityTransmutation.Add(new(Atoms.Lux, Atoms.Pax, AtomTypes.earth));
        // Stability recipes, across (opposing ordinals make salt)
        StabilityTransmutation.Add(new(Atoms.Bellum, Atoms.Pax, AtomTypes.salt));
        StabilityTransmutation.Add(new(Atoms.Obscurum, Atoms.Lux, AtomTypes.salt));
        StabilityTransmutation.Add(new(Atoms.Pax, Atoms.Bellum, AtomTypes.salt));
        StabilityTransmutation.Add(new(Atoms.Lux, Atoms.Obscurum, AtomTypes.salt));



        // Osmosis, from basemetals to second order
        OsmosisTransmutation.Add(new(AtomTypes.lead, AtomTypes.tin, Atoms.Zinc));
        OsmosisTransmutation.Add(new(AtomTypes.tin, AtomTypes.iron, Atoms.Nickel));
        OsmosisTransmutation.Add(new(AtomTypes.iron, AtomTypes.copper, Atoms.Bismuth));
        OsmosisTransmutation.Add(new(AtomTypes.copper, AtomTypes.silver, Atoms.Cobalt));
        OsmosisTransmutation.Add(new(AtomTypes.silver, AtomTypes.gold, Atoms.Platinum));
        // Osmosis, from second order to basemetals
        OsmosisTransmutation.Add(new(Atoms.Zinc, Atoms.Nickel, AtomTypes.tin));
        OsmosisTransmutation.Add(new(Atoms.Nickel, Atoms.Bismuth, AtomTypes.iron));
        OsmosisTransmutation.Add(new(Atoms.Bismuth, Atoms.Cobalt, AtomTypes.copper));
        OsmosisTransmutation.Add(new(Atoms.Cobalt, Atoms.Platinum, AtomTypes.silver));

        // HalfDemotions
        HalfDemotionTransmutation.Add(new(AtomTypes.gold, Atoms.Platinum));
        HalfDemotionTransmutation.Add(new(Atoms.Platinum, AtomTypes.silver));
        HalfDemotionTransmutation.Add(new(AtomTypes.silver, Atoms.Cobalt));
        HalfDemotionTransmutation.Add(new(Atoms.Cobalt, AtomTypes.copper));
        HalfDemotionTransmutation.Add(new(AtomTypes.copper, Atoms.Bismuth));
        HalfDemotionTransmutation.Add(new(Atoms.Bismuth, AtomTypes.iron));
        HalfDemotionTransmutation.Add(new(AtomTypes.iron, Atoms.Nickel));
        HalfDemotionTransmutation.Add(new(Atoms.Nickel, AtomTypes.tin));
        HalfDemotionTransmutation.Add(new(AtomTypes.tin, Atoms.Zinc));
        HalfDemotionTransmutation.Add(new(Atoms.Zinc, AtomTypes.lead));

        // Fluxismus recipe, just one
        FluxismusTransmutation.Add(new(AtomTypes.quicksilver, Atoms.Muto, Atoms.Fixus));
    }
}