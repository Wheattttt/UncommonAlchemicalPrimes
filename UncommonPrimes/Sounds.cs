using Quintessential;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UncommonPrimes
{
    internal class Sounds
    {
        public static Sound glyph_dissolution, glyph_osmosis, glyph_similarity;
        public static void AddSounds()
        {
            glyph_dissolution = AssetLoaderHelper.LoadSound("sounds/uncommonprimes/glyph_dissolution");
            glyph_osmosis = AssetLoaderHelper.LoadSound("sounds/uncommonprimes/glyph_osmosis");
            glyph_similarity = AssetLoaderHelper.LoadSound("sounds/uncommonprimes/glyph_similarity");

            Volume.SetVolume(glyph_dissolution, 0.25f);
            Volume.SetVolume(glyph_osmosis, 0.3f);
            Volume.SetVolume(glyph_similarity, 0.35f);
        }
    }
}
