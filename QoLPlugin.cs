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

    public LocalizedText ModMenuName() => Name;

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
            if (section.Key == Configs.NPCIntroSection || section.Key == Configs.ToolPogoSection)
                continue;

            var sectionBuilder = new PaginatedMenuScreenBuilder(section.Key, 8);

            if (section.Key == Configs.OldPatchSection)
            {
                var toolPogoBuilder = new PaginatedMenuScreenBuilder(LocalizedText.Raw("Old Patch " + Configs.ToolPogoSection), 8);
                foreach (var entry in sectionEntries.First(c => c.Key == Configs.ToolPogoSection))
                {
                    if (factory.GenerateMenuElement(entry.Value, out MenuElement? element))
                        toolPogoBuilder.Add(element);
                }
                
                var introButton = new TextButton(LocalizedText.Raw(Configs.ToolPogoSection))
                {
                    OnSubmit = () => MenuScreenNavigation.Show(toolPogoBuilder.Build(), HistoryMode.Add)
                };
                sectionBuilder.Add(introButton);
            }

            foreach (var entry in section)
            {
                if (factory.GenerateMenuElement(entry.Value, out MenuElement? element))
                    sectionBuilder.Add(element);
            }

            if (section.Key == Configs.NPCSection)
            {
                var npcIntroSectionBuilder = new PaginatedMenuScreenBuilder(LocalizedText.Raw("NPC " + Configs.NPCIntroSection), 8);
                foreach (var entry in sectionEntries.First(c => c.Key == Configs.NPCIntroSection))
                {
                    if (factory.GenerateMenuElement(entry.Value, out MenuElement? element))
                        npcIntroSectionBuilder.Add(element);
                }
                
                var introButton = new TextButton(LocalizedText.Raw(Configs.NPCIntroSection))
                {
                    OnSubmit = () => MenuScreenNavigation.Show(npcIntroSectionBuilder.Build(), HistoryMode.Add)
                };
                sectionBuilder.Add(introButton);
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
