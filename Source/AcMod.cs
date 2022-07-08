using HarmonyLib;
using Verse;

namespace AcMod
{
    public class AcMod : Mod
    {
        public AcMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.AcMod.patches");
            harmony.PatchAll();
        }
    }
}
