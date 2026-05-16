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

    public static List<SimilarityRecipe> SimilarityTransmutation = new(); // Left input, Right input, Output

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
    }
}