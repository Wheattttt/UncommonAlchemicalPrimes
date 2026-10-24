using MonoMod;
using UncommonPrimes;
using Quintessential;
using System.Linq;
using UncommonPrimes;

public class patch_PeriodicTableScreen : PeriodicTableScreen
{
    public patch_PeriodicTableScreen(bool useTransition) : base(useTransition) { }

    private extern void orig_RenderFrame(float deltaTime);
    private void RenderFrame(float deltaTime)
    {
        orig_RenderFrame(deltaTime);
        Vector2 vector = new Vector2(1516f, 922f);
        Vector2 vector2 = (InputManager.screenSize / 2 - vector / 2 + new Vector2(-2f, -11f)).Rounded();
        // Ordinals
        if (API.OrdinalsEnabled == true) //Only show the ordinals on the periodic table if enabled in the API
        {
            TextureRenderer.Render(UncommonPrimes.UncommonPrimes.periodicTableOverlayOrdinals, vector2 + new Vector2(83f, 94f));
            TextureRenderer.RenderText("_Bellum_", vector2 + new Vector2(658f, 578f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Obscurum_", vector2 + new Vector2(860f, 578f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Lux_", vector2 + new Vector2(658f, 374f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Pax_", vector2 + new Vector2(860f, 374f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
        }

        // Second-Order Metals
        if (API.SecondOrderMetalsEnabled == true) //Only show the metals on the periodic table if enabled in the API
        {
            TextureRenderer.Render(UncommonPrimes.UncommonPrimes.periodicTableOverlaySoMetals, vector2 + new Vector2(83f, 94f));
            TextureRenderer.RenderText("_Zinc_", vector2 + new Vector2(1148f, 156f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Nickel_", vector2 + new Vector2(1318f, 272f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Bismuth_", vector2 + new Vector2(1147f, 372f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Cobalt_", vector2 + new Vector2(1318f, 480f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Platinum_", vector2 + new Vector2(1148f, 585f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
        }
        if (API.FluxismusEnabled == true) //Only show the metals on the periodic table if enabled in the API
        {
            TextureRenderer.Render(UncommonPrimes.UncommonPrimes.periodicTableOverlayFluxismus, vector2 + new Vector2(83f, 94f));
            TextureRenderer.RenderText("_Muto_", vector2 + new Vector2(1037f, 350f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
            TextureRenderer.RenderText("_Fixus_", vector2 + new Vector2(1037f, 176f), Assets.fonts.cormorant_12_75, DocumentScreen.textBlack, (TextAlignment)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, calculateBounds: false, doRender: true);
        }

    }
}