using BepInEx;
using BepInEx.Logging;
using FsmUtilPlugin = Silksong.FsmUtil.Plugin;
using Silksong.ModMenu;
using Silksong.ModMenu.Plugin;
using Silksong.ModMenu.Screens;
using Silksong.ModMenu.Elements;

namespace QoL;

[BepInDependency(FsmUtilPlugin.Id)]
[BepInDependency(ModMenuPlugin.Id)]
[BepInAutoPlugin(id: "io.github.vitaxses.qol")]
public sealed partial class QoLPlugin : BaseUnityPlugin, IModMenuCustomMenu
{
    private readonly Harmony harmony = new(Id);

	internal static new ManualLogSource Logger { get; private set; } = null!;

    private void Awake()
    {
		Logger = base.Logger;

        Configs.Bind(Config);
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {Name} ({Id}) v{Version} has loaded!");
    }

    public string ModMenuName() => Name;

    public AbstractMenuScreen BuildCustomMenu()
    {
        PaginatedMenuScreenBuilder builder = new(Name);
        var factory = new ConfigEntryFactory();

        var sectionEntries = Config
            .GroupBy(entry => entry.Value.Definition.Section)
            .Where(g => g.Any())
            .ToList();

        foreach (var section in sectionEntries)
        {
            var sectionBuilder = new PaginatedMenuScreenBuilder(section.Key, 10);

            foreach (var entry in section)
            {
                if (factory.GenerateMenuElement(entry.Value, out MenuElement? element))
                    sectionBuilder.Add(element);
            }

            var sectionMenu = sectionBuilder.Build();
            var sectionButton = new TextButton(LocalizedText.Raw(section.Key))
            {
                OnSubmit = () => MenuScreenNavigation.Show(sectionMenu, HistoryMode.Add)
            };

            builder.Add(sectionButton);
        }

        return builder.Build();
    }

}
