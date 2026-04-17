namespace QoL.Patches.Fast;

[HarmonyPatch(typeof(LiftControl), nameof(LiftControl.Start))]
internal static class LiftControlPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(LiftControl __instance)
    {
        string sceneName = __instance.gameObject.scene.name;
        Configs.LiftSpeed liftSpeed = Configs.FasterLifts.Value;

        if (liftSpeed == Configs.LiftSpeed.Vanilla || sceneName == "Ward_01")
            return;

        __instance.moveSpeed = 28f; // Trobbio Underworks lift

        if (sceneName != "Library_11")
        {
            float speed = 0;
            if (liftSpeed == Configs.LiftSpeed.SlightlyFaster)
                speed = 35;
            else if (liftSpeed == Configs.LiftSpeed.Fast)
                speed = 75;
            else if (liftSpeed == Configs.LiftSpeed.VeryFast)
                speed = 90;

            __instance.moveSpeed = speed;
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
        Configs.LiftSpeed liftSpeed = Configs.FasterLifts.Value;
        if (liftSpeed == Configs.LiftSpeed.Vanilla)
            return;

        float speed = __instance.moveSpeed;
        if (liftSpeed == Configs.LiftSpeed.SlightlyFaster)
            speed *= 2;
        else if (liftSpeed == Configs.LiftSpeed.Fast)
            speed = 55;
        else if (liftSpeed == Configs.LiftSpeed.VeryFast)
            speed = 75;

        __instance.moveSpeed = speed;
        __instance.acceleration = 12f;
        __instance.moveDelay = 0f;
    }
}
