namespace QoL.FSMEdits;

internal static class FsmLiftControl
{

    private static bool IsInLiftScene(Component component)
    {
        string baseSceneName = GameManager.InternalBaseSceneName(component.gameObject.scene.name);
        return baseSceneName == "Bonetown" || baseSceneName == "Belltown_06" || baseSceneName == "Room_Forge" || baseSceneName == "Dock_01";
    }

    internal static void Lift(PlayMakerFSM fsm)
    {
        if (!Configs.FasterLifts.Value || !IsInLiftScene(fsm) || fsm is not { FsmName: "Lift Control"})
            return;

        Plugin.Logger.LogDebug("Modifying Lift FSM");

        // Default is 8
        fsm.FindFloatVariable("Speed")!.RawValue = 20f;

        float speedDown =  GameManager.instance.sceneName == "Room_Forge" ? 30f : 31.40f;
        if (Configs.SlowerOptions.Value) speedDown *= 0.75f;

        fsm.GetFirstActionOfType<SetFloatValue>("Init")!.floatValue = speedDown;
    }
}
