using BepInEx;
using BepInEx.Logging;

using QoL.Patches;

using UnityEngine.SceneManagement;

namespace QoL;

[BepInAutoPlugin(id: "io.github.vitaxses.qol")]
[BepInDependency("org.silksong-modding.fsmutil", BepInDependency.DependencyFlags.HardDependency)]
public sealed partial class QoLPlugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new(Id);

	internal static new ManualLogSource Logger { get; private set; } = null!;

    private void Awake()
    {
		Logger = base.Logger;

        Configs.Bind(Config);
        SceneManager.activeSceneChanged += OnSceneLoadPatch.OnSceneLoad;
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {Name} ({Id}) v{Version} has loaded!");
    }
}
