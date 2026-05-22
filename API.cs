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

    public static List<SimilarityRecipe> SimilarityTransmutation = new(); // Left input, Right input, Output
    public static List<OsmosisRecipe> OsmosisTransmutation = new(); // Low metal input, High metal input, Output

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

        //Osmosis, from basemetals to second order
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.lead, Brimstone.API.VanillaAtoms.tin, UncommonPrimesAtoms.Zinc));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.tin, Brimstone.API.VanillaAtoms.iron, UncommonPrimesAtoms.Nickel));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.iron, Brimstone.API.VanillaAtoms.copper, UncommonPrimesAtoms.Bismuth));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.copper, Brimstone.API.VanillaAtoms.silver, UncommonPrimesAtoms.Cobalt));
        OsmosisTransmutation.Add(new(Brimstone.API.VanillaAtoms.silver, Brimstone.API.VanillaAtoms.gold, UncommonPrimesAtoms.Platinum));
        //Osmosis, from second order to basemetals
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Zinc, UncommonPrimesAtoms.Nickel, Brimstone.API.VanillaAtoms.tin));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Bismuth, Brimstone.API.VanillaAtoms.iron));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Cobalt, Brimstone.API.VanillaAtoms.copper));
        OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Cobalt, UncommonPrimesAtoms.Platinum, Brimstone.API.VanillaAtoms.silver));
    }
}