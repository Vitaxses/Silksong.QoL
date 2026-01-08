using BepInEx;
using BepInEx.Logging;

namespace QoL;

[BepInAutoPlugin(id: "io.github.vitaxses.qol")]
[BepInDependency("org.silksong-modding.fsmutil", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency(ModCompatibility.ModMenuGuid, BepInDependency.DependencyFlags.SoftDependency)]
public sealed partial class QoLPlugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new(Id);

	internal static new ManualLogSource Logger { get; private set; } = null!;

    private void Awake()
    {
		Logger = base.Logger;

        Configs.Bind(Config);
        harmony.PatchAll();

        ModCompatibility.Init();

        Logger.LogInfo($"Plugin {Name} ({Id}) v{Version} has loaded!");
    }
}
