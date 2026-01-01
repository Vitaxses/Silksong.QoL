namespace QoL.FSMEdits;

internal static class DreamCutscene
{
    internal static void Widow(PlayMakerFSM fsm)
    {
        if (!Configs.SkipDreamCutscene.Value)
            return;

        if (fsm is { FsmName: "Control", name: "Spinner Boss" })
        {
            var pd = PlayerData.instance;

            if (!(Configs.SkipDreamCutsceneFully.Value && pd.hasWalljump && (pd.hasDash || pd.hasHarpoonDash || pd.hasBrolly || pd.hasDoubleJump)))
            {
                return;
            }

            fsm.GetState("To Memory Scene")!.GetFirstActionOfType<BeginSceneTransition>()!.sceneName = fsm.gameObject.scene.name;
        }

        if (fsm.gameObject.scene.name != "Memory_Needolin")
            return;

        if (fsm is { FsmName: "Memory Control" })
        {
            fsm.ChangeTransition("End Pause", FsmEvent.Finished.Name, "Wait");
        }

        MemorySkipCutscene(fsm);
    }

    internal static void FirstSinner(PlayMakerFSM fsm)
    {
        if (!Configs.SkipDreamCutscene.Value)
            return;

        if (fsm is { FsmName: "Inspection", name: "Shrine First Weaver NPC" })
        {
            var pd = PlayerData.instance;
            
            if (!(Configs.SkipDreamCutsceneFully.Value && pd.hasWalljump && pd.hasDoubleJump && pd.hasHarpoonDash))
            {
                return;
            }

            FsmState toMemoryState = fsm.GetState("To First Sinner Memory")!;
            toMemoryState.GetFirstActionOfType<BeginSceneTransition>()!.sceneName = fsm.gameObject.scene.name;
            toMemoryState.GetFirstActionOfType<Wait>()!.time = 1.5f;

            toMemoryState.InsertMethod((action) =>
            {
                PlayerData.instance.hasSilkBomb = PlayerData.instance.defeatedFirstWeaver = true;
                ToolItemManager.AutoEquip((ToolItem)fsm.Fsm.Variables.FindFsmObject("Equip Tool").Value);
                action.Finish();
            }, 1);

            var action = fsm.GetState("Skill Msg")!.GetFirstActionOfType<SpawnSkillGetMsg>()!;

            toMemoryState.InsertAction(new SpawnSkillGetMsg()
            {
                MsgPrefab = action.MsgPrefab,
                Skill = action.Skill
            }, 2);
        }

        if (fsm.gameObject.scene.name != "Memory_First_Sinner")
            return;

        if (fsm is { FsmName: "Memory Control" })
        {
            fsm.ChangeTransition("End Pause", FsmEvent.Finished.Name, "Get Rune Bomb");
        }

        MemorySkipCutscene(fsm);
    }

    internal static void MemorySkipCutscene(PlayMakerFSM fsm)
    {
        if (fsm is { FsmName: "Control", name: "memory_orb_inspect" })
        {
            fsm.ChangeTransition("Fade To Black", FsmEvent.Finished.Name, "End Memory");
        }
    }
    
    internal static void MemoryThread(PlayMakerFSM fsm)
    {
        if (!Configs.SkipDreamCutscene.Value)
            return;

        if (fsm is { FsmName: "FSM", name: "thread_memory" })
        {
            fsm.ChangeTransition("Burst? Hold.", "TRUE", "Deep Memory Enter Fall");

            FsmState collapseState = fsm.GetState("Collapse")!;
            collapseState.AddAction(new Wait()
            {
                time = 0.7f
            });
            collapseState.DisableActionsOfType<ListenForAnimationEvent>();
        } 
        
        else if (fsm is { FsmName: "To Memory", name: "Memory Group" })
        {
            fsm.GetState("Transition Scene")!.GetFirstActionOfType<ScreenFader>()!.duration = 0.3f;
        }
    }
}
