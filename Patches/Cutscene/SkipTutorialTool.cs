namespace QoL.Patches.Cutscene;

[HarmonyPatch(typeof(PlayerData), nameof(PlayerData.SetupNewPlayerData))]
internal static class SkipTutorialTool
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_SetupNewPlayerData(PlayerData __instance)
    {
        if (!Configs.SkipTutorialToolMsg.Value)
            return;
            
        __instance.SeenToolGetPrompt = __instance.SeenToolWeaponGetPrompt = true;
    }
}
