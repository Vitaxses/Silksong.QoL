namespace QoL.FSMEdits;

internal static class SkipWeakness
{
    internal static void SkipWeaknessPatch(PlayMakerFSM fsm)
    {
        if (!Configs.SkipWeakness.Value)
            return;

        if (!PlayerData.instance.churchKeeperIntro && fsm is { name: "Churchkeeper Intro Scene", FsmName: "Control" })
        {
            Plugin.Logger.LogDebug("Modifying Churchkeeper Dialogue");
            PlayerData.instance.churchKeeperIntro = true;
            fsm.ChangeTransition("Pause", FsmEvent.Finished.Name, "Set End");
        } 
        
        else if (fsm is { name: "Weakness Scene", FsmName: "Control" })
        {
            Plugin.Logger.LogDebug("Modifying Weakness Scene");
            fsm.ChangeTransition("Check PD Bool", FsmEvent.Finished.Name, "Activated");
        } 
        
        else if (fsm is { name: "Weakness Cog Drop Scene", FsmName: "Control" })
        {
            Plugin.Logger.LogDebug("Modifying Weakness Scene");
            fsm.ChangeTransition("Init", FsmEvent.Finished.Name, "Activated");
        }
    }
}