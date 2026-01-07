namespace QoL.Patches.Cutscene;

[HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Unlock))]
internal static class SkipTutorialTool
{
    [HarmonyWrapSafe, HarmonyPrefix]
    private static void Prefix_Unlock()
    {
        if (!Configs.SkipTutorialToolMsg.Value)
            return;
            
        PlayerData pd = PlayerData.instance;
        pd.SeenToolGetPrompt = pd.SeenToolWeaponGetPrompt = true;
    }
}
