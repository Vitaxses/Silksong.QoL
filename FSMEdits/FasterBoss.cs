using PD = PlayerData;

namespace QoL.FSMEdits;

public static class FasterBoss
{
    internal static void FasterBOSS(PlayMakerFSM fsm) {
        if (!Configs.FasterBossLoad.Value)
            return;
        
        FasterLace(fsm);
        FasterBellBeast(fsm);
        FasterMossMother(fsm);
        FasterGMS(fsm);
        FasterTrobbio(fsm);
        FasterWidow(fsm);
        FasterLastJudge(fsm);
    }

    internal static void FasterLastJudge(PlayMakerFSM fsm)
    {
        if (fsm.FsmName != "Control" || fsm.gameObject.scene.name != "Coral_Judge_Arena")
            return;
        
        if (fsm.name == "Boss Scene" && PD.instance.bellShrineBellhart && PD.instance.bellShrineBoneForest 
            && PD.instance.bellShrineGreymoor && PD.instance.bellShrineShellwood && PD.instance.bellShrineWilds)
            fsm.ChangeTransition("Init", "UNENCOUNTERED", "Encountered");
        
        else if (fsm.name == "Last Judge") {
            fsm.GetState("Intro Roar")!.AddMethod((action) =>
            {
                PD.instance.encounteredLastJudge = true;
                action.Finish();
            });
        }
    }

    internal static void FasterLace(PlayMakerFSM fsm)
    {
        if (fsm is not { FsmName: "Control", name: "Lace Boss1" })
            return;

        Plugin.Logger.LogDebug("Modifying Lace1 Boss FSM");

        fsm.ChangeTransition("Encountered?", "MEET", "Refight");
    }

    internal static void FasterBellBeast(PlayMakerFSM fsm) 
    {
        if (fsm is not { FsmName: "Return State", name: "Boss Scene", gameObject.scene.name: "Bone_05_boss" } || !ToolItemManager.IsToolEquipped("Silk Spear"))
            return;

        Plugin.Logger.LogDebug("Modifying BellBeast Boss FSM");

        fsm.ChangeTransition("Init", "FINISHED", "Set Return State");
    }

    internal static void FasterMossMother(PlayMakerFSM fsm) 
    {
        if (fsm is not { FsmName: "Control", name: "Mossbone Mother" })
            return;

        Plugin.Logger.LogDebug("Modifying MossMother Boss FSM");

        fsm.gameObject.transform.GetChild(12).position = new Vector3(65.7739f, 15.14f, 0.0062f);
    }

    internal static void FasterGMS(PlayMakerFSM fsm) 
    {
        if (fsm is not { FsmName: "First Challenge", name: "Intro Sequence", gameObject.scene.name: "Cradle_03" })
            return;

        Plugin.Logger.LogDebug("Modifying GMS Boss FSM");

        fsm.ChangeTransition("Challenge Cam", "CHALLENGE", "Quick Start");
    }
    
    internal static void FasterTrobbio(PlayMakerFSM fsm)
    {
        if (fsm is { FsmName: "Control", name: "Trobbio" })
        {
            Plugin.Logger.LogDebug("Modifying Trobbio Boss FSM");

            fsm.ChangeTransition("Init", "FINISHED", "Wait Refight");
            fsm.GetState("Start Pause")!.RemoveFirstActionOfType<Wait>();
            fsm.ChangeTransition("Start Pause", "FINISHED", "Quick Entrance 1");
        }

        else if (fsm is { FsmName: "Control", name: "Tormented Trobbio" })
        {
            Plugin.Logger.LogDebug("Modifying TormentedTrobbio Boss FSM");
            
            fsm.ChangeTransition("State", "MEET", "Start Pause");
            FsmState TTState = fsm.GetState("State")!;
            TTState.RemoveFirstActionOfType<GetPlayerDataBool>();
            TTState.InsertMethod(1, (a) =>
            {
                fsm.FindBoolVariable("Encountered")!.RawValue = true;
                a.Finish();
            });

            fsm.GetState("Trobbio Rise")!.GetFirstActionOfType<ConvertBoolToFloat>()!.trueValue = 0.2f;
            fsm.GetState("Rise End")!.GetFirstActionOfType<ConvertBoolToFloat>()!.trueValue = 0f;
        }
    }
    
    internal static void FasterWidow(PlayMakerFSM fsm)
    {
        if (fsm is not { FsmName: "Control", name: "Boss Scene", gameObject.scene.name: "Belltown_Shrine" })
            return;
            
        Plugin.Logger.LogDebug("Modifying Widow Boss FSM");

        fsm.ChangeTransition("Check State", "UNENCOUNTERED", "State 1");
        fsm.GetState("Spinner Look")!.RemoveFirstActionOfType<Wait>();
    }
}
