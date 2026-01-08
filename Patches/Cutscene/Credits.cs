namespace QoL.Patches.Cutscene;

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

[HarmonyPatch(typeof(CutsceneHelper), nameof(CutsceneHelper.Start))]
internal static class CutsceneHelperPatch
{

    [HarmonyWrapSafe, HarmonyPrefix]
    static void Prefix_Start(CutsceneHelper __instance)
    {
        if (!Configs.SkipCutscene.Value || GameManager.instance.sceneName != "End_Credits_Scroll")
            return;

        __instance.startSkipLocked = false;
        __instance.waitBeforeFadeIn = 0f;
        __instance.skipMode = GlobalEnums.SkipPromptMode.SKIP_INSTANT;
        __instance.UnlockSkip();
    }
}
