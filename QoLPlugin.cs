using BepInEx;
using BepInEx.Logging;
using static BepInEx.BepInDependency;
using FsmUtilPlugin = Silksong.FsmUtil.Plugin;

namespace QoL;

[BepInAutoPlugin(id: "io.github.vitaxses.qol")]
[BepInDependency(FsmUtilPlugin.Id, DependencyFlags.HardDependency)]
[BepInDependency(ModCompatibility.ModMenuGuid, DependencyFlags.SoftDependency)]
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
