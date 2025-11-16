namespace QoL.FSMEdits;

public static class FasterBoss
{
    internal static void FasterBOSS(PlayMakerFSM fsm) {
        if (!Configs.FasterBossLoad.Value)
            return;
        
        FasterLace(fsm);
    }

    internal static void FasterLace(PlayMakerFSM fsm)
    {
        if (fsm is not { FsmName: "Control", name: "Lace Boss1" })
            return;

        fsm.ChangeTransition("Encountered?", "MEET", "Refight");
    }
}
