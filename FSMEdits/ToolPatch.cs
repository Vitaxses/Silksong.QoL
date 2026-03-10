namespace QoL.FSMEdits;

public static class ToolPatch
{
    internal static void Scuttlebrace(PlayMakerFSM fsm)
    {
        if (!Configs.OldScuttlebrace.Value || fsm.FsmName != "Tool Attacks")
            return;

        Plugin.Logger.LogDebug("Modifying Scuttlebrace Tool");
        
        fsm.GetState("Scuttle End")!.GetFirstActionOfType<ListenForJump>()!.activeBool = true;
    }
}
