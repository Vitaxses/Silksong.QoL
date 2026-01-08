namespace QoL.Patches.Fast;

[HarmonyPatch(typeof(LiftControl), nameof(LiftControl.Start))]
internal static class LiftControlPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(LiftControl __instance)
    {
        string sceneName = __instance.gameObject.scene.name;
        if (!Configs.FasterLifts.Value || sceneName == "Ward_01")
            return;

        __instance.moveSpeed = 25f;

        if (sceneName != "Library_11")
        {
            __instance.moveSpeed = Configs.SlowerOptions.Value ? 35 : 100;
        }
        
        __instance.moveDelay = 0f;
        __instance.endDelay = 0f;
    }
}

[HarmonyPatch(typeof(ManualLift), nameof(ManualLift.Start))]
internal static class ManualLiftPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(ManualLift __instance)
    {
        if (!Configs.FasterLifts.Value)
            return;

        __instance.moveSpeed = Configs.SlowerOptions.Value ? 40 : 75;
        __instance.acceleration = 12f;
        __instance.moveDelay = 0f;
    }
}
