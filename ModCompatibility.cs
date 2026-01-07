using BepInEx;
using BepInEx.Bootstrap;

namespace QoL;

public static class ModCompatibility
{
    public const string ModMenuGuid = "org.silksong-modding.modmenu";
    public const string SupportedModMenuVersion = "0.2.0";

    public static void Init()
    {
        if (!Chainloader.PluginInfos.TryGetValue(ModMenuGuid, out PluginInfo modMenu))
        {
            return;
        }

        if (modMenu.Metadata.Version > Version.Parse(SupportedModMenuVersion))
        {
            return;
        }

        var assembly = modMenu.Instance.GetType().Assembly;
        var types = AccessTools.GetTypesFromAssembly(assembly);

        var StringUtil = types.First(t => t.FullName == "Silksong.ModMenu.Internal.StringUtil");
        var UnCamelCase = AccessTools.Method(StringUtil, "UnCamelCase");

        var harmony = new Harmony("vitaxses.qol.modmenu.patch");

        harmony.Patch(UnCamelCase,
            prefix: null, postfix: new HarmonyMethod(AccessTools.Method(typeof(ModCompatibility), nameof(Postfix_UnCamelCase))));
    }

    public static void Postfix_UnCamelCase(ref string __result)
    {
        if (__result == "Qo L")
        {
            __result = "QoL";
        }
    }
}
