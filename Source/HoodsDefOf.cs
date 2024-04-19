using System.Collections.Generic;
using System.Linq;

namespace AssassinsRim;

[DefOf]
public static class HoodsDefOf
{
    private static List<ThingDef> _hoodDefs;
    public static List<ThingDef> HoodDefs => _hoodDefs ??= GetHoodDefs();

    private static List<ThingDef> GetHoodDefs()
    {
        var hoodedApparelDefs = DefDatabase<ThingDef>.AllDefs
            .Where(x => x.HasComp(typeof(CompApparelWithAttachedHeadgear)))
            .ToList();
        
        var hoodDefs = new List<ThingDef>();

        foreach (var hoodedApparelDef in hoodedApparelDefs)
        {
            hoodDefs.Add(hoodedApparelDef.GetCompProperties<CompProperties_ApparelWithAttachedHeadgear>()
                .attachedHeadgearDef);
        }

        return hoodDefs;
    }
}