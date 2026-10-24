using Quintessential;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace UncommonPrimes
{
    internal static class Atoms
    {
        public static AtomType Bellum, Obscurum, Lux, Pax, Arsenic, Zinc, Nickel, Bismuth, Cobalt, Platinum, Muto, Fixus;
        public static void AddAtomTypes()
        {
            Bellum = new()
            {
                isGlassy = true,
                shadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/bellum_shadow"),
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/bellum_symbol"),
                glassyTextures = new()
                {
                    base1 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/bellum_base"),
                    fog = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/bellum_fog"),
                    base2 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog")
                    
                }
            };
            UncommonPrimes.Instance.AddAtomType(Bellum, "bellum");

            Obscurum = new()
            {
                isGlassy = true,
                shadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/obscurus_shadow"),
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/obscurus_symbol"),
                glassyTextures = new()
                {
                    base1 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/obscurus_base"),
                    fog = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog"),
                    base2 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog")
                }
            };
            UncommonPrimes.Instance.AddAtomType(Obscurum, "obscurum");

            Lux = new()
            {
                isGlassy = true,
                shadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/lux_shadow"),
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/lux_symbol"),
                glassyTextures = new()
                {
                    base1 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/lux_base"),
                    fog = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog"),
                    base2 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog")
                }
            };
            UncommonPrimes.Instance.AddAtomType(Lux, "lux");

            Pax = new()
            {
                isGlassy = true,
                shadow = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/pax_shadow"),
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/pax_symbol"),
                glassyTextures = new()
                {
                    base1 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/pax_base"),
                    fog = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog"),
                    base2 = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fog")
                }
            };
            UncommonPrimes.Instance.AddAtomType(Pax, "pax");

            //Second Order Metals

            Platinum = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/platinum_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/platinum_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/platinum_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Platinum, "platinum");

            Cobalt = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/cobalt_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/cobalt_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/cobalt_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Cobalt, "cobalt");

            Bismuth = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/bismuth_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/bismuth_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/bismuth_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Bismuth, "bismuth");

            Nickel = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/nickel_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/nickel_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/nickel_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Nickel, "nickel");

            Zinc = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/zinc_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/zinc_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/zinc_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Zinc, "zinc");

            Arsenic = new()
            {
                isMetallic = true,
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/arsenic_symbol"),
                metallicTextures = new()
                {
                    lightramp = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/arsenic_lightramp"),
                    rimlight = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/metals/arsenic_rimlight"),
                    diffuse = Assets.textures.atoms.copper_diffuse
                }
            };
            UncommonPrimes.Instance.AddAtomType(Arsenic, "arsenic");

            //Fluxismus

            Muto = new()
            {
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/muto_symbol"),
                matteTextures = new()
                {
                    diffuse = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/muto_diffuse"),
                    shade = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/muto_shade"),
                }
            };
            UncommonPrimes.Instance.AddAtomType(Muto, "muto");

            Fixus = new()
            {
                shadow = Assets.textures.atoms.shadow,
                symbol = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/fixus_symbol"),
                matteTextures = new()
                {
                    diffuse = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/fixus_diffuse"),
                    shade = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/atoms/fluxismus/fixus_shade"),
                }
            };
            UncommonPrimes.Instance.AddAtomType(Fixus, "fixus");


        }
        public static void AddAtomTags()
        {
            AtomTag.AtomTags["om:calcifiable"].Add(Bellum.Id);
            AtomTag.AtomTags["om:calcifiable"].Add(Obscurum.Id);
            AtomTag.AtomTags["om:calcifiable"].Add(Lux.Id);
            AtomTag.AtomTags["om:calcifiable"].Add(Pax.Id);
            AtomTag.AtomTags["om:duplicatable"].Add(Bellum.Id);
            AtomTag.AtomTags["om:duplicatable"].Add(Obscurum.Id);
            AtomTag.AtomTags["om:duplicatable"].Add(Lux.Id);
            AtomTag.AtomTags["om:duplicatable"].Add(Pax.Id);

            AtomTag.AtomTags["om:proliferatable"].Add(Arsenic.Id);
            AtomTag.AtomTags["om:proliferatable"].Add(Zinc.Id);
            AtomTag.AtomTags["om:proliferatable"].Add(Nickel.Id);
            AtomTag.AtomTags["om:proliferatable"].Add(Bismuth.Id);
            AtomTag.AtomTags["om:proliferatable"].Add(Cobalt.Id);
            AtomTag.AtomTags["om:proliferatable"].Add(Platinum.Id);

            AtomTag.AtomTags["om:$successor_purif"].Add(Arsenic.Id, AtomTypes.lead.Id);
            AtomTag.AtomTags["om:$successor_purif"].Add(Zinc.Id, Nickel.Id);
            AtomTag.AtomTags["om:$successor_purif"].Add(Nickel.Id, Bismuth.Id);
            AtomTag.AtomTags["om:$successor_purif"].Add(Bismuth.Id, Cobalt.Id);
            AtomTag.AtomTags["om:$successor_purif"].Add(Cobalt.Id, Platinum.Id);

            AtomTag.AtomTags["om:$successor_proj"].Add(Arsenic.Id, Zinc.Id);
            AtomTag.AtomTags["om:$successor_proj"].Add(Zinc.Id, Nickel.Id);
            AtomTag.AtomTags["om:$successor_proj"].Add(Nickel.Id, Bismuth.Id);
            AtomTag.AtomTags["om:$successor_proj"].Add(Bismuth.Id, Cobalt.Id);
            AtomTag.AtomTags["om:$successor_proj"].Add(Cobalt.Id, Platinum.Id);

            AtomTag.AtomTags["om:$predecessor"].Add(Platinum.Id, Cobalt.Id);
            AtomTag.AtomTags["om:$predecessor"].Add(Cobalt.Id, Bismuth.Id);
            AtomTag.AtomTags["om:$predecessor"].Add(Bismuth.Id, Nickel.Id);
            AtomTag.AtomTags["om:$predecessor"].Add(Nickel.Id, Zinc.Id);
            //AtomTag.AtomTags["om:$predecessor"].Add(Zinc.Id, Arsenic.Id);

            AtomTag.AtomTags["om:$division_upper"].Add(Platinum.Id, AtomTypes.iron.Id);
            AtomTag.AtomTags["om:$division_lower"].Add(Platinum.Id, AtomTypes.tin.Id);

            AtomTag.AtomTags["om:$division_upper"].Add(Cobalt.Id, AtomTypes.tin.Id);
            AtomTag.AtomTags["om:$division_lower"].Add(Cobalt.Id, AtomTypes.tin.Id);

            AtomTag.AtomTags["om:$division_upper"].Add(Bismuth.Id, AtomTypes.tin.Id);
            AtomTag.AtomTags["om:$division_lower"].Add(Bismuth.Id, AtomTypes.lead.Id);

            AtomTag.AtomTags["om:$division_upper"].Add(Nickel.Id, AtomTypes.lead.Id);
            AtomTag.AtomTags["om:$division_lower"].Add(Nickel.Id, AtomTypes.lead.Id);

        }
    }
}
