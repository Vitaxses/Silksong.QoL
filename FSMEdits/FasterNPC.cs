namespace QoL.FSMEdits;

internal static class FasterNpc
{
    internal static void FasterNPC(PlayMakerFSM fsm)
    {
        if (!Configs.FasterNPC.Value)
            return;

        if (fsm is { FsmName: "Sequence", name: "Boss Scene", gameObject.scene.name: "Cog_Dancers"})
        {
            if (!PlayerData.instance.defeatedCogworkDancers)
                fsm.ChangeTransition("Repeat?", "CANCEL", "All Talk Choose Dlg");
            
            fsm.ChangeTransition("Statues End Singing", FsmEvent.Finished.Name, "Return Control");
        } 
        
        /*
        else if (fsm is { FsmName: "Control", name: "Lace NPC Citadel Meet" })
        {
            fsm.DisableActionsOfType<SetMeshRenderer>("Init");
            fsm.AddMethod("Init", (action) =>
            {
                fsm.transform.position = new(30f, 10.57f, 0.006f);
                fsm.transform.GetChild(0).position = new(31f, 10.57f, 0.006f);
            });

            fsm.DisableActionsOfType<HeroPlayLookUpAnim>("Look Up");
            fsm.ChangeTransition("Look Up", FsmEvent.Finished.Name, "Wait");
            fsm.DisableActionsOfType<Wait>("Wait");
            fsm.ChangeTransition("Wait", FsmEvent.Finished.Name, "Jump Antic");

            fsm.DisableActionsOfType<Wait>("Wait");

            fsm.DisableActionsOfType<Wait>("Quest Update");
            fsm.GetFirstActionOfType<Wait>("Wake Rumble")!.time = 0.5f;
        }
        */
        
        if (fsm.FsmName != "Dialogue")
            return;

        if (fsm.gameObject is { name: "Seamstress", scene.name: "Bone_East_Umbrella" })
        {
            Plugin.Logger.LogDebug("Modifying Seamstress Dialogue");
            fsm.GetState("DLG After Dress")!.DisableAction(0);
            fsm.gameObject.transform.Find("Exit Lore Wall").localPosition = new Vector3(-30f, 1.91f, 0f);
        }
        
        else if (fsm.gameObject is { name: "Enclave Caretaker FirstMeet", scene.name: "Song_Enclave" })
        {
            Plugin.Logger.LogDebug("Modifying Caretaker Dialogue");
            if (PlayerData.instance.bellShrineEnclave)
                PlayerData.instance.metCaretaker = true;
        }
        
        else if (fsm.gameObject.name == "Mapper NPC")
        {
            Plugin.Logger.LogDebug("Modifying Shakra Dialogue");
            PlayerData.instance.metMapper = true;
        }
    }
}
