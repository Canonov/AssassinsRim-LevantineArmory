using System.Reflection;
using HarmonyLib;

namespace AssassinsRim;

public class CompApparelIgnoreStuffColor : CompColorable
{
    // ReSharper disable once InconsistentNaming
    private static FieldInfo Fi_CompColorable_IsActive => AccessTools.Field(typeof(CompColorable), "active");
        
    public override void Initialize(CompProperties props)
    {
        base.Initialize(props);
        var comp = parent.GetComp<CompColorable>();
        if (comp != null && !comp.Active)
        {
            Fi_CompColorable_IsActive.SetValue(comp, true);
        }
        parent.Notify_ColorChanged();
    }
}