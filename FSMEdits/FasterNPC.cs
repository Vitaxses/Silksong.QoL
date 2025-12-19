namespace QoL.FSMEdits;

internal static class FasterNpc
{

    internal static void FasterNPC(PlayMakerFSM fsm)
    {
        if (!Configs.FasterNPC.Value || fsm.FsmName != "Dialogue")
            return;

        if (fsm.gameObject is { name: "Seamstress", scene.name: "Bone_East_Umbrella" }) {
            Plugin.Logger.LogDebug("Modifying Seamstress Dialogue");
            fsm.GetState("DLG After Dress")!.DisableAction(0);
            fsm.gameObject.transform.Find("Exit Lore Wall").localPosition = new Vector3(-30f, 1.91f, 0f);
        }
        
        else if (fsm.gameObject is { name: "Enclave Caretaker FirstMeet", scene.name: "Song_Enclave" }) {

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
