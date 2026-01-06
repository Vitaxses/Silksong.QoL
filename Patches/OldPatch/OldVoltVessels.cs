namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(HeroDownAttack), nameof(HeroDownAttack.IsNonBounce))]
internal static class OldVoltVesselsPogoPatch
{
    [HarmonyPostfix]
    public static void Postfix_IsNonBounce(GameObject obj, ref bool __result)
    {
        if (__result || !Configs.OldVoltVessels.Value)
            return;
        
        if (obj.name.StartsWith("Lightning Bola Ball"))
        {
            __result = true;
        }
    }
}
