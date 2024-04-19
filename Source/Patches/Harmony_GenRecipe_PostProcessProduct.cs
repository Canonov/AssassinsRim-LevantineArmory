using HarmonyLib;
using UnityEngine;

namespace AssassinsRim.Patches;

[HarmonyPatch(typeof(GenRecipe), "PostProcessProduct")]
public static class Harmony_GenRecipe_PostProcessProduct
{
    static void Prefix(ref Thing product)
    {
        var compApparelIgnoreStuffColor = product.TryGetComp<CompApparelIgnoreStuffColor>();

        if (compApparelIgnoreStuffColor != null) 
            product.SetColor(Color.white);
    }
}