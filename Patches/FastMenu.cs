namespace QoL.Patches;

[HarmonyPatch(typeof(UIManager))]
internal static class UIManagerPatch
{
    [HarmonyPatch(nameof(UIManager.HideMenu))]
    [HarmonyWrapSafe, HarmonyPrefix]
    private static void HideMenu_Prefix()
    {
        Adjust();
    }

    [HarmonyPatch(nameof(UIManager.Start))]
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Start_Postfix()
    {
        Adjust();
    }

    internal static void Adjust()
    {
        if (Configs.FastUI.Value) UIManager.instance.MENU_FADE_SPEED = Configs.SlowerOptions.Value ? 13 : 14;
        else UIManager.instance.MENU_FADE_SPEED = 3.2f; // default
    }
}
