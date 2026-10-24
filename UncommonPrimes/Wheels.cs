using MonoMod.Utils;
using Quintessential;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static Quintessential.PartCycleDelegate;

namespace UncommonPrimes;

internal static class Wheels
{
    public static PartType ServinWheel;
    public static Texture servinIcon = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/servin");
    public static Texture servinHover = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/parts/icons/servin_hover");

    public static void AddWheelTypes()
    {
        ServinWheel = new PartType
        {
            baseTexture = servinIcon, // Panel icon
            hoverTexture = servinHover, // Hovered panel icon
            cost = 30,
            isUnique = true,
            layer = PartTypeLayer.Top,
            isProgrammable = true,
            attachedAtoms = new Dictionary<HexIndex, AtomType>
            {
                {new HexIndex(0, 1),Atoms.Obscurum},
                {new HexIndex(1, 0),AtomTypes.salt},
                {new HexIndex(1, -1),Atoms.Pax},
                {new HexIndex(0, -1),Atoms.Lux},
                {new HexIndex(-1, 0),AtomTypes.salt},
                {new HexIndex(-1, 1),Atoms.Bellum}
            },
            canRotateInSimulation = true,
            permissionCategory = PuzzlePermissions.None,
            CustomPermissionCheck = perms => perms.Contains(UncommonPrimes.Instance.GetIdentifier("servinwheel"))
        };
        QApi.AddPartTypeToPanel(ServinWheel, true);
        UncommonPrimes.Instance.AddPartType(ServinWheel, "servinwheel", (part, pos, editor, renderer) => {});
        UncommonPrimes.Instance.AddPuzzlePermission("servinwheel");
    }
}