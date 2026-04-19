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
        Configs.LiftSpeed liftSpeed = Configs.FasterLifts.Value;
        if (liftSpeed == Configs.LiftSpeed.Vanilla || !IsInLiftScene(fsm) || fsm is not { FsmName: "Lift Control"})
            return;

        Plugin.Logger.LogDebug("Modifying Lift FSM");

        // Default is 8
        float speed = 0;
        float speedDown = 0;
        if (liftSpeed == Configs.LiftSpeed.SlightlyFaster)
        {
            speed = 16;
            speedDown = 16;
        } else if (liftSpeed == Configs.LiftSpeed.Fast)
        {
            speed = 19;
            speedDown = 31.40f;
        } else if (liftSpeed == Configs.LiftSpeed.VeryFast)
        {
            speed = fsm.gameObject.scene.name == "Bonetown" ? 28 : 21;
            speedDown = 31.40f;
        }

        fsm.FindFloatVariable("Speed")!.Value = speed;
        fsm.GetFirstActionOfType<SetFloatValue>("Init")!.floatValue = speedDown;
    }
}
