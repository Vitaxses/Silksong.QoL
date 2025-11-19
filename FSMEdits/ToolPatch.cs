using HutongGames.PlayMaker.Actions;

namespace QoL.FSMEdits;

public static class ToolPatch
{
    internal static void VoltVessel(PlayMakerFSM fsm)
    {
        if (!Configs.OldVoltVessels.Value || fsm.FsmName != "Control" || !fsm.name.StartsWith("Lightning Bola Ball"))
            return;

        Plugin.Logger.LogDebug("Modifying Throwable Volt Vessel Tool");

        if (fsm.gameObject.TryGetComponent<NonBouncer>(out var nonBouncer))
        {
            UObject.Destroy(nonBouncer);
        }
    }

    internal static void Scuttlebrace(PlayMakerFSM fsm)
    {
        if (!Configs.OldScuttlebrace.Value || fsm.FsmName != "Tool Attacks")
            return;

        Plugin.Logger.LogDebug("Modifying Scuttlebrace Tool");
        
        fsm.GetState("Scuttle End")!.GetFirstActionOfType<ListenForJump>()!.activeBool = true;
    }
}
