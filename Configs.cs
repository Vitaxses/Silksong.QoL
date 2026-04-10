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
    public static ConfigEntry<bool> PinPogo { get; private set; } = null!;
    public static ConfigEntry<bool> OldScuttlebrace { get; private set; } = null!;
    public static ConfigEntry<bool> OldMist { get; private set; } = null!;
    public static ConfigEntry<bool> TrobbioSkip { get; private set; } = null!;
    public static ConfigEntry<bool> OldSkullTyrantLever { get; private set; } = null!;
    public static ConfigEntry<bool> RemoveFaydownNeedolinCheck { get; private set; } = null!;
    public static ConfigEntry<bool> OldPutrifiedPlanks { get; private set; } = null!;

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
        var TrackerSection = "Tracker Settings";
        var BellwaySection = "Bellway Settings";
        var VentricaSection = "Ventrica Settings";
        var NPCSection = "NPC Settings";
        var OldPatchSection = "Old Patch Settings";
        var CutsceneSection = "Cutscene Settings";
        var FastSection = "Fast Settings";
        
        FleaTracked = config.Bind(TrackerSection, "Count Fleas", false, "Counts saved fleas");
        
        FasterBellwayTravel = config.Bind(BellwaySection, "Faster Bellway Travel Animation", true, "Speeds up arrival and departure animations for Bellway travel");
        FasterBellwayBuy = config.Bind(BellwaySection, "Faster Bellway Purchase", true, "Speeds up the animation when buying Bellway stations and calls the Bell Beast afterwards");
        NoBellBeastSleep = config.Bind(BellwaySection, "BellBeast Always Awake", true, "Keeps the Bell Beast always awake");
        BellBeastFreeWill = config.Bind(BellwaySection, "BellBeast Always Ready", false, "Makes the Bell Beast always ready at your location");
        
        FasterBeastlingCall = config.Bind(BellwaySection, "Faster Beastling Call", true, "Speeds up Beastling call performance and travel");
        SkipBeastlingCallPerformance = config.Bind(BellwaySection, "Skip Beastling Call Performance", false, "Skips the Beastling call performance entirely");

        FasterVentricaTravel = config.Bind(VentricaSection, "Faster Ventrica Travel Animation", true, "Speeds up arrival and departure animations for Ventrica travel");
        FasterVentricaBuy = config.Bind(VentricaSection, "Faster Ventrica Purchase", true, "Speeds up the animation when buying Ventrica stations");

        FasterNPC = config.Bind(NPCSection, "Faster NPC", true, "Removes some introductory dialogue for NPCs");
        FasterBossLoad = config.Bind(NPCSection, "Faster Boss Start", false, "Removes first-time events for bosses");
        
        OldFloat = config.Bind(OldPatchSection, "Drifters Cloak Override", false, "Re-adds float override input (Down + Jump)");
        CloaklessClawline = config.Bind(OldPatchSection, "Cloakless Clawline", false, "Wall requiring the Drifter's Cloak in Underworks is now clingable");
        OldVoltVessels = config.Bind(OldPatchSection, "Old Volt Vessels", false, "Allows Volt Vessels to be pogoed with tools");
        BeastBoosts = config.Bind(OldPatchSection, "Beast Boosts", false, "The Beast's Crest Needle Art occasionally grants extra height");
        PinPogo = config.Bind(OldPatchSection, "Pimpillo Pogo", false, "");
        OldScuttlebrace = config.Bind(OldPatchSection, "Old Scuttlebrace", false, "Scuttlebrace Allows wall-jumping off unclingable walls");
        OldMist = config.Bind(OldPatchSection, "Old Mist", false, "Enables room juggling in the Mist");
        TrobbioSkip = config.Bind(OldPatchSection, "Trobbio Skip", false, "Allows tools to hit the slide platforms in the Whispering Vaults");
        OldSkullTyrantLever = config.Bind(OldPatchSection, "Skull Tyrant Lever Skip", false, "Allows hitting the Skull Tyrant shortcut lever through the gate");
        RemoveFaydownNeedolinCheck = config.Bind(OldPatchSection, "Faydown Cloak Without Needolin", false, "Allows getting the Faydown Cloak without the Needolin");
        OldPutrifiedPlanks = config.Bind(OldPatchSection, "Old Putrified Ducts Planks", false, "Allows certain tools to break the planks between Bilewater and Putrified Ducts");

        SkipCutscene = config.Bind(CutsceneSection, "Skip Cutscenes", true, "Skips cutscenes");
        SkipDreamCutscene = config.Bind(CutsceneSection, "Skip Dream Cutscenes", true, "Skips dream cutscenes (Needolin & First Sinner)");
        SkipDreamCutsceneFully = config.Bind(CutsceneSection, "Fully Skip Dream Scenes", false, "Skips dream scenes entirely");
        SkipTutorialToolMsg = config.Bind(CutsceneSection, "Skip Tool Pickup Tutorial", true, "Skips the first tool pickup tutorial");
        SkipWeakness = config.Bind(CutsceneSection, "Skip Weakness", true, "Removes weakness scenes");

        InstantLevers = config.Bind(FastSection, "Instant Levers", true, "Removes the delay when hitting a lever");
        InstantText = config.Bind(FastSection, "Instant Text", true, "Makes text scroll speed and popup speed instant");
        FasterLifts = config.Bind(FastSection, "Faster Lifts", true, "Makes lifts faster");
        FastUI = config.Bind(FastSection, "Fast Menu", true, "Removes the menu fade delay");
        SlowerOptions = config.Bind(FastSection, "Soften Fast Settings", false, "Makes some Fast Settings less extreme");
    }
}
