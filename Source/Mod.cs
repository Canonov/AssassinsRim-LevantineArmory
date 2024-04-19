using HarmonyLib;

namespace AssassinsRim;

public class Mod : Verse.Mod
{
    public Mod(ModContentPack content) : base(content)
    {
        var harmony = new Harmony("com.AssassinsRim.patches");
        harmony.PatchAll();
    }
}