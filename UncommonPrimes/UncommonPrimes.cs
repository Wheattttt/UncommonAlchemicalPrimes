using Quintessential;
using System.IO;

namespace UncommonPrimes;

internal class UncommonPrimes : QuintessentialMod, IDataMod {
    public static UncommonPrimes Instance { get; }
    public static Texture periodicTableOverlayFluxismus;
    public static Texture periodicTableOverlayOrdinals;
    public static Texture periodicTableOverlaySoMetals;
    public override string ModId => "uncommonprimes";

    public override void Load() { }

    public override void LoadContent() {
        Atoms.AddAtomTypes();
        Parts.AddPartTypes();
        Wheels.AddWheelTypes();
        Sounds.AddSounds();
        API.AddTransmutations();
        periodicTableOverlayFluxismus = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/periodic_table/overlay_fluxismus");
        periodicTableOverlayOrdinals = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/periodic_table/overlay_ordinals");
        periodicTableOverlaySoMetals = AssetLoaderHelper.LoadTexture("textures/uncommonprimes/periodic_table/overlay_sometals");
    }
    public override void LoadCompatContent() {
        Atoms.AddAtomTags();
    }
    public override void FinaliseContent() { }

    public override void PostLoad() { }
	public override void Unload() { }
}
