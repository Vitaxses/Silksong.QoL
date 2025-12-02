
namespace QoL.Patches;

// prob not the best way to achieve the old behaviour
[HarmonyPatch(typeof(MazeController), nameof(MazeController.SubscribeDoorEntered))]
internal static class OldMist
{
    [HarmonyWrapSafe, HarmonyPrefix]
    private static bool Prefix(MazeController __instance, TransitionPoint door)
    {
        if (!Configs.OldMist.Value) return true;
        
        door.OnBeforeTransition += () =>
        {
            OnBeforeTransition(__instance, door);
        };

        return false;
    }

    internal static void OnBeforeTransition(MazeController controller, TransitionPoint door)
    {
        string doorName = door.name;
        PlayerData pd = PlayerData.instance;

        if (controller.isCapScene)
        {
            pd.PreviousMazeTargetDoor = door.entryPoint;
            pd.PreviousMazeScene = door.gameObject.scene.name;
            pd.PreviousMazeDoor = doorName;
            return;
        }

        bool isRest = door.targetScene == controller.restSceneName;
        bool isNew = pd.PreviousMazeTargetDoor != doorName;

        if (isRest)
        {
            pd.EnteredMazeRestScene = true;
            pd.CorrectMazeDoorsEntered = controller.neededCorrectDoors - controller.restScenePoint;
            pd.IncorrectMazeDoorsEntered = 0;
        }
        else if (isNew)
        {
            if (controller.correctDoors.Contains(door))
            {
                pd.CorrectMazeDoorsEntered++;
                pd.IncorrectMazeDoorsEntered = 0;
            }
            else
            {
                pd.CorrectMazeDoorsEntered = 0;
                pd.IncorrectMazeDoorsEntered++;
                pd.EnteredMazeRestScene = false;

                if (pd.IncorrectMazeDoorsEntered >= controller.allowedIncorrectDoors && doorName.StartsWith("right"))
                {
                    door.SetTargetScene("Dust_Maze_09_entrance");
                    door.entryPoint = "left1";
                }
            }
        }

        pd.PreviousMazeTargetDoor = door.entryPoint;
        pd.PreviousMazeScene = door.gameObject.scene.name;
        pd.PreviousMazeDoor = doorName;
    }
}
