namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(NonSlider), nameof(NonSlider.Awake))]
internal static class CloaklessClawline
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Awake(NonSlider __instance)
    {
        if (__instance.gameObject is not { name: "terrain collider (15)", scene.name: "Under_17" } || !Configs.CloaklessClawline.Value)
            return;

        Plugin.Logger.LogDebug("Modifying Cloakless Clawline Wall Grab");
        __instance.transform.position = new Vector3(12.22f, 7.64f, 0f);
        UObject.Destroy(__instance);
    }
}
