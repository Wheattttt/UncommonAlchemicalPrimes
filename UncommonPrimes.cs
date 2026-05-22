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
using Permissions = enum_149;
using Texture = class_256;

namespace UncommonPrimes;

public class UncommonPrimes : QuintessentialMod
{
    // optional dependencies
    public static readonly bool ReductiveMetallurgyLoaded = Brimstone.API.IsModLoaded("ReductiveMetallurgy");

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
        QApi.AddPuzzlePermission("UncommonPrimes: Similarity", "Glyph of Similarity", "Uncommon Alchemical Primes");
        QApi.AddPuzzlePermission("UncommonPrimes: Osmosis", "Glyph of Osmosis", "Uncommon Alchemical Primes");
        QApi.AddPuzzlePermission("UncommonPrimes: Dissolution", "Glyph of Dissolution", "Uncommon Alchemical Primes");
        QApi.AddPuzzlePermission("UncommonPrimes: Servin's Wheel", "Servin's Wheel", "Uncommon Alchemical Primes");
        //QApi.AddPuzzlePermission("UncommonPrimes: Mutable Van Berlo's Wheel", "Mutable Van Berlo's Wheel", "Uncommon Primes");
        Quintessential.Logger.Log("[UncommonAlchemicalPrimes] Loaded");
        if (ReductiveMetallurgyLoaded)
        {
            LoadReductiveMetallurgyRules();
        }
        //------------------------- WHEEL HOOKING, stolen from RM -------------------------//
        IL.SolutionEditorBase.method_1984 += drawWheelAtoms;
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

    public static Texture periodicTableOverlay;
    public override void PostLoad()
    {
        periodicTableOverlay = Brimstone.API.GetTexture("textures/periodic_table/UncommonPrimes/overlay");
        On.SolutionEditorBase.method_1997 += DrawPartSelectionGlows;
        On.class_177.method_50 += OnMethod50;
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
        class_135.method_272(periodicTableOverlay, vector2 + new Vector2(83f, 94f));

        // Ordinals
        class_135.method_290("_Bellum_", vector2 + new Vector2(658f, 578f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Obscurum_", vector2 + new Vector2(860f, 578f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Lux_", vector2 + new Vector2(658f, 374f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Pax_", vector2 + new Vector2(860f, 374f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);

        // Second-Order Metals
        class_135.method_290("_Zinc_", vector2 + new Vector2(1148f, 156f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Nickel_", vector2 + new Vector2(1318f, 272f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Bismuth_", vector2 + new Vector2(1147f, 372f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Cobalt_", vector2 + new Vector2(1318f, 480f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
        class_135.method_290("_Platinum_", vector2 + new Vector2(1148f, 585f), class_238.field_1990.field_2151, DocumentScreen.field_2410, (enum_0)1, 1f, 0.6f, float.MaxValue, float.MaxValue, 0, default(Color), null, int.MaxValue, param_3473: false, param_3474: true);
    }

    public void DrawPartSelectionGlows(On.SolutionEditorBase.orig_method_1997 orig, SolutionEditorBase seb_self, Part part, Vector2 pos, float alpha)
    {
        if (part.method_1159() == Wheel_Servin.Servin) Wheel_Servin.drawSelectionGlow(seb_self, part, pos, alpha);
        orig(seb_self, part, pos, alpha);
        //if (part.method_1159() == Wheel_MutableBerlos.MutableBerlos) Wheel_MutableBerlos.drawSelectionGlow(seb_self, part, pos, alpha);
        //orig(seb_self, part, pos, alpha);
    }
    private static void LoadReductiveMetallurgyRules()
    {
        //Add RM Rejection Rules for new atoms
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Platinum, UncommonPrimesAtoms.Cobalt);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Cobalt, UncommonPrimesAtoms.Bismuth);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addRejectionRule(UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Zinc);
        // Add RM Deposition Rules
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Platinum, UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Cobalt, UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Bismuth, UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Zinc);
        ReductiveMetallurgy.API.addDepositionRule(UncommonPrimesAtoms.Nickel, UncommonPrimesAtoms.Zinc, UncommonPrimesAtoms.Zinc);
        // Add RM Proliferation
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Platinum);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Cobalt);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Bismuth);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Nickel);
        ReductiveMetallurgy.API.addProliferationRule(UncommonPrimesAtoms.Zinc);
    }
}
