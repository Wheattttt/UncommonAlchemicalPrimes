using Brimstone;
using Quintessential;
using Texture = class_256;

namespace UncommonPrimes;

public static class UncommonPrimesAtoms
{
    public static AtomType Bellum, Obscurum, Lux, Pax, Zinc, Nickel, Bismuth, Cobalt, Platinum, Muto, PaleMuto, TrueMuto, Fixus, DarkFixus, TrueFixus;
    public static void AddAtomTypes()
    {
        // Ordinals
        Bellum = Brimstone.API.CreateCardinalAtom(
            ID: 190, 
            modName: "UncommonPrimes",
            name: "Bellum",
            pathToSymbol: "textures/atoms/UncommonPrimes/bellum_symbol",
            pathToBase: "textures/atoms/UncommonPrimes/bellum_base",
            pathToShadow: "textures/atoms/UncommonPrimes/bellum_shadow",
            pathToFog: "textures/atoms/UncommonPrimes/bellum_fog",
            pathToRim: "textures/atoms/UncommonPrimes/fog"
        );
        Obscurum = Brimstone.API.CreateCardinalAtom(
            ID: 191,
            modName: "UncommonPrimes",
            name: "Obscurum",
            pathToSymbol: "textures/atoms/UncommonPrimes/obscurus_symbol",
            pathToBase: "textures/atoms/UncommonPrimes/obscurus_base",
            pathToShadow: "textures/atoms/UncommonPrimes/obscurus_shadow",
            pathToFog: "textures/atoms/UncommonPrimes/fog",
            pathToRim: "textures/atoms/UncommonPrimes/fog"
        );
        Lux = Brimstone.API.CreateCardinalAtom(
            ID: 192,
            modName: "UncommonPrimes",
            name: "Lux",
            pathToSymbol: "textures/atoms/UncommonPrimes/lux_symbol",
            pathToBase: "textures/atoms/UncommonPrimes/lux_base",
            pathToShadow: "textures/atoms/UncommonPrimes/lux_shadow",
            pathToFog: "textures/atoms/UncommonPrimes/fog",
            pathToRim: "textures/atoms/UncommonPrimes/fog"
        );
        Pax = Brimstone.API.CreateCardinalAtom(
            ID: 193,
            modName: "UncommonPrimes",
            name: "Pax",
            pathToSymbol: "textures/atoms/UncommonPrimes/pax_symbol",
            pathToBase: "textures/atoms/UncommonPrimes/pax_base",
            pathToShadow: "textures/atoms/UncommonPrimes/pax_shadow",
            pathToFog: "textures/atoms/UncommonPrimes/fog",
            pathToRim: "textures/atoms/UncommonPrimes/fog"
        );

        // Add second-order metals (In reverse order, as promotesTo can only reference already existing atoms)
        Platinum = Brimstone.API.CreateMetalAtom(
            ID: 160,
            modName: "UncommonPrimes",
            name: "Platinum",
            pathToSymbol: "textures/atoms/UncommonPrimes/metals/platinum_symbol",
            pathToLightramp: "textures/atoms/UncommonPrimes/metals/platinum_lightramp",
            pathToRimlight: "textures/atoms/UncommonPrimes/metals/platinum_rimlight"
        );
        Cobalt = Brimstone.API.CreateMetalAtom(
            ID: 159,
            modName: "UncommonPrimes",
            name: "Cobalt",
            pathToSymbol: "textures/atoms/UncommonPrimes/metals/cobalt_symbol",
            pathToLightramp: "textures/atoms/UncommonPrimes/metals/cobalt_lightramp",
            pathToRimlight: "textures/atoms/UncommonPrimes/metals/cobalt_rimlight",
            promotesTo: Platinum
        );
        Bismuth = Brimstone.API.CreateMetalAtom(
            ID: 158,
            modName: "UncommonPrimes",
            name: "Bismuth",
            pathToSymbol: "textures/atoms/UncommonPrimes/metals/bismuth_symbol",
            pathToLightramp: "textures/atoms/UncommonPrimes/metals/bismuth_lightramp",
            pathToRimlight: "textures/atoms/UncommonPrimes/metals/bismuth_rimlight",
            promotesTo: Cobalt
        );
        Nickel = Brimstone.API.CreateMetalAtom(
            ID: 157,
            modName: "UncommonPrimes",
            name: "Nickel",
            pathToSymbol: "textures/atoms/UncommonPrimes/metals/nickel_symbol",
            pathToLightramp: "textures/atoms/UncommonPrimes/metals/nickel_lightramp",
            pathToRimlight: "textures/atoms/UncommonPrimes/metals/nickel_rimlight",
            promotesTo: Bismuth
        );
        Zinc = Brimstone.API.CreateMetalAtom(
            ID: 156,
            modName: "UncommonPrimes",
            name: "Zinc",
            pathToSymbol: "textures/atoms/UncommonPrimes/metals/zinc_symbol",
            pathToLightramp: "textures/atoms/UncommonPrimes/metals/zinc_lightramp",
            pathToRimlight: "textures/atoms/UncommonPrimes/metals/zinc_rimlight",
            promotesTo: Nickel
        );

        // Add Fluxismus
        Muto = Brimstone.API.CreateNormalAtom(
            ID: 194,
            modName: "UncommonPrimes",
            name: "Muto",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/muto_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/muto_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/muto_shade"
        );
        PaleMuto = Brimstone.API.CreateNormalAtom(
            ID: 195,
            modName: "UncommonPrimes",
            name: "Pale Muto",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/pale_muto_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/pale_muto_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/pale_muto_shade"
        );
        TrueMuto = Brimstone.API.CreateNormalAtom(
            ID: 196,
            modName: "UncommonPrimes",
            name: "True Muto",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_muto_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_muto_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_muto_shade"
        );
        Fixus = Brimstone.API.CreateNormalAtom(
            ID: 197,
            modName: "UncommonPrimes",
            name: "Fixus",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/fixus_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/fixus_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/fixus_shade"
        );
        DarkFixus = Brimstone.API.CreateNormalAtom(
            ID: 198,
            modName: "UncommonPrimes",
            name: "Dark Fixus",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/dark_fixus_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/dark_fixus_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/dark_fixus_shade"
        );
        TrueFixus = Brimstone.API.CreateNormalAtom(
            ID: 199,
            modName: "UncommonPrimes",
            name: "True Fixus",
            pathToSymbol: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_fixus_symbol",
            pathToDiffuse: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_fixus_diffuse",
            pathToShade: "textures/atoms/UncommonPrimes/fluxismus/trueancompat/true_fixus_shade"
        );

        if (API.OrdinalsEnabled == true) // Only add the ordinals to the editor if enabled in the API
        {
            QApi.AddAtomType(Bellum);
            QApi.AddAtomType(Obscurum);
            QApi.AddAtomType(Lux);
            QApi.AddAtomType(Pax);
        }
        if (API.SecondOrderMetalsEnabled == true) // Same for metals
        {
            QApi.AddAtomType(Zinc);
            QApi.AddAtomType(Nickel);
            QApi.AddAtomType(Bismuth);
            QApi.AddAtomType(Cobalt);
            QApi.AddAtomType(Platinum);
        }
        if (API.FluxismusEnabled == true) // Same for fluxismus
        {
            QApi.AddAtomType(Muto);
            if (UncommonPrimes.TrueAnimismusLoaded) // Additionally, if True Animismus is installed, add Colored and True versions of Muto
            {
                QApi.AddAtomType(PaleMuto);
                QApi.AddAtomType(TrueMuto);
            }
            QApi.AddAtomType(Fixus);
            if (UncommonPrimes.TrueAnimismusLoaded) // Same for Fixus
            {
                QApi.AddAtomType(DarkFixus);
                QApi.AddAtomType(TrueFixus);
            }
        }
    }
}