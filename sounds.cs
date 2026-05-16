using System.Collections.Generic;
using System.Reflection;

namespace UncommonPrimes
{
    internal static class Sounds
    {
        public static Sound Similarity;

        public static void LoadSounds()
        {
            Similarity = Brimstone.API.GetSound(UncommonPrimes.contentPath, "sounds/glyph_similarity").method_1087();

            FieldInfo field = typeof(class_11).GetField("field_52", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<string, float> volumeDictionary = (Dictionary<string, float>)field.GetValue(null);

            volumeDictionary.Add("glyph_similarity", 0.2f);

            On.class_201.method_540 += Sounds.Method_540;
        }

        public static void Unload()
        {
            On.class_201.method_540 -= Sounds.Method_540;
        }

        public static void Method_540(On.class_201.orig_method_540 orig, class_201 self)
        {
            orig(self);
            Similarity.field_4062 = false;
        }
    }
}