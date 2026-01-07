namespace QoL.Patches.OldPatch;

[HarmonyPatch(typeof(HeroController), nameof(HeroController.CanDoubleJump))]
internal static class OldFloat
{
    [HarmonyPostfix]
    private static void Postfix_CanDoubleJump(HeroController __instance, ref bool __result)
    {
        if (!Configs.OldFloat.Value)
            return;

        if (!__result)
            return;
        
        __result = !__instance.inputHandler.inputActions.Down.IsPressed;
    }
}
