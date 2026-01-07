namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(HitSlidePlatform), nameof(HitSlidePlatform.Awake))]
internal static class TrobbioSkip
{
    
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Awake(HitSlidePlatform __instance)
    {
        if (!Configs.TrobbioSkip.Value)
            return;

        foreach (var tinker in __instance.tinkers)
        {
            tinker.onlyReactToNail = false;
        }
    }
}
