using BepInEx;
using BepInEx.Bootstrap;

namespace QoL;

public static class ModCompatibility
{
    public const string ModMenuGuid = "org.silksong-modding.modmenu";
    public const string SupportedModMenuVersion = "0.2.0";

    private static Type FactoryType = null!;
    private static Type BuilderType = null!;
    private static Type TextButtonType = null!;
    private static Type MenuNavType = null!;

    public static void Init()
    {
        if (!Chainloader.PluginInfos.TryGetValue(ModMenuGuid, out var modMenu))
            return;

        if (modMenu.Metadata.Version > Version.Parse(SupportedModMenuVersion))
            return;

        var types = AccessTools.GetTypesFromAssembly(modMenu.Instance.GetType().Assembly);

        BuilderType = types.First(t => t.Name == "PaginatedMenuScreenBuilder");
        TextButtonType = types.First(t => t.Name == "TextButton");
        MenuNavType = types.First(t => t.Name == "MenuScreenNavigation");

        FactoryType = types.First(t => t.Name == "ConfigEntryFactory");
        var generateEntryButton = AccessTools.Method(FactoryType, "GenerateEntryButton");

        var StringUtil = types.First(t => t.Name == "StringUtil");
        var UnCamelCase = AccessTools.Method(StringUtil, "UnCamelCase");
        
        var harmony = new Harmony("vitaxses.qol.modmenu.patch");

        harmony.Patch(UnCamelCase,
            postfix: new HarmonyMethod(AccessTools.Method(typeof(ModCompatibility), nameof(Postfix_UnCamelCase))));
        
        harmony.Patch(generateEntryButton,
            prefix: new HarmonyMethod(typeof(ModCompatibility).GetMethod(nameof(Prefix_GenerateEntryButton))));
    }

    public static void Postfix_UnCamelCase(ref string __result)
    {
        if (__result == "Qo L")
        {
            __result = "QoL";
        }
    }

    public static bool Prefix_GenerateEntryButton(object __instance, string name, BaseUnityPlugin plugin, ref object selectableElement, ref bool __result)
    {
        if (plugin.Info.Metadata.GUID != Plugin.Id)
            return true;

        var sectionEntries = plugin.Config
            .GroupBy(e => e.Value.Definition.Section)
            .Where(g => g.Any())
            .ToList();

        var rootBuilder = Activator.CreateInstance(BuilderType, name, 8);

        foreach (var section in sectionEntries)
        {
            var sectionBuilder = Activator.CreateInstance(BuilderType, /*"QoL " + */section.Key, 8);
            

            foreach (var entry in section)
            {
                var args = new object[] { entry.Value, null! };
                if ((bool)AccessTools.Method(FactoryType, "GenerateMenuElement").Invoke(__instance, args))
                    BuilderType.GetMethod("Add").Invoke(sectionBuilder, new object[] { args[1] });
            }

            var sectionMenu = BuilderType.GetMethod("Build").Invoke(sectionBuilder, null);
            var sectionButton = Activator.CreateInstance(TextButtonType, section.Key);
            TextButtonType.GetField("OnSubmit").SetValue(sectionButton, () => { MenuNavType.GetMethod("Show").Invoke(null, new object[] { sectionMenu, 0 }); });
            BuilderType.GetMethod("Add").Invoke(rootBuilder, new object[] { sectionButton });
        }

        var rootMenu = BuilderType.GetMethod("Build").Invoke(rootBuilder, null);;
        var button = Activator.CreateInstance(TextButtonType, name);
        TextButtonType.GetField("OnSubmit").SetValue(button, () => { MenuNavType.GetMethod("Show").Invoke(null, new object[] { rootMenu, 0 }); });
        selectableElement = button;

        __result = true;
        return false;
    }

}
