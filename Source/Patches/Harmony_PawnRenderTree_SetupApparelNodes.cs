using HarmonyLib;

// ReSharper disable InconsistentNaming

namespace AssassinsRim.Patches;

// Add or remove a dynamically-generated hood headgear, based on the state of the gizmo in the hooded apparel
[HarmonyPatch(typeof(PawnRenderTree), "SetupApparelNodes")]
public static class Harmony_PawnRenderTree_SetupApparelNodes_ToggleAttachedHood
{
    static void Prefix(PawnRenderTree __instance)
    {
        var pawn = __instance.pawn;
            
        if (pawn.apparel == null || pawn.apparel.WornApparelCount <= 0) 
            return;
            
        if (pawn.apparel?.WornApparel?.Find(ap => ap.TryGetComp<CompApparelWithAttachedHeadgear>() is not null) is Apparel apparelWithAttachedHeadgear)
        {
            var comp = apparelWithAttachedHeadgear.GetComp<CompApparelWithAttachedHeadgear>();
            var hoodApparel = (Apparel)ThingMaker.MakeThing(comp.Props.attachedHeadgearDef, apparelWithAttachedHeadgear.Stuff);
            hoodApparel.DrawColor = apparelWithAttachedHeadgear.DrawColor;

            if (comp.isHatOn)
            {
                pawn.apparel.WornApparel.Add(hoodApparel);
            }
            else
            {
                pawn.apparel.WornApparel.RemoveAll(ap => HoodsDefOf.HoodDefs.Contains(ap.def));
            }
        }
        else
        {
            // Remove any worn hoods if the base hooded apparel is removed or destroyed
            pawn.apparel?.WornApparel?.RemoveAll(ap => HoodsDefOf.HoodDefs.Contains(ap.def));
        }
    }
}