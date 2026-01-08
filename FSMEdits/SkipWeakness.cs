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

        else if (fsm is { name: "Weakness Scene Act3 Final", FsmName: "Control" })
        {
            Plugin.Logger.LogDebug("Modifying Weakness Scene");
            Trigger2dEvent action = fsm.GetState("Dormant")!.GetFirstActionOfType<Trigger2dEvent>()!;
            Trigger2dEvent @ref = fsm.GetState("Walk R")!.GetLastActionOfType<Trigger2dEvent>()!;
            action.gameObject = @ref.gameObject;
            action.trigger = Trigger2DType.OnTriggerStay2D;
            action.sendEvent = @ref.sendEvent; // HATCH
            action.storeCollider = @ref.storeCollider;

            fsm.GetState("Weak Fall")!.InsertAction(new AddHeroInputBlocker()
            {
                Blocker = fsm.GetState("First Land")!.GetFirstActionOfType<AddHeroInputBlocker>()!.Blocker
            }, 0);

            fsm.GetState("Weak Fall Land")!.GetFirstActionOfType<Wait>()!.time = 0.5f;

            FsmState fadeOutState = fsm.GetState("Fade Out")!;
            fadeOutState.GetFirstActionOfType<ScreenFader>()!.duration = 0.6f;
            fadeOutState.GetFirstActionOfType<Wait>()!.time = 0.6f;

            fsm.ChangeTransition("Get Up To Kneel", FsmEvent.Finished.Name, "Fade Out");
            fsm.GetState("To Enclave Scene")!.InsertAction(new RemoveHeroInputBlocker()
            {
                Blocker = fsm.GetState("Release Lock")!.GetFirstActionOfType<RemoveHeroInputBlocker>()!.Blocker
            }, 4);
        }

        else if (fsm is { FsmName: "Control", name: "Hatch", gameObject.scene.name: "Song_25" })
        {
            fsm.GetState("Wait for Call")!.GetFirstActionOfType<SetFloatValue>()!.floatValue = 0.1f;
        }
    }
}