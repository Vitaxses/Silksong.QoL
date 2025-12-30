namespace QoL.Patches;

[HarmonyPatch(typeof(InputHandler), nameof(InputHandler.SetSkipMode))]
internal static class InputHandlerPatch
{
    private static readonly string[] UnskipScene = { "Bone_East_Umbrella", "Belltown", "Room_Pinstress", "Belltown_Room_pinsmith", 
        "Belltown_Room_doctor", "End_Credits_Scroll", "End_Credits", "Menu_Credits", "End_Game_Completion", 
        "PermaDeath", "Bellway_City", "City_Lace_cutscene", "Opening_Sequence_Act3", "Belltown_Room_Spare" };

    [HarmonyWrapSafe, HarmonyPrefix]
    private static bool Prefix(InputHandler __instance, ref GlobalEnums.SkipPromptMode newMode)
    {
        if (Configs.SkipCutscene.Value && !UnskipScene.Contains(GameManager.instance.sceneName))
        {
            newMode = GlobalEnums.SkipPromptMode.SKIP_INSTANT;
        }
        return true;
    }
}

[HarmonyPatch(typeof(SkippableSequence), nameof(SkippableSequence.CanSkip), MethodType.Getter)]
internal static class SkippableSequencePatch
{
    [HarmonyWrapSafe, HarmonyPrefix]
    private static bool Prefix(SkippableSequence __instance)
    {
        if (Configs.SkipCutscene.Value)
            __instance.AllowSkip();
        
        return true;
    }
}

[HarmonyPatch(typeof(CreditsHelper), nameof(CreditsHelper.Start))]
internal static class CreditsHelperPatch
{

    [HarmonyWrapSafe, HarmonyPrefix]
    static void Prefix_Start(CreditsHelper __instance)
    {
        if (!Configs.SkipCutscene.Value)
            return;

        CutsceneHelper cutSceneHelper = __instance.cutSceneHelper;
        cutSceneHelper.startSkipLocked = false;
        __instance.startPause = cutSceneHelper.waitBeforeFadeIn = 0f;
        cutSceneHelper.skipMode = GlobalEnums.SkipPromptMode.SKIP_INSTANT;
        cutSceneHelper.UnlockSkip();
    }
}

// Skips the Team Cherry icon
[HarmonyPatch(typeof(StartManager), nameof(StartManager.Start))]
internal static class StartManagerPatch
{

    [HarmonyWrapSafe, HarmonyPrefix]
    static void Prefix_Start(StartManager __instance)
    {
        if (Configs.SkipCutscene.Value)
            __instance.startManagerAnimator?.speed = 1000f;
    }
}
