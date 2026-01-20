namespace QoL.Patches;

[HarmonyPatch(typeof(Lever), nameof(Lever.Start))]
internal static class LeverPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(Lever __instance)
    {
        if (Configs.InstantLevers.Value)
            __instance.openGateDelay = 0f;
    }
}

[HarmonyPatch(typeof(Lever_tk2d), nameof(Lever_tk2d.Start))]
internal static class Lever_tk2dPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(Lever_tk2d __instance)
    {
        if (Configs.InstantLevers.Value)
            __instance.openGateDelay = 0f;
    }
}

[HarmonyPatch(typeof(PressurePlateBase), nameof(PressurePlateBase.Awake))]
internal static class PressurePlateBasePatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(PressurePlateBase __instance)
    {
        if (Configs.InstantLevers.Value)
            __instance.gateOpenDelay = __instance.waitTime = __instance.dropTime = 0f;
    }
}

[HarmonyPatch(typeof(DialDoorBridge), nameof(DialDoorBridge.Start))]
internal static class DialDoorBridgePatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(DialDoorBridge __instance)
    {
        if (Configs.InstantLevers.Value)
        {
            __instance.doorOpenDelay = __instance.moveDelay = 0f;
            __instance.moveDuration = Configs.SlowerOptions.Value ? 1f : 0f;
        }        
    }
}
