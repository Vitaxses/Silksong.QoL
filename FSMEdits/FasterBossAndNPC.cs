using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;

namespace QoL.FSMEdits;

internal static class FasterBossAndNpc
{
    internal static void FasterLace1(PlayMakerFSM fsm)
    {
        if (!Configs.FasterBossLoad.Value)
            return;

        if (fsm is not { FsmName: "Control", gameObject: { name: "Lace Boss1", scene.name: "Bone_East_12" } })
            return;

        fsm.ChangeTransition("Encountered?", "MEET", "Refight");
    }

    internal static void FasterLostLace(PlayMakerFSM fsm) {
        if (!Configs.FasterBossLoad.Value)
            return;

        if (fsm is not { FsmName: "Control", gameObject: { name: "Intro Control", scene.name: "Abyss_Cocoon" } })
            return;

        fsm.ChangeTransition("Check Encountered", FsmEvent.Finished.Name, "ENCOUNTERED");
        
        FsmState stateEmerge = fsm.GetState("Lace Re-emerge")!;
        const float SPEED_MULT = 3f;
        stateEmerge.GetAction<ActivateGameObject>(10)!
            .gameObject.GameObject.Value.GetComponent<Animator>()
            .speed = SPEED_MULT;
        stateEmerge.GetAction<Wait>(11)!.time.Value /= SPEED_MULT;

        fsm.DisableAction("Lace Roar", 4); // Wait
        fsm.DisableAction("Silk Scream", 1); // Wait
    }

    internal static void FasterNPC(PlayMakerFSM fsm)
    {
        if (!Configs.FasterNPC.Value || fsm.FsmName != "Dialogue")
            return;

        if (fsm.gameObject is { name: "Seamstress", scene.name: "Bone_East_Umbrella" }) {

            fsm.GetState("DLG After Dress")!.DisableAction(0);
            fsm.gameObject.transform.Find("Exit Lore Wall").localPosition = new Vector3(-30f, 1.91f, 0f);

        } else if (fsm.gameObject is { name: "Enclave Caretaker FirstMeet", scene.name: "Song_Enclave" }) {

            if (PlayerData.instance.bellShrineEnclave)
                PlayerData.instance.metCaretaker = true;
        }
    }
}