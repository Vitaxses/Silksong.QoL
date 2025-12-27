namespace QoL.FSMEdits;

internal static class Cutscene
{
    internal static void LastDive(PlayMakerFSM fsm)
    {
        if (!Configs.SkipCutscene.Value)
            return;

        if (fsm is not { FsmName: "Dive Cutscene", gameObject: { name: "Abyss Dive Cutscene", scene.name: "Abyss_05" } })
            return;

        fsm.DisableAction("Preload Scene", 3); // Don't Preload
        BeginSceneTransition bst = fsm.GetAction<BeginSceneTransition>("Dive End", 9)!;
        bst.sceneName = "Abyss_Cocoon";
        bst.entryGateName = "door_entry";
    }

    internal static void Lace2(PlayMakerFSM fsm)
    {
        if (!Configs.SkipCutscene.Value)
            return;

        if (fsm is { name: "door_cutsceneEndLaceTower", FsmName: "Travel Control" })
        {   
            FsmState arriveState = fsm.GetState("Lift Arrive?")!;
            FsmState hereState = fsm.GetState("Lift Already Here")!;

            PlayerDataBoolTest pdbTest = arriveState.GetFirstActionOfType<PlayerDataBoolTest>()!;
            pdbTest.isTrue = pdbTest.isFalse;
            fsm.ChangeTransition(arriveState.Name, "FALSE", hereState.Name);
            
            if (PlayerData.instance.encounteredLaceTower) 
            {
                hereState.GetFirstActionOfType<SendEventToRegister>()!.eventName = "BATTLE START REFIGHT";
            }

            fsm.GetState("Hero Control")!.GetFirstActionOfType<PlayerDataVariableTest>()!.Enabled = PlayerData.instance.laceTowerDoorOpened;

            fsm.ChangeTransition("Lift Active", "INTERACT", "Transition");
            FsmState transitionState = fsm.GetState("Transition")!;
            transitionState.InsertAction(0, new ToolsCutsceneControl()
            {
                SetInCutscene = true
            });
            transitionState.GetFirstActionOfType<BeginSceneTransition>()!.preventCameraFadeOut = true;
        }
        
        else if (fsm is { FsmName: "Sequence", name: "Boss Scene", gameObject.scene.name: "Cog_Dancers"})
        {
            fsm.ChangeTransition("Idle Unlocked", "LIFT INSPECTED", "Travel Instant");
            FsmState travelState = fsm.GetState("Travel Instant")!;
            travelState.InsertAction(0, new ToolsCutsceneControl()
            {
                SetInCutscene = true
            });
            travelState.GetFirstActionOfType<ScreenFader>()!.duration = 0.2f;

            fsm.ChangeTransition("Door Entry", "TRUE", "Hero Exited");

            FsmState exitedState = fsm.GetState("Hero Exited")!;
            exitedState.GetFirstActionOfType<Tk2dPlayAnimationWait>()!.Enabled = false;

            exitedState.AddAction(new AnimatorPlay()
            {
                gameObject = fsm.GetState("Move Down")!.GetFirstActionOfType<AnimatorPlay>()!.gameObject,
                stateName = "Open",
                layer = 0,
                normalizedTime = 3f
            });
        }
    }
}
