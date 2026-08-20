using Brimstone;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using TrueAnimismus;
using Permissions = enum_149;
using Texture = class_256;

namespace UncommonPrimes;

public class UncommonPrimes : QuintessentialMod
{
    public static QuintessentialMod self;
    // optional dependencies
    public static readonly bool ReductiveMetallurgyLoaded = Brimstone.API.IsModLoaded("ReductiveMetallurgy");
    public static readonly bool TrueAnimismusLoaded = Brimstone.API.IsModLoaded("TrueAnimismus");
    public static readonly bool VacancyLoaded = Brimstone.API.IsModLoaded("Vacancy");

    // Drawing helpers, stolen from RM
    public static Vector2 hexGraphicalOffset(HexIndex hex) => class_187.field_1742.method_492(hex);

    public static string contentPath;
    public override void Load()
    {
        Quintessential.Logger.Log("[UncommonAlchemicalPrimes] Registered");
        if (ReductiveMetallurgyLoaded)
        {
            Logger.Log("[UncommonAlchemicalPrimes] Found Reductive Metallurgy");
        }
        if (TrueAnimismusLoaded)
        {
            Logger.Log("[UncommonAlchemicalPrimes] Found True Animismus");
        }
        if (VacancyLoaded)
        {
            Logger.Log("[UncommonAlchemicalPrimes] Found Vacancy");
        }
    }
    public override void Unload()
    {
        Quintessential.Logger.Log("[UncommonAlchemicalPrimes] Unloaded");
        On.class_177.method_50 -= OnMethod50;
        Sounds.Unload();
    }
    public override void LoadPuzzleContent()
    {
        Quintessential.Logger.Log("[UncommonAlchemicalPrimes] Uncommon Primes Loading");
        UncommonPrimesAtoms.AddAtomTypes();
        UncommonPrimesParts.AddPartTypes();
        Wheel_Servin.LoadContent();
        //Wheel_MutableBerlos.LoadContent();
        contentPath = Brimstone.API.GetContentPath("UncommonPrimes").method_1087();
        Sounds.LoadSounds();
        API.AddTransmutations();
        // Add permissions
        if (API.OrdinalsEnabled == true) // Only show Ordinal glyph permissions if Ordinals are enabled
        {
            QApi.AddPuzzlePermission("UncommonPrimes: Similarity", "Glyph of Similarity", "Uncommon Alchemical Primes");
            QApi.AddPuzzlePermission("UncommonPrimes: Stability", "Glyph of Stability", "Uncommon Alchemical Primes");
        }
        if (API.SecondOrderMetalsEnabled == true) // Same for Second-Order Metals
        {
            QApi.AddPuzzlePermission("UncommonPrimes: Osmosis", "Glyph of Osmosis", "Uncommon Alchemical Primes");
            QApi.AddPuzzlePermission("UncommonPrimes: Dissolution", "Glyph of Dissolution", "Uncommon Alchemical Primes");
            QApi.AddPuzzlePermission("UncommonPrimes: Exchange", "Glyph of Exchange", "Uncommon Alchemical Primes");
        }
        if (API.FluxismusEnabled == true) // Same for Fluxismus
        {
            QApi.AddPuzzlePermission("UncommonPrimes: Fluxismus", "Glyph of Fluxismus", "Uncommon Alchemical Primes");
        }
        if (API.OrdinalsEnabled == true) // Wheels go at the end
        {
            QApi.AddPuzzlePermission("UncommonPrimes: Servin's Wheel", "Servin's Wheel", "Uncommon Alchemical Primes");
        }
            QApi.AddPuzzlePermission("UncommonPrimes: Mutable Berlo's", "[WIP] Mutable Berlo's", "Uncommon Alchemical Primes");
        Quintessential.Logger.Log("[UncommonAlchemicalPrimes] Loaded");
        if (ReductiveMetallurgyLoaded)
        {
            LoadReductiveMetallurgyRules();
        }
        if (TrueAnimismusLoaded)
        {
            LoadTrueAnimismusRules();
        }
        if (VacancyLoaded)
        {
            LoadVacancyRules();
        }
        //------------------------- WHEEL HOOKING, stolen from RM -------------------------//
        IL.SolutionEditorBase.method_1984 += drawWheelAtoms;
        IL.class_123.method_231 += method_231_limitlength;
    }
    private static void drawWheelAtoms(ILContext il)
    {
        ILCursor cursor = new ILCursor(il);
        // skip ahead to roughly where method_2015 is called
        cursor.Goto(658);

        // jump ahead to just after the method_2015 for-loop
        if (!cursor.TryGotoNext(MoveType.After, instr => instr.Match(OpCodes.Ldarga_S))) return;

        // load the SolutionEditorBase self and the class423 local onto the stack so we can use it
        cursor.Emit(OpCodes.Ldarg_0);
        cursor.Emit(OpCodes.Ldloc_0);
        // then run the new code
        cursor.EmitDelegate<Action<SolutionEditorBase, SolutionEditorBase.class_423>>((seb_self, class423) =>
        {
            if (seb_self.method_503() != enum_128.Stopped)
            {
                var partList = seb_self.method_502().field_3919;
                foreach (var servin in partList.Where(x => x.method_1159() == Wheel_Servin.Servin))
                {
                    Wheel_Servin.drawServinAtoms(seb_self, servin, class423.field_3959);
                }
                //foreach (var servin in partList.Where(x => x.method_1159() == Wheel_MutableBerlos.MutableBerlos))
                //{
                //    Wheel_MutableBerlos.drawMutableBerlosAtoms(seb_self, servin, class423.field_3959);
                //}
            }
        });
    }

    public static Texture periodicTableOverlay_Ordinals;
    public static Texture periodicTableOverlay_SoMetals;
    public static Texture periodicTableOverlay_Fluxismus;
    public override void PostLoad()
    {
        periodicTableOverlay_Ordinals = Brimstone.API.GetTexture("textures/periodic_table/UncommonPrimes/overlay_ordinals");
        periodicTableOverlay_SoMetals = Brimstone.API.GetTexture("textures/periodic_table/UncommonPrimes/overlay_sometals");
        periodicTableOverlay_Fluxismus = Brimstone.API.GetTexture("textures/periodic_table/UncommonPrimes/overlay_fluxismus");
        On.SolutionEditorBase.method_1997 += DrawPartSelectionGlows;
        On.class_177.method_50 += OnMethod50;
        hook_Sim_method_1828 = new Hook(API.PrivateMethod<Sim>("method_1828"), OnSimMethod1828_SpawnScaffolds);
    }

    //Modify Periodic Table
    private static void OnMethod50(
    On.class_177.orig_method_50 orig,
    class_177 self,
    float param_3780)
    {
        orig(self, param_3780);
        Vector2 vector = new Vector2(1516f, 922f);
        Vector2 vector2 = (class_115.field_1433 / 2 - vector / 2 + new Vector2(-2f, -11f)).Rounded();

        // Ordinals
        if (API.OrdinalsEnabled == true) //Only show the ordinals on the periodic table if enabled in the API
        {
            class_135.method_272(periodicTableOverlay_Ordinals, vector2 + new Vector2(83f, 94f));
            class_135.method_290("_Bellum_", vector2 + new Vector2(658f, 578f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Obscurum_", vector2 + new Vector2(860f, 578f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Lux_", vector2 + new Vector2(658f, 374f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Pax_", vector2 + new Vector2(860f, 374f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        }

        // Second-Order Metals
        if (API.SecondOrderMetalsEnabled == true) //Only show the metals on the periodic table if enabled in the API
        {
            class_135.method_272(periodicTableOverlay_SoMetals, vector2 + new Vector2(83f, 94f));
            class_135.method_290("_Zinc_", vector2 + new Vector2(1148f, 156f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Nickel_", vector2 + new Vector2(1318f, 272f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Bismuth_", vector2 + new Vector2(1147f, 372f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Cobalt_", vector2 + new Vector2(1318f, 480f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Platinum_", vector2 + new Vector2(1148f, 585f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        }
        if (API.FluxismusEnabled == true) //Only show the metals on the periodic table if enabled in the API
        {
            class_135.method_272(periodicTableOverlay_Fluxismus, vector2 + new Vector2(83f, 94f));
            class_135.method_290("_Muto_", vector2 + new Vector2(1037f, 350f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
            class_135.method_290("_Fixus_", vector2 + new Vector2(1037f, 176f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        }
    }

    // Limit length of Mutable Wheel
    public static void method_231_limitlength(ILContext il)
    {
        ILCursor cursor = new ILCursor(il);
        if (
            !cursor.TryGotoNext(MoveType.After, instr => instr.Match(OpCodes.Ldloc_1))
        ) { return; }
        cursor.RemoveRange(2);
        cursor.Emit(OpCodes.Ldarg_1);
        cursor.EmitDelegate<Func<int, Part, HexIndex>>((int q, Part partType) => {
            if (partType.method_1159() == UncommonPrimesParts.MutableBerlos)
            {
                return new HexIndex(1, 0);
            }
            else
            {
                return new HexIndex(q, 0);
            }
        });
    }
    private static IDetour hook_Sim_method_1828;
    private delegate void orig_Sim_method_1828(Sim sim); //code that runs every cycle but before parts are processed

    // Create Mutable Berlo atoms
    private static void OnSimMethod1828_SpawnScaffolds(orig_Sim_method_1828 orig, Sim sim)
    {
        orig(sim);
        if (sim.method_1818() == 0)//run once at the start of simulation, before arms execute grabs
        {
            var partDict = sim.field_3821;
            List<Molecule> molecules = sim.field_3823;
            foreach (var part in partDict.Keys)
            {
                if (part.method_1159() == UncommonPrimesParts.MutableBerlos)
                {
                    Molecule molecule = new Molecule();
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.water), part.method_1184(new HexIndex(0, 1)));
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.salt), part.method_1184(new HexIndex(1, 0)));
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.earth), part.method_1184(new HexIndex(1, -1)));
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.fire), part.method_1184(new HexIndex(0, -1)));
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.salt), part.method_1184(new HexIndex(-1, 0)));
                    molecule.method_1105(new Atom(Brimstone.API.VanillaAtoms.air), part.method_1184(new HexIndex(-1, 1)));
                    molecules.Add(molecule);
                }
            }
        }
    }

    public void DrawPartSelectionGlows(On.SolutionEditorBase.orig_method_1997 orig, SolutionEditorBase seb_self, Part part, Vector2 pos, float alpha)
    {
        if (part.method_1159() == Wheel_Servin.Servin) Wheel_Servin.drawSelectionGlow(seb_self, part, pos, alpha);
        if (part.method_1159() == UncommonPrimesParts.MutableBerlos) Wheel_Servin.drawSelectionGlow(seb_self, part, pos, alpha);
        orig(seb_self, part, pos, alpha);
    }
    private static void LoadReductiveMetallurgyRules()
    {
        //Add RM Rejection Rules for new atoms
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Platinum, UncommonPrimesAtoms.Cobalt);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Cobalt, UncommonPrimesAtoms.Bismuth);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Zinc);
        // Add RM Deposition Rules
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Platinum, Brimstone.API.VanillaAtoms.iron, Brimstone.API.VanillaAtoms.tin);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Cobalt, Brimstone.API.VanillaAtoms.tin, Brimstone.API.VanillaAtoms.tin);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Bismuth, Brimstone.API.VanillaAtoms.tin, Brimstone.API.VanillaAtoms.lead);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Nickel, Brimstone.API.VanillaAtoms.lead, Brimstone.API.VanillaAtoms.lead);
        // Add RM Proliferation
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Platinum);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Cobalt);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Bismuth);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Zinc);
    }
    private static void LoadTrueAnimismusRules()
    {
        //Add TA Disproportion
        TrueAnimismus.API.addDisproportionRule(UncommonPrimesAtoms.Muto, UncommonPrimesAtoms.PaleMuto, Brimstone.API.VanillaAtoms.quicksilver);
        TrueAnimismus.API.addDisproportionRule(UncommonPrimesAtoms.PaleMuto, UncommonPrimesAtoms.TrueMuto, UncommonPrimesAtoms.Muto);
        TrueAnimismus.API.addDisproportionRule(UncommonPrimesAtoms.Fixus, UncommonPrimesAtoms.DarkFixus, Brimstone.API.VanillaAtoms.quicksilver);
        TrueAnimismus.API.addDisproportionRule(UncommonPrimesAtoms.DarkFixus, UncommonPrimesAtoms.TrueFixus, UncommonPrimesAtoms.Fixus);
        // Add TA Left Hand
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.Muto, UncommonPrimesAtoms.Fixus);
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.Fixus, UncommonPrimesAtoms.Muto);
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.PaleMuto, UncommonPrimesAtoms.DarkFixus);
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.DarkFixus, UncommonPrimesAtoms.PaleMuto);
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.TrueMuto, UncommonPrimesAtoms.TrueFixus);
        TrueAnimismus.API.addLeftHandRule(UncommonPrimesAtoms.TrueFixus, UncommonPrimesAtoms.TrueMuto);
        // Add ratings
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.TrueMuto, 3, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.PaleMuto, 2, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.Muto, 1, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(Brimstone.API.VanillaAtoms.quicksilver, 0, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.Fixus, -1, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.DarkFixus, -2, "fluxismus"));
        TrueAnimismus.API.AtomsForRating.Add(new(UncommonPrimesAtoms.TrueFixus, -3, "fluxismus"));
    }

    private static void LoadVacancyRules()
    {
        API.OsmosisTransmutation.Add(new(Vaca.MainClass.VacaAtom, Brimstone.API.VanillaAtoms.lead, UncommonPrimesAtoms.Arsenic));
        API.OsmosisTransmutation.Add(new(UncommonPrimesAtoms.Arsenic, UncommonPrimesAtoms.Zinc, Brimstone.API.VanillaAtoms.lead));
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Arsenic);
    }
}
