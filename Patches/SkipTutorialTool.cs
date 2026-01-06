namespace QoL.Patches;

[HarmonyPatch(typeof(ToolItem), nameof(ToolItem.Unlock))]
internal static class SkipTutorialTool
{
    [HarmonyWrapSafe, HarmonyPrefix]
    public static void Prefix_Unlock()
    {
        if (!Configs.SkipTutorialToolMsg.Value)
            return;
            
        PlayerData pd = PlayerData.instance;
        pd.SeenToolGetPrompt = pd.SeenToolWeaponGetPrompt = true;
    }
}
