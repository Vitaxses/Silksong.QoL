namespace QoL.FSMEdits;

internal static class FsmCutscene
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

            //fsm.GetState("Hero Control")!.GetFirstActionOfType<PlayerDataVariableTest>()!.Enabled = PlayerData.instance.laceTowerDoorOpened;

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

    internal static void FleaTravel(PlayMakerFSM fsm)
    {
        if (!Configs.SkipCutscene.Value)
            return;
        
        if (fsm is { FsmName: "Quest End Sequence", name: "Caravan"} )
        {
            fsm.GetState("Fade Down")!.GetFirstActionOfType<Wait>()!.time = 1.5f;
            
            FsmState travelState = fsm.GetState("Sound With")!;
            travelState.GetFirstActionOfType<StartPreloadingScene>()!.Enabled = false;
            travelState.GetFirstActionOfType<PlayAudioEvent>()!.Enabled = false;
            travelState.GetFirstActionOfType<Wait>()!.time = 1.5f;
            
            FsmState noTravelState = fsm.GetState("Sound Without")!;
            noTravelState.GetFirstActionOfType<PlayAudioEvent>()!.Enabled = false;
            noTravelState.GetFirstActionOfType<Wait>()!.time = 1.5f;
        }

        else if (fsm is { FsmName: "Cutscene Control", name: "_SceneManager" } )
        {
            FsmState waitState = fsm.GetState("Wait")!;

            waitState.GetLastActionOfType<Wait>()!.time = 3f;
            waitState.GetFirstActionOfType<Wait>()!.time = 0.5f;
            waitState.GetFirstActionOfType<ScreenFader>()!.duration = 0.5f;

            FsmState fadeOutState = fsm.GetState("Fade Out")!;
            
            fadeOutState.GetFirstActionOfType<Wait>()!.time = 0.3f;
            fadeOutState.GetFirstActionOfType<ScreenFader>()!.duration = 0.3f;
            fadeOutState.GetFirstActionOfType<PlayAudioEvent>()!.Enabled = false;
            fadeOutState.GetLastActionOfType<Wait>()!.Enabled = false;
        }
    }

    internal static void ShermaAct3Intro(PlayMakerFSM fsm)
    {
        if (!Configs.SkipCutscene.Value)
            return;

        if (fsm is not { FsmName: "Cutscene Control", name: "door_act3_wakeUp" }) 
            return;

        fsm.GetState("Fade Pause")!.DisableActionsOfType<Wait>();
        fsm.GetState("Sherma Start")!.GetFirstActionOfType<Wait>()!.time = 1.5f;
        FsmState fadeInState = fsm.GetState("Fade In")!;
        fadeInState.GetFirstActionOfType<ScreenFader>()!.duration = 2.5f;
        fadeInState.GetFirstActionOfType<CameraBlurPlaneFade>()!.Duration = 2f;
        fadeInState.GetFirstActionOfType<Wait>()!.time = 4f;
        fsm.GetState("Get Up")!.GetFirstActionOfType<Wait>()!.time = 0.5f;
    }

    internal static void DivingBell(PlayMakerFSM fsm)
    {
        if (!Configs.SkipCutscene.Value)
            return;

        if (fsm is { FsmName: "Dialogue", name: "Wake Up Scene", gameObject.scene.name: "Room_Diving_Bell_Abyss"})
        {
            fsm.ChangeTransition("Wait", FsmEvent.Finished.Name, "Fade Up");
            fsm.GetState("Wait")!.GetFirstActionOfType<Wait>()!.time = 1f;

            FsmState fadeUpState = fsm.GetState("Fade Up")!;
            fadeUpState.GetFirstActionOfType<ScreenFaderAlpha>()!.Duration = 1f;
            fadeUpState.GetFirstActionOfType<Wait>()!.time = 1.5f;

            fsm.ChangeTransition("Fade Up", FsmEvent.Finished.Name, "Get Up");
            fsm.GetState("Get Up")!.AddMethod((action) =>
            {
                HeroController.instance.RegainControl();
            });
        }

        if (fsm is { FsmName: "Sequence", name: "Travel Sequence Control" })
        {
            // Departure
            fsm.GetState("Depressed")!.GetFirstActionOfType<Wait>()!.time = 0.5f;
            fsm.GetState("Hornet Grab")!.DisableActionsOfType<Wait>();
            fsm.GetState("Descend Sequence Fixed")!.DisableActionsOfType<Wait>();

            FsmState fadeState = fsm.GetState("Fade To Black")!;
            fadeState.DisableActionsOfType<Wait>();
            Wait wait = fadeState.GetLastActionOfType<Wait>()!;
            wait.time = 1.5f;
            wait.Enabled = true;

            FsmState cinematicState = fsm.GetState("Cut To Cinematic")!;
            cinematicState.GetFirstActionOfType<ScreenFader>()!.duration =
            cinematicState.GetFirstActionOfType<Wait>()!.time = 1f;
            cinematicState.GetLastActionOfType<Wait>()!.time = 1.5f;

            fsm.GetState("Fade To End")!.GetFirstActionOfType<Wait>()!.time = 1.5f;

            // Arrival
            FsmState fadeUpState = fsm.GetState("Fade Up")!;
            fadeUpState.DisableActionsOfType<Wait>();
            Wait fadeWait = fadeUpState.GetLastActionOfType<Wait>()!;
            fadeWait.time = 0.5f;
            fadeWait.Enabled = true;
        }
    }
    
}
