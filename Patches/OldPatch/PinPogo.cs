namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(ToolPin), nameof(ToolPin.OnEnable))]
internal static class ToolPinPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    public static void Postfix_OnEnable(ToolPin __instance)
    {
        if (Configs.PinPogo.Value && __instance.TryGetComponent(out NonBouncer nonBouncer))
            UObject.Destroy(nonBouncer);
    }
}
