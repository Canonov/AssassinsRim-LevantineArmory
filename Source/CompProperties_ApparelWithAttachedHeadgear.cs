namespace AssassinsRim;

[UsedImplicitly]
public class CompProperties_ApparelWithAttachedHeadgear : CompProperties
{
    // ReSharper disable UnassignedField.Global
    public ThingDef attachedHeadgearDef;
    public string toggleUiIconPath;
    // ReSharper restore UnassignedField.Global

    public CompProperties_ApparelWithAttachedHeadgear()
    {
        compClass = typeof(CompApparelWithAttachedHeadgear);
    }
}