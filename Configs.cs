using BepInEx.Configuration;

namespace QoL;

public static class Configs
{
    public static ConfigEntry<bool> FleaTracked { get; private set; } = null!;

    public static ConfigEntry<bool> FasterBellwayTravel { get; private set; } = null!;
    public static ConfigEntry<bool> FasterBellwayBuy { get; private set; } = null!;
    public static ConfigEntry<bool> NoBellBeastSleep { get; private set; } = null!;
    public static ConfigEntry<bool> BellBeastFreeWill { get; private set; } = null!;
    public static ConfigEntry<bool> FasterBeastlingCall { get; private set; } = null!;
    public static ConfigEntry<bool> SkipBeastlingCallPerformance { get; private set; } = null!;

    public static ConfigEntry<bool> FasterVentricaTravel { get; private set; } = null!;
    public static ConfigEntry<bool> FasterVentricaBuy { get; private set; } = null!;

    public static ConfigEntry<bool> FasterNPC { get; private set; } = null!;
    public static ConfigEntry<bool> FasterBossLoad { get; private set; } = null!;

    public static ConfigEntry<bool> OldFloat { get; private set; } = null!;
    public static ConfigEntry<bool> CloaklessClawline { get; private set; } = null!;
    public static ConfigEntry<bool> OldVoltVessels { get; private set; } = null!;
    public static ConfigEntry<bool> BeastBoosts { get; private set; } = null!;
    public static ConfigEntry<bool> OldScuttlebrace { get; private set; } = null!;
    public static ConfigEntry<bool> OldMist { get; private set; } = null!;
    public static ConfigEntry<bool> TrobbioSkip { get; private set; } = null!;

    public static ConfigEntry<bool> SkipCutscene { get; private set; } = null!;
    public static ConfigEntry<bool> SkipDreamCutscene { get; private set; } = null!;
    public static ConfigEntry<bool> SkipDreamCutsceneFully { get; private set; } = null!;
    public static ConfigEntry<bool> SkipTutorialToolMsg { get; private set; } = null!;
    public static ConfigEntry<bool> SkipWeakness { get; private set; } = null!;

    public static ConfigEntry<bool> InstantLevers { get; private set; } = null!;
    public static ConfigEntry<bool> FasterLifts { get; private set; } = null!;
    public static ConfigEntry<bool> InstantText { get; private set; } = null!;
    public static ConfigEntry<bool> FastUI { get; private set; } = null!;
    public static ConfigEntry<bool> SlowerOptions { get; private set; } = null!;

    internal static void Bind(ConfigFile config)
    {
        FleaTracked = config.Bind("Tracker Settings", "Count Fleas", false, "Counts saved fleas.");

        FasterBellwayTravel = config.Bind("Bellway Settings", "Faster Bellway Travel Animation", true, "Speeds up arrival and departure animations for Bellway travel.");
        FasterBellwayBuy = config.Bind("Bellway Settings", "Faster Bellway Purchase", true, "Speeds up the animation when buying Bellway stations and calls the Bell Beast afterwards.");
        NoBellBeastSleep = config.Bind("Bellway Settings", "BellBeast Always Awake", true, "Keeps the Bell Beast always awake.");
        BellBeastFreeWill = config.Bind("Bellway Settings", "BellBeast Has Free Will", false, "Makes the Bell Beast always ready at your location.");
        
        FasterBeastlingCall = config.Bind("Bellway Settings", "Faster Beastling Call", true, "Speeds up Beastling call performance and travel.");
        SkipBeastlingCallPerformance = config.Bind("Bellway Settings", "Skip Beastling Call Performance", false, "Skips the Beastling call performance entirely.");

        FasterVentricaTravel = config.Bind("Ventrica Settings", "Faster Ventrica Travel Animation", true, "Speeds up arrival and departure animations for Ventrica travel.");
        FasterVentricaBuy = config.Bind("Ventrica Settings", "Faster Ventrica Purchase", true, "Speeds up the animation when buying Ventrica stations.");

        FasterNPC = config.Bind("NPC Settings", "Faster NPC", true, "Removes some introductory dialogue for NPCs.");
        FasterBossLoad = config.Bind("NPC Settings", "Faster Boss Start", false, "Removes first-time events for bosses.");
        
        OldFloat = config.Bind("Old Patch Settings", "Drifters Cloak Override", false, "Re-adds float override input (Down + Jump).");
        CloaklessClawline = config.Bind("Old Patch Settings", "Cloakless Clawline", false, "Wall requiring the Drifter's Cloak in Underworks is now clingable.");
        OldVoltVessels = config.Bind("Old Patch Settings", "Old Volt Vessels", false, "Allows Volt Vessels to be pogoed with tools and break the one-way floor in the Putrified Ducts.");
        BeastBoosts = config.Bind("Old Patch Settings", "Beast Boosts", false, "The Beast's Crest Needle Art occasionally grants extra height.");
        OldScuttlebrace = config.Bind("Old Patch Settings", "Old Scuttlebrace", false, "Scuttlebrace Allows wall-jumping off unclingable walls.");
        OldMist = config.Bind("Old Patch Settings", "Old Mist", false, "Enables room juggling in the Mist.");
        TrobbioSkip = config.Bind("Old Patch Settings", "Trobbio Skip", false, "Allows tools to hit the slide platforms in the Whispering Vaults.");

        SkipCutscene = config.Bind("Cutscene Settings", "Skip Cutscenes Faster", true, "Skips cutscenes faster.");
        SkipDreamCutscene = config.Bind("Cutscene Settings", "Skip Dream Cutscenes", true, "Skips dream cutscenes (Needolin & First Sinner).");
        SkipDreamCutsceneFully = config.Bind("Cutscene Settings", "Fully Skip Dream Scenes", false, "Skips dream scenes entirely.");
        SkipTutorialToolMsg = config.Bind("Cutscene Settings", "Skip Tool Pickup Tutorial", true, "Skips the first tool pickup tutorial.");
        SkipWeakness = config.Bind("Cutscene Settings", "Skip Weakness", true, "Removes weakness scenes in Moss Grotto and the Cradle.");

        InstantLevers = config.Bind("Fast Settings", "Instant Levers", true, "Removes the delay when hitting a lever.");
        InstantText = config.Bind("Fast Settings", "Instant Text", true, "Makes text scroll speed and popup speed instant.");
        FasterLifts = config.Bind("Fast Settings", "Faster Lifts", true, "Makes lifts faster.");
        FastUI = config.Bind("Fast Settings", "Fast Menu", true, "Removes the menu fade delay.");
        SlowerOptions = config.Bind("Fast Settings", "Soften Fast Settings", false, "Makes some Fast Settings less extreme.");
    }
}
