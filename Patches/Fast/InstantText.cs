namespace QoL.Patches.Fast;

[HarmonyPatch(typeof(DialogueBox), nameof(DialogueBox.Start))]
internal static class DialogueBoxPatch
{
    [HarmonyWrapSafe, HarmonyPostfix]
    private static void Postfix_Start(DialogueBox __instance)
    {
        if (!Configs.InstantText.Value)
            return;

        __instance.currentRevealSpeed = __instance.regularRevealSpeed = __instance.fastRevealSpeed *= 50;
        __instance.animator.speed = Configs.SlowerOptions.Value ? 2f : 10f;
    }
}
