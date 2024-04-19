using HarmonyLib;
using Verse.AI;

// ReSharper disable InconsistentNaming

namespace AssassinsRim.Patches;

// Forces pawns to flip down the hoods of their hooded apparel before they try wearing another hat
// (The game otherwise throws an error when trying to discard an un-discardable hood)
[HarmonyPatch(typeof(Pawn_JobTracker), "TryTakeOrderedJob")]
public static class Harmony_Pawn_JobTracker_TryTakeOrderedJob_ForceHoodDownWhenWearingNewHat
{
    static void Postfix(bool __result, Pawn ___pawn, Job job)
    {
        if (!__result || job.def != JobDefOf.Wear || !job.targetA.Thing.def.apparel.layers.Contains(ApparelLayerDefOf.Overhead)) 
            return; // Only run on a successful Force wear apparel job for a hat
            
        if (___pawn.apparel.WornApparel.Find(x => x.HasComp<CompApparelWithAttachedHeadgear>()) is Apparel hoodedApparel && hoodedApparel.GetComp<CompApparelWithAttachedHeadgear>() is CompApparelWithAttachedHeadgear comp)
        {
            comp.isHatOn = false;
            ___pawn.Drawer.renderer.SetAllGraphicsDirty(); // Pawn needs to be redrawn, or the hood def doesn't disappear from the pawn inventory
        }
    }
}