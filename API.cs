using IL;
using Quintessential;
using System.Collections.Generic;
using System.Reflection;
using UncommonPrimes;
using static Brimstone.API;
using AtomTypes = class_175;
using BondType = enum_126;

namespace UncommonPrimes;

public static class API
{
    public static bool OrdinalsEnabled = true;
    public static bool SecondOrderMetalsEnabled = true;
    public static bool FluxismusEnabled = true;
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
    public static List<FluxismusRecipe> FluxismusTransmutation = new(); // Input atom, Upper atom, Lower atom

    public static void AddTransmutations()
    {
        // Similarity recipes
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.fire, Brimstone.API.VanillaAtoms.air, UncommonPrimesAtoms.Bellum));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.air, Brimstone.API.VanillaAtoms.water, UncommonPrimesAtoms.Obscurum));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.water, Brimstone.API.VanillaAtoms.earth, UncommonPrimesAtoms.Pax));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.earth, Brimstone.API.VanillaAtoms.fire, UncommonPrimesAtoms.Lux));
        // ...And the inverse, for now
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.air, Brimstone.API.VanillaAtoms.fire, UncommonPrimesAtoms.Bellum));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.water, Brimstone.API.VanillaAtoms.air, UncommonPrimesAtoms.Obscurum));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.earth, Brimstone.API.VanillaAtoms.water, UncommonPrimesAtoms.Pax));
        SimilarityTransmutation.Add(new(Brimstone.API.VanillaAtoms.fire, Brimstone.API.VanillaAtoms.earth, UncommonPrimesAtoms.Lux));

        // Stability recipes
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Lux, UncommonPrimesAtoms.Bellum, Brimstone.API.VanillaAtoms.fire));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Bellum, UncommonPrimesAtoms.Obscurum, Brimstone.API.VanillaAtoms.air));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Obscurum, UncommonPrimesAtoms.Pax, Brimstone.API.VanillaAtoms.water));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Pax, UncommonPrimesAtoms.Lux, Brimstone.API.VanillaAtoms.earth));
        // Stability recipes, inverse
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Bellum, UncommonPrimesAtoms.Lux, Brimstone.API.VanillaAtoms.fire));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Obscurum, UncommonPrimesAtoms.Bellum, Brimstone.API.VanillaAtoms.air));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Pax, UncommonPrimesAtoms.Obscurum, Brimstone.API.VanillaAtoms.water));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Lux, UncommonPrimesAtoms.Pax, Brimstone.API.VanillaAtoms.earth));
        // Stability recipes, across (opposing ordinals make salt)
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Bellum, UncommonPrimesAtoms.Pax, Brimstone.API.VanillaAtoms.salt));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Obscurum, UncommonPrimesAtoms.Lux, Brimstone.API.VanillaAtoms.salt));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Pax, UncommonPrimesAtoms.Bellum, Brimstone.API.VanillaAtoms.salt));
        StabilityTransmutation.Add(new(UncommonPrimesAtoms.Lux, UncommonPrimesAtoms.Obscurum, Brimstone.API.VanillaAtoms.salt));



        // Osmosis, from basemetals to second order
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.lead, Brimstone.API.VanillaAtoms.tin, UncommonPrimesAtoms.Zinc));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.tin, Brimstone.API.VanillaAtoms.iron, UncommonPrimesAtoms.Nickel));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.iron, Brimstone.API.VanillaAtoms.copper, UncommonPrimesAtoms.Bismuth));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.copper, Brimstone.API.VanillaAtoms.silver, UncommonPrimesAtoms.Cobalt));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.silver, Brimstone.API.VanillaAtoms.gold, UncommonPrimesAtoms.Platinum));
        // Osmosis, from second order to basemetals
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Zinc, UncommonPrimesAtoms.Nickel, Brimstone.API.VanillaAtoms.tin));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Bismuth, Brimstone.API.VanillaAtoms.iron));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Cobalt, Brimstone.API.VanillaAtoms.copper));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Cobalt, UncommonPrimesAtoms.Platinum, Brimstone.API.VanillaAtoms.silver));

        // Fluxismus recipe, just one
        FluxismusTransmutation.Add(new(Brimstone.API.VanillaAtoms.quicksilver, UncommonPrimesAtoms.Muto, UncommonPrimesAtoms.Fixus));
    }
}