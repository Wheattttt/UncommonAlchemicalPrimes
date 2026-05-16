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
using AtomTypes = class_175;
using BondStyle = class_200;
using BondType = enum_126;
using PartType = class_139;
using PartTypes = class_191;
using Permissions = enum_149;
using Texture = class_256;

namespace UncommonPrimes;

public class UncommonPrimes : QuintessentialMod
{
    //drawing helpers
    public static Vector2 hexGraphicalOffset(HexIndex hex) => class_187.field_1742.method_492(hex);

    public static Texture[] fetchTextureArray(int length, string path)
    {
        var ret = new Texture[length];
        for (int i = 0; i < ret.Length; i++)
        {
            ret[i] = class_235.method_615(path + (i + 1).ToString("0000"));
        }
        return ret;
    }
    public static string contentPath;

    public override void Load()
    {
        Quintessential.Logger.Log("UP - Uncommon Primes Registered");
    }
    public override void Unload()
    {
        Quintessential.Logger.Log("UP - Uncommon Primes Unloaded");
        Sounds.Unload();
    }

    public override void LoadPuzzleContent()
    {
        Quintessential.Logger.Log("UP - Uncommon Primes Loading");
        UncommonPrimesAtoms.AddAtomTypes();
        UncommonPrimesParts.AddPartTypes();
        Wheel_Servin.LoadContent();
        //Wheel_MutableBerlos.LoadContent();
        contentPath = Brimstone.API.GetContentPath("UncommonPrimes").method_1087();
        Sounds.LoadSounds();
        API.AddTransmutations();
        QApi.AddPuzzlePermission("UncommonPrimes: Similarity", "Glyph of Similarity", "Uncommon Primes");
        QApi.AddPuzzlePermission("UncommonPrimes: Servin's Wheel", "Servin's Wheel", "Uncommon Primes");
        //QApi.AddPuzzlePermission("UncommonPrimes: Mutable Van Berlo's Wheel", "Mutable Van Berlo's Wheel", "Uncommon Primes");
        Quintessential.Logger.Log("UP - Uncommon Primes Loaded");
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

    public override void PostLoad()
    {
        On.SolutionEditorBase.method_1997 += DrawPartSelectionGlows;
    }

    public void DrawPartSelectionGlows(On.SolutionEditorBase.orig_method_1997 orig, SolutionEditorBase seb_self, Part part, Vector2 pos, float alpha)
    {
        if (part.method_1159() == Wheel_Servin.Servin) Wheel_Servin.drawSelectionGlow(seb_self, part, pos, alpha);
        orig(seb_self, part, pos, alpha);
        //if (part.method_1159() == Wheel_MutableBerlos.MutableBerlos) Wheel_MutableBerlos.drawSelectionGlow(seb_self, part, pos, alpha);
        //orig(seb_self, part, pos, alpha);
    }
}
