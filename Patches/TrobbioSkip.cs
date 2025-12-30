namespace QoL.Patches;

[HarmonyPatch(typeof(HitSlidePlatform), nameof(HitSlidePlatform.Awake))]
internal static class TrobbioSkip
{
    
    [HarmonyWrapSafe, HarmonyPostfix]
    public static void Awake_Postfix(HitSlidePlatform __instance)
    {
        if (!Configs.TrobbioSkip.Value)
            return;

        foreach (var tinker in __instance.tinkers)
        {
            tinker.onlyReactToNail = false;
        }
    }
}
