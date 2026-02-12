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

[HarmonyPatch(typeof(Trapdoor), nameof(Trapdoor.Awake))]
internal static class TrapdoorPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Awake(Trapdoor __instance)
    {
        if (Configs.InstantLevers.Value)
        {
            __instance.retractStartDelay = __instance.retractEndDelay = __instance.openWaitTime = 0f;
        }
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

        if (Configs.OldSkullTyrantLever.Value && __instance.gameObject is { name: "Song_lever_side", scene.name: "Bone_10" })
        {
            __instance.transform.GetChild(2).GetComponent<BoxCollider2D>().offset = new(-4.5f, 2.77f); // Can Hit Range
        }
    }
}

[HarmonyPatch(typeof(Gate), nameof(Gate.Start))]
internal static class GatePatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(Gate __instance)
    {
        if (Configs.OldPutrifiedPlanks.Value && __instance.gameObject is { name: "drop_planks", scene.name: "Aqueduct_04" })
        {
            var canHitCollidder = __instance.transform.GetChild(3).GetComponent<BoxCollider2D>();
            canHitCollidder.size = new(canHitCollidder.size.x * 2, 50f);
        }
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
