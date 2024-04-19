using HarmonyLib;

// ReSharper disable InconsistentNaming

namespace AssassinsRim.Patches;

// Hide all other headgear if hood from hooded apparel is worn up
[HarmonyPatch(typeof(PawnRenderTree), "ProcessApparel")]
public static class Harmony_PawnRenderTree_ProcessApparel_HideOtherNonHoodHeadgear
{
    static bool Prefix(PawnRenderTree __instance, Apparel ap, PawnRenderNode headApparelNode)
    {
        var pawn = __instance.pawn;
        if (pawn.apparel.WornApparel.Find(x => x.HasComp<CompApparelWithAttachedHeadgear>()) is Apparel hoodedApparel && hoodedApparel.GetComp<CompApparelWithAttachedHeadgear>() is CompApparelWithAttachedHeadgear comp && comp.isHatOn)
        {
            if (ap.def.apparel.layers.Contains(ApparelLayerDefOf.Overhead) && !HoodsDefOf.HoodDefs.Contains(ap.def))
            {
                return false; // Hide
            }
        }
        return true;
    }
}