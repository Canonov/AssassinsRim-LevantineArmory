using System.Collections.Generic;
using UnityEngine;

namespace AssassinsRim;

public class CompApparelWithAttachedHeadgear : ThingComp
{
    public CompProperties_ApparelWithAttachedHeadgear Props => (CompProperties_ApparelWithAttachedHeadgear)props;

    public Apparel Apparel => parent as Apparel;
    public Pawn Pawn => Apparel.Wearer;

    public bool isHatOn;

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref isHatOn, "isHatOn", defaultValue: false, forceSave: true);
    }

    public override IEnumerable<Gizmo> CompGetWornGizmosExtra()
    {
        foreach (var gizmo in base.CompGetWornGizmosExtra())
            yield return gizmo;

        if (Pawn == null) 
            yield break;
            
        var commandToggle = new Command_Toggle
        {
            defaultLabel = "AcMod_ToggleableHeadgearCommand_Label".Translate(Props.attachedHeadgearDef.label),
            defaultDesc = "AcMod_ToggleableHeadgearCommand_Desc".Translate(Props.attachedHeadgearDef.label),
            icon = ContentFinder<Texture2D>.Get(Props.toggleUiIconPath),
            isActive = () => isHatOn,
            toggleAction = delegate
            {
                isHatOn = !isHatOn;
                Pawn.Drawer.renderer.SetAllGraphicsDirty();
            },
            turnOffSound = null,
            turnOnSound = null
        };
        yield return commandToggle;
    }
}