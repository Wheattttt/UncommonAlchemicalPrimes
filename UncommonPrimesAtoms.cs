using Brimstone;
using Quintessential;

namespace UncommonPrimes;

public static class UncommonPrimesAtoms
{
    public static AtomType Bellum, Obscurum, Lux, Pax;

    public static void AddAtomTypes()
    {

        // Replace the vanilla periodic table
        //class_238.field_1989.field_91.field_799 = Brimstone.API.GetTexture("textures/periodic_table/background");

        // Composite Cardinals
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

        QApi.AddAtomType(Bellum);
        QApi.AddAtomType(Obscurum);
        QApi.AddAtomType(Lux);
        QApi.AddAtomType(Pax);

    }
}