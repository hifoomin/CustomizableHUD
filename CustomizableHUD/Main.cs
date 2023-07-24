using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using UnityEngine;
using RoR2.UI;
using System.Reflection;
using RoR2;

namespace CustomizableHUD
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        public const string PluginAuthor = "HIFU";
        public const string PluginName = "CustomizableHUD";
        public const string PluginVersion = "1.2.0";

        private ConfigFile CHConfig;
        public static ManualLogSource CHLogger;

        public AssetBundle customizablehud;

        public static ConfigEntry<bool> showBuildLabel { get; set; }
        public static ConfigEntry<bool> showNotifArea { get; set; }
        public static ConfigEntry<bool> showMapName { get; set; }
        public static ConfigEntry<bool> showMapSubtitle { get; set; }
        public static ConfigEntry<bool> showHitmarker { get; set; }
        public static ConfigEntry<bool> showCrosshair { get; set; }
        public static ConfigEntry<bool> showScope { get; set; }
        public static ConfigEntry<bool> showBottomLeft { get; set; }
        public static ConfigEntry<bool> showChatBox { get; set; }
        public static ConfigEntry<bool> showHpLevelVal { get; set; }
        public static ConfigEntry<bool> showHpLevelBar { get; set; }
        public static ConfigEntry<bool> showUpperRight { get; set; }
        public static ConfigEntry<bool> showStupidWormgear { get; set; }
        public static ConfigEntry<bool> showTimerText { get; set; }
        public static ConfigEntry<bool> showTimerBg { get; set; }
        public static ConfigEntry<bool> showDiffPanel { get; set; }
        public static ConfigEntry<bool> showStage { get; set; }
        public static ConfigEntry<bool> showAmbient { get; set; }
        public static ConfigEntry<bool> showCoolWormgear { get; set; }
        public static ConfigEntry<bool> showScroller { get; set; }
        public static ConfigEntry<bool> showBackdrop { get; set; }
        public static ConfigEntry<bool> showViewport { get; set; }
        public static ConfigEntry<bool> showMarker { get; set; }
        public static ConfigEntry<bool> showOutline { get; set; }
        public static ConfigEntry<bool> showArtifact { get; set; }
        public static ConfigEntry<bool> showObjective { get; set; }
        public static ConfigEntry<bool> showArtifactBg { get; set; }
        public static ConfigEntry<bool> showObjectiveBg { get; set; }
        public static ConfigEntry<bool> showEvolutionBg { get; set; }
        public static ConfigEntry<bool> showEvolutionLabel { get; set; }
        public static ConfigEntry<bool> showSimulacrumNextWaveUI { get; set; }
        public static ConfigEntry<bool> showSimulacrumCurrentWaveUI { get; set; }
        public static ConfigEntry<bool> showSimulacrumWaveIcon { get; set; }
        public static ConfigEntry<bool> showSimulacrumWaveTitle { get; set; }
        public static ConfigEntry<bool> showSimulacrumWaveOptionalWaveInfo { get; set; }
        public static ConfigEntry<bool> showSimulacrumBackdrop { get; set; }
        public static ConfigEntry<bool> showSimulacrumOutline { get; set; }
        public static ConfigEntry<bool> showSimulacrumTitle { get; set; }
        public static ConfigEntry<bool> showViendCorruptionBackfill { get; set; }
        public static ConfigEntry<bool> showViendCorruptionColorfill { get; set; }
        public static ConfigEntry<bool> showViendCorruptionTextBackdrop { get; set; }
        public static ConfigEntry<bool> showViendCorruptionText { get; set; }
        public static ConfigEntry<float> changeViendCorruptionPosX { get; set; }
        public static ConfigEntry<float> changeViendCorruptionPosY { get; set; }

        public static ConfigEntry<bool> showBottomRight { get; set; }
        public static ConfigEntry<bool> showAltEquipBg { get; set; }
        public static ConfigEntry<bool> showAltEquipText { get; set; }
        public static ConfigEntry<bool> showEquipBg { get; set; }
        public static ConfigEntry<bool> showEquipText { get; set; }
        public static ConfigEntry<bool> showSkillText { get; set; }
        public static ConfigEntry<bool> showSprintText { get; set; }
        public static ConfigEntry<bool> showSprintIcon { get; set; }
        public static ConfigEntry<bool> showInventoryText { get; set; }
        public static ConfigEntry<bool> showInventoryIcon { get; set; }
        public static ConfigEntry<bool> showUpperLeft { get; set; }
        public static ConfigEntry<bool> showUpperLeftOutline { get; set; }
        public static ConfigEntry<bool> showMoneyBg { get; set; }
        public static ConfigEntry<bool> showMoneyIcon { get; set; }
        public static ConfigEntry<bool> showLunarBg { get; set; }
        public static ConfigEntry<bool> showLunarIcon { get; set; }
        public static ConfigEntry<bool> showBottomCenter { get; set; }
        public static ConfigEntry<bool> showSpectatorLabel { get; set; }
        public static ConfigEntry<bool> showTopCenter { get; set; }
        public static ConfigEntry<bool> showTopCenterOutline { get; set; }
        public static ConfigEntry<bool> showBossText { get; set; }
        public static ConfigEntry<bool> showBossSubtitle { get; set; }
        public static ConfigEntry<bool> showScoreboardText { get; set; }
        public static ConfigEntry<bool> showScoreboardOutlineLight { get; set; }
        public static ConfigEntry<bool> showScoreboardOutlinesDark { get; set; }
        public static ConfigEntry<bool> showScoreboardBackground { get; set; }
        public static ConfigEntry<bool> showBetteruiStupidSTUPIDBuffer { get; set; }
        public static ConfigEntry<bool> showBetteruiBackground { get; set; }

        // TRANSFORMS

        public static ConfigEntry<float> changeTopLeftPosX { get; set; }
        public static ConfigEntry<float> changeTopLeftPosY { get; set; }

        public static ConfigEntry<float> changeTopCenterPosX { get; set; }
        public static ConfigEntry<float> changeTopCenterPosY { get; set; }

        public static ConfigEntry<float> changeTopRightPosX { get; set; }
        public static ConfigEntry<float> changeTopRightPosY { get; set; }

        public static ConfigEntry<float> changeLeftCenterPosX { get; set; }
        public static ConfigEntry<float> changeLeftCenterPosY { get; set; }

        public static ConfigEntry<float> changeRightCenterPosX { get; set; }
        public static ConfigEntry<float> changeRightCenterPosY { get; set; }

        public static ConfigEntry<float> changeBottomLeftPosX { get; set; }
        public static ConfigEntry<float> changeBottomLeftPosY { get; set; }

        public static ConfigEntry<float> changeBottomCenterPosX { get; set; }
        public static ConfigEntry<float> changeBottomCenterPosY { get; set; }

        public static ConfigEntry<float> changeBottomRightPosX { get; set; }
        public static ConfigEntry<float> changeBottomRightPosY { get; set; }

        // ROTATION

        public static ConfigEntry<float> changeTopLeftRotX { get; set; }
        public static ConfigEntry<float> changeTopLeftRotY { get; set; }
        public static ConfigEntry<float> changeTopLeftRotZ { get; set; }

        public static ConfigEntry<float> changeTopCenterRotX { get; set; }
        public static ConfigEntry<float> changeTopCenterRotY { get; set; }
        public static ConfigEntry<float> changeTopCenterRotZ { get; set; }

        public static ConfigEntry<float> changeTopRightRotX { get; set; }
        public static ConfigEntry<float> changeTopRightRotY { get; set; }
        public static ConfigEntry<float> changeTopRightRotZ { get; set; }

        public static ConfigEntry<float> changeLeftCenterRotX { get; set; }
        public static ConfigEntry<float> changeLeftCenterRotY { get; set; }
        public static ConfigEntry<float> changeLeftCenterRotZ { get; set; }

        public static ConfigEntry<float> changeRightCenterRotX { get; set; }
        public static ConfigEntry<float> changeRightCenterRotY { get; set; }
        public static ConfigEntry<float> changeRightCenterRotZ { get; set; }

        public static ConfigEntry<float> changeBottomLeftRotX { get; set; }
        public static ConfigEntry<float> changeBottomLeftRotY { get; set; }
        public static ConfigEntry<float> changeBottomLeftRotZ { get; set; }

        public static ConfigEntry<float> changeBottomCenterRotX { get; set; }
        public static ConfigEntry<float> changeBottomCenterRotY { get; set; }
        public static ConfigEntry<float> changeBottomCenterRotZ { get; set; }

        public static ConfigEntry<float> changeBottomRightRotX { get; set; }
        public static ConfigEntry<float> changeBottomRightRotY { get; set; }
        public static ConfigEntry<float> changeBottomRightRotZ { get; set; }
        public static ConfigEntry<float> entireHUDalpha { get; set; }
        public static ConfigEntry<Color> entireHUDtint { get; set; }
        public static ConfigEntry<Color> fontTint { get; set; }

        public void Awake()
        {
            CHLogger = Logger;
            CHConfig = Config;

            customizablehud = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("CustomizableHUD.dll", "customizablehud"));

            showUpperLeft = Config.Bind("Top Left Toggles", "Top left", true, "Show top left holder? Vanilla is true");
            showUpperLeftOutline = Config.Bind("Top Left Toggles", "Outline", true, "Show outline? Vanilla is true");
            showMoneyBg = Config.Bind("Top Left Toggles", "Money bg", true, "Show money background? Vanilla is true");
            showMoneyIcon = Config.Bind("Top Left Toggles", "Money icon", true, "Show money icon? Vanilla is true");
            showLunarBg = Config.Bind("Top Left Toggles", "Lunar bg", true, "Show lunar background? Vanilla is true");
            showLunarIcon = Config.Bind("Top Left Toggles", "Lunar icon", true, "Show lunar icon? Vanilla is true");

            showTopCenter = Config.Bind("Top Center Toggles", "Top center", true, "Show top center holder? Vanilla is true");
            showTopCenterOutline = Config.Bind("Top Center Toggles", "Outline", true, "Show outline? Vanilla is true");
            showMapName = Config.Bind("Top Center Toggles", "Map name", true, "Show stage name on entry? Vanilla is true");
            showMapSubtitle = Config.Bind("Top Center Toggles", "Map subtitle", true, "Show stage subtitle on entry? Vanilla is true");
            showBossText = Config.Bind("Top Center Toggles", "Boss text", true, "Show boss name? Vanilla is true");
            showBossSubtitle = Config.Bind("Top Center Toggles", "Boss subtitle", true, "Show boss subtitle? Vanilla is true");

            showBuildLabel = Config.Bind("Top Right Toggles", "Build label", true, "Show game version number? Vanilla is true");
            showUpperRight = Config.Bind("Top Right Toggles", "Top right", true, "Show top right holder? Vanilla is true");
            showStupidWormgear = Config.Bind("Top Right Toggles", "Stupid wormgear", true, "Show rightmost wormgear? Vanilla is true");
            showTimerText = Config.Bind("Top Right Toggles", "Timer text", true, "Show timer value? Vanilla is true");
            showTimerBg = Config.Bind("Top Right Toggles", "Timer bg", true, "Show timer background? Vanilla is true");
            showDiffPanel = Config.Bind("Top Right Toggles", "Difficulty panel", true, "Show difficulty icon? Vanilla is true");
            showAmbient = Config.Bind("Top Right Toggles", "Stage ambient panel", true, "Show ambient level? Vanilla is true");
            showStage = Config.Bind("Top Right Toggles", "Stage", true, "Show stage number? Vanilla is true");
            showCoolWormgear = Config.Bind("Top Right Toggles", "Cool wormgear", true, "Show the based wormgear? Vanilla is true");
            showScroller = Config.Bind("Top Right Toggles", "Scroller", true, "Show difficulty bar background 1? Vanilla is true");
            showBackdrop = Config.Bind("Top Right Toggles", "Backdrop", true, "Show difficulty bar background 2? Vanilla is true");
            showViewport = Config.Bind("Top Right Toggles", "Viewport", true, "Show difficulty bars? Vanilla is true");
            showMarker = Config.Bind("Top Right Toggles", "Marker", true, "Show current difficulty marker? Vanilla is true");
            showOutline = Config.Bind("Top Right Toggles", "Outline", true, "Show difficulty holder outline? Vanilla is true");
            showArtifact = Config.Bind("Top Right Toggles", "Artifact", true, "Show artifact holder? Vanilla is true");
            showObjective = Config.Bind("Top Right Toggles", "Objective", true, "Show objective holder? Vanilla is true");
            showArtifactBg = Config.Bind("Top Right Toggles", "Artifact bg", true, "Show artifact background? Vanilla is true");
            showObjectiveBg = Config.Bind("Top Right Toggles", "Objective bg", true, "Show objective background? Vanilla is true");
            showEvolutionBg = Config.Bind("Top Right Toggles", "Evolution bg", true, "Show artifact of evolution/void fields backgrounds? Vanilla is true");
            showEvolutionLabel = Config.Bind("Top Right Toggles", "Evolution label", true, "Show artifact of evolution/void fields text? Vanilla is true");
            showBetteruiStupidSTUPIDBuffer = Config.Bind("Top Right Toggles", "BetterUI Stupid STUPID Buffer", true, "Show that stupid really STUPID (even called stupid by the dev) buffer?");
            showBetteruiBackground = Config.Bind("Top Right Toggles", "BetterUI Background", true, "Show background?");

            showHitmarker = Config.Bind("Center Toggles", "Hitmarker", true, "Show hitmarker? Vanilla is true");
            showCrosshair = Config.Bind("Center Toggles", "Crosshair", true, "Show crosshair holder? Vanilla is true");
            showSimulacrumNextWaveUI = Config.Bind("Center Toggles", "Next wave ui", true, "Show simulacrum next wave ui? Vanilla is true");
            showSimulacrumCurrentWaveUI = Config.Bind("Center Toggles", "Current wave ui", true, "Show simulacrum current wave ui? Vanilla is true");
            showSimulacrumWaveIcon = Config.Bind("Center Toggles", "Wave icon", true, "Show simulacrum wave icon? Vanilla is true");
            showSimulacrumWaveTitle = Config.Bind("Center Toggles", "Wave title", true, "Show simulacrum wave title? Vanilla is true");
            showSimulacrumWaveOptionalWaveInfo = Config.Bind("Center Toggles", "Optional wave info", true, "Show simulacrum optional wave info? Vanilla is true");
            showSimulacrumBackdrop = Config.Bind("Center Toggles", "Backdrop", true, "Show simulacrum backdrop? Vanilla is true");
            showSimulacrumOutline = Config.Bind("Center Toggles", "Outline", true, "Show simulacrum outline? Vanilla is true");
            showSimulacrumTitle = Config.Bind("Center Toggles", "Wave title", true, "Show simulacrum title? Vanilla is true");

            showViendCorruptionBackfill = Config.Bind("Center Toggles", "Corruption backdrop", true, "Show Void Fiend's corruption meter circle background? Vanilla is true");
            showViendCorruptionColorfill = Config.Bind("Center Toggles", "Corruption color fill", true, "Show Void Fiend's corruption meter circle foreground? Vanilla is true");
            showViendCorruptionTextBackdrop = Config.Bind("Center Toggles", "Corruption text backdrop", true, "Show Void Fiend's corruption meter text background? Vanilla is true");
            showViendCorruptionText = Config.Bind("Center Toggles", "Corruption text", true, "Show Void Fiend's corruption meter text? Vanilla is true");
            showScope = Config.Bind("Center Toggles", "Scope", true, "Show Railgunner's scope? Vanilla is true");

            showBottomLeft = Config.Bind("Bottom Left Toggles", "Bottom left", true, "Show bottom left holder? Vanilla is true");
            showChatBox = Config.Bind("Bottom Left Toggles", "Chatbox", true, "Show chatbox holder? Vanilla is true");
            showHpLevelVal = Config.Bind("Bottom Left Toggles", "Hp level val", true, "Show your level? Vanilla is true");
            showHpLevelBar = Config.Bind("Bottom Left Toggles", "Hp level bar", true, "Show experience bar? Vanilla is true");

            showBottomCenter = Config.Bind("Bottom Center Toggles", "Bottom Center", true, "Show bottom center holder? Vanilla is true");
            showSpectatorLabel = Config.Bind("Bottom Center Toggles", "Spectator label", true, "Show spectator text and background? Vanilla is true");

            showNotifArea = Config.Bind("Bottom Center Toggles", "Transformation notification", true, "Show transformation notifications such as corrupting or consuming an item? Vanilla is true");

            showBottomRight = Config.Bind("Bottom Right Toggles", "Bottom Right", true, "Show bottom right holder? Vanilla is true");
            showAltEquipBg = Config.Bind("Bottom Right Toggles", "Alt equip bg", true, "Show alt equipment background? Vanilla is true");
            showAltEquipText = Config.Bind("Bottom Right Toggles", "Alt equip text", true, "Show alt equipment keybind text? Vanilla is true");
            showEquipBg = Config.Bind("Bottom Right Toggles", "Equip bg", true, "Show equipment background? Vanilla is true");
            showEquipText = Config.Bind("Bottom Right Toggles", "Equip text", true, "Show equipment keybind text? Vanilla is true");
            showSkillText = Config.Bind("Bottom Right Toggles", "Skill text", true, "Show skill keybind text? Vanilla is true");
            showSprintText = Config.Bind("Bottom Right Toggles", "Sprint text", true, "Show sprint keybind text? Vanilla is true");
            showSprintIcon = Config.Bind("Bottom Right Toggles", "Sprint icon", true, "Show sprint icon? Vanilla is true");
            showInventoryText = Config.Bind("Bottom Right Toggles", "Inventory text", true, "Show inventory keybind text? Vanilla is true");
            showInventoryIcon = Config.Bind("Bottom Right Toggles", "Inventory icon", true, "Show inventory icon? Vanilla is true");

            showScoreboardText = Config.Bind("Center Toggles", "Scoreboard text", true, "Show scoreboard text? Vanilla is true");
            showScoreboardBackground = Config.Bind("Center Toggles", "Scoreboard bg", true, "Show scoreboard background? Vanilla is true");
            showScoreboardOutlineLight = Config.Bind("Center Toggles", "Scoreboard outline", true, "Show outer, light scoreboard outline? Vanilla is true");
            showScoreboardOutlinesDark = Config.Bind("Center Toggles", "Scoreboard outline 2", true, "Show inner, dark scoreboard outlines? Vanilla is true");

            changeViendCorruptionPosX = Config.Bind("Center Positions", "Corruption pos X", -0.0077f, "Void Fiend's corruption meter position on the X axis. Vanilla is -0.0077");
            changeViendCorruptionPosY = Config.Bind("Center Positions", "Corruption pos Y", -0.61f, "Void Fiend's corruption meter position on the Y axis. Vanilla is -0.61");
            ModSettingsManager.AddOption(new StepSliderOption(changeViendCorruptionPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeViendCorruptionPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");
            // TRANSFORMS

            changeTopLeftPosX = Config.Bind("Top Left Positions", "Top left pos X", -11.4084f, "Top left holder's position on the X axis. Vanilla is -11.4084");
            changeTopLeftPosY = Config.Bind("Top Left Positions", "Top left pos Y", 6.4172f, "Top left holder's position on the Y axis. Vanilla is 6.4172");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopLeftPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopLeftPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");

            changeTopCenterPosX = Config.Bind("Top Center Positions", "Top center pos X", 0f, "Top center holder's position on the X axis. Vanilla is 0");
            changeTopCenterPosY = Config.Bind("Top Center Positions", "Top center pos Y", 6.4172f, "Top center holder's position on the Y axis. Vanilla is 6.4172");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopCenterPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopCenterPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");

            changeTopRightPosX = Config.Bind("Top Right Positions", "Top right pos X", 11.4084f, "Top right holder's position on the X axis. Vanilla is 11.4084");
            changeTopRightPosY = Config.Bind("Top Right Positions", "Top right pos Y", 6.4172f, "Top right holder's position on the Y axis. Vanilla is 6.4172");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopRightPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopRightPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 3, "CH: Top Positions");

            changeLeftCenterPosX = Config.Bind("Left Center Positions", "Left center pos X", -11.4084f, "Left center holder's position on the X axis. Vanilla is -11.4084");
            changeLeftCenterPosY = Config.Bind("Left Center Positions", "Left center pos Y", 1.2835f, "Left center holder's position on the Y axis. Vanilla is 1.2835");
            ModSettingsManager.AddOption(new StepSliderOption(changeLeftCenterPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeLeftCenterPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");

            changeRightCenterPosX = Config.Bind("Right Center Positions", "Right center pos X", 6.8451f, "Right center holder's position on the X axis. Vanilla is 6.8451");
            changeRightCenterPosY = Config.Bind("Right Center Positions", "Right center pos Y", 1.2835f, "Right center holder's position on the Y axis. Vanilla is 1.2835");
            ModSettingsManager.AddOption(new StepSliderOption(changeRightCenterPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeRightCenterPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 4, "CH: Center Positions");

            changeBottomLeftPosX = Config.Bind("Bottom Left Positions", "Bottom left pos X", -11.4084f, "Bottom left holder's position on the X axis. Vanilla is -11.4084");
            changeBottomLeftPosY = Config.Bind("Bottom Left Positions", "Bottom left pos Y", -6.4172f, "Bottom left holder's position on the Y axis. Vanilla is -6.4172");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomLeftPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomLeftPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");

            changeBottomCenterPosX = Config.Bind("Bottom Center Positions", "Bottom center pos X", 0f, "Bottom center holder's position on the X axis. Vanilla is 0");
            changeBottomCenterPosY = Config.Bind("Bottom Center Positions", "Bottom center pos Y", -6.4172f, "Bottom center holder's position on the Y axis. Vanilla is -6.4172");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomCenterPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomCenterPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");

            changeBottomRightPosX = Config.Bind("Bottom Right Positions", "Bottom right pos X", 9.83f, "Bottom right holder's position on the X axis. Vanilla is 9.83");
            changeBottomRightPosY = Config.Bind("Bottom Right Positions", "Bottom right pos Y", -5.71f, "Bottom right holder's position on the Y axis. Vanilla is -5.71");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomRightPosX, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomRightPosY, new StepSliderConfig() { min = -25f, max = 25f, increment = 0.0001f }), "CH.TabID." + 5, "CH: Bottom Positions");

            // ROTATIONS

            changeTopLeftRotX = Config.Bind("Top Left Rotations", "Top left rot X", 0f, "Top left holder's rotation on the X axis. Vanilla is 0");
            changeTopLeftRotY = Config.Bind("Top Left Rotations", "Top left rot Y", 354f, "Top left holder's rotation on the Y axis. Vanilla is 354");
            changeTopLeftRotZ = Config.Bind("Top Left Rotations", "Top left rot Z", 0f, "Top left holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopLeftRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopLeftRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopLeftRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");

            changeTopCenterRotX = Config.Bind("Top Center Rotations", "Top center rot X", 0f, "Top center holder's rotation on the X axis. Vanilla is 0");
            changeTopCenterRotY = Config.Bind("Top Center Rotations", "Top center rot Y", 0f, "Top center holder's rotation on the Y axis. Vanilla is 0");
            changeTopCenterRotZ = Config.Bind("Top Center Rotations", "Top center rot Z", 0f, "Top center holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopCenterRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopCenterRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopCenterRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");

            changeTopRightRotX = Config.Bind("Top Right Rotations", "Top right rot X", 0f, "Top right holder's rotation on the X axis. Vanilla is 0");
            changeTopRightRotY = Config.Bind("Top Right Rotations", "Top right rot Y", 6f, "Top right holder's rotation on the Y axis. Vanilla is 6");
            changeTopRightRotZ = Config.Bind("Top Right Rotations", "Top right rot Z", 0f, "Top right holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopRightRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopRightRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeTopRightRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 6, "CH: Top Rotations");

            changeLeftCenterRotX = Config.Bind("Left Center Rotations", "Left center rot X", 0f, "Left center holder's rotation on the X axis. Vanilla is 0");
            changeLeftCenterRotY = Config.Bind("Left Center Rotations", "Left center rot Y", 0f, "Left center holder's rotation on the Y axis. Vanilla is 0");
            changeLeftCenterRotZ = Config.Bind("Left Center Rotations", "Left center rot Z", 0f, "Left center holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeLeftCenterRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeLeftCenterRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeLeftCenterRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");

            changeRightCenterRotX = Config.Bind("Right Center Rotations", "Right center rot X", 0f, "Right center holder's rotation on the X axis. Vanilla is 0");
            changeRightCenterRotY = Config.Bind("Right Center Rotations", "Right center rot Y", 0f, "Right center holder's rotation on the Y axis. Vanilla is 0");
            changeRightCenterRotZ = Config.Bind("Right Center Rotations", "Right center rot Z", 0f, "Right center holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeRightCenterRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeRightCenterRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeRightCenterRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 7, "CH: Center Rotations");

            changeBottomLeftRotX = Config.Bind("Bottom Left Rotations", "Bottom left rot X", 0f, "Bottom left holder's rotation on the X axis. Vanilla is 0");
            changeBottomLeftRotY = Config.Bind("Bottom Left Rotations", "Bottom left rot Y", 354f, "Bottom left holder's rotation on the Y axis. Vanilla is 354");
            changeBottomLeftRotZ = Config.Bind("Bottom Left Rotations", "Bottom left rot Z", 0f, "Bottom left holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomLeftRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomLeftRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomLeftRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");

            changeBottomCenterRotX = Config.Bind("Bottom Center Rotations", "Bottom center rot X", 0f, "Bottom center holder's rotation on the X axis. Vanilla is 0");
            changeBottomCenterRotY = Config.Bind("Bottom Center Rotations", "Bottom center rot Y", 0f, "Bottom center holder's rotation on the Y axis. Vanilla is 0");
            changeBottomCenterRotZ = Config.Bind("Bottom Center Rotations", "Bottom center rot Z", 0f, "Bottom center holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomCenterRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomCenterRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomCenterRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");

            changeBottomRightRotX = Config.Bind("Bottom Right Rotations", "Bottom right rot X", 0f, "Bottom right holder's rotation on the X axis. Vanilla is 0");
            changeBottomRightRotY = Config.Bind("Bottom Right Rotations", "Bottom right rot Y", 0f, "Bottom right holder's rotation on the Y axis. Vanilla is 0");
            changeBottomRightRotZ = Config.Bind("Bottom Right Rotations", "Bottom right rot Z", 0f, "Bottom right holder's rotation on the Z axis. Vanilla is 0");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomRightRotX, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomRightRotY, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");
            ModSettingsManager.AddOption(new StepSliderOption(changeBottomRightRotZ, new StepSliderConfig() { min = -360f, max = 360f, increment = 1 }), "CH.TabID." + 8, "CH: Bottom Rotations");

            entireHUDalpha = Config.Bind("Misc", "Transparency", 1f, "Decimal. Vanilla is 1");
            entireHUDtint = Config.Bind("Misc", "Entire HUD Tint", new Color(1f, 1f, 1f, 1f), "Wack on multiplayer - everyone must have the same colors. The tint of the HUD. Vanilla is 1, 1, 1, 1 (255, 255, 255, 255). These numbers are RGB values divided by 255.");
            fontTint = Config.Bind("Misc", "Font Tint", new Color(1f, 1f, 1f, 1f), "Wack on multiplayer - everyone must have the same colors. The tint of the font. Vanilla is 1, 1, 1, 1 (255, 255, 255, 255). These numbers are RGB values divided by 255.");
            ModSettingsManager.AddOption(new StepSliderOption(entireHUDalpha, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }), "CH.TabID." + 9, "CH: Misc");
            ModSettingsManager.AddOption(new ColorOption(entireHUDtint), "CH.TabID." + 9, "CH: Misc");
            ModSettingsManager.AddOption(new ColorOption(fontTint), "CH.TabID." + 9, "CH: Misc");

            var tabID = 0;
            foreach (ConfigEntryBase ceb in Config.GetConfigEntries())
            {
                var Name = ceb.Definition.Section;
                if (Name == "Top Left Toggles" || Name == "Top Center Toggles" || Name == "Top Right Toggles")
                {
                    tabID = 0;
                    Name = "Top Toggles";
                    //ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, Name);
                }
                if (Name == "Center Toggles")
                {
                    tabID = 1;
                    Name = "Center Toggles";
                    // ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name == "Bottom Left Toggles" || Name == "Bottom Center Toggles" || Name == "Bottom Right Toggles")
                {
                    tabID = 2;
                    Name = "Bottom Toggles";
                    // ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                /*
                if (Name == "Top Left Positions" || Name == "Top Center Positions" || Name == "Top Right Positions")
                {
                    tabID = 3;
                    name = "Top Positions";
                }
                if (Name == "Left Center Positions" || Name == "Center Positions" || Name == "Right Center Positions")
                {
                    tabID = 4;
                    name = "Center Positions";
                }
                if (Name == "Bottom Left Positions" || Name == "Bottom Center Positions" || Name == "Bottom Right Positions")
                {
                    tabID = 5;
                    name = "Bottom Positions";
                }
                if (Name == "Top Left Rotations" || Name == "Top Center Rotations" || Name == "Top Right Rotations")
                {
                    tabID = 6;
                    name = "Top Rotations";
                }
                if (Name == "Left Center Rotations" || Name == "Center Rotations" || Name == "Right Center Rotations")
                {
                    tabID = 7;
                    name = "Center Rotations";
                }
                if (Name == "Bottom Left Rotations" || Name == "Bottom Center Rotations" || Name == "Bottom Right Rotations")
                {
                    tabID = 8;
                    name = "Bottom Rotations";
                }
                */
                if (ceb.DefaultValue.GetType() == typeof(bool))
                {
                    ModSettingsManager.AddOption(new CheckBoxOption((ConfigEntry<bool>)ceb, new CheckBoxConfig()), "CH.TabID." + tabID, "CH: " + Name);
                }
                /*
                if (ceb.DefaultValue.GetType() == typeof(float))
                {
                    ModSettingsManager.AddOption(new StepSliderOption((ConfigEntry<float>)ceb, new StepSliderConfig() { min = -360f, max = 360f, increment = 0.000001f }), "CH.TabID." + tabID, "CH: " + Name);
                }
                */

                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Toggle.png"), "CH.TabID." + 0, "CH: Top Toggles");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Toggle.png"), "CH.TabID." + 1, "CH: Center Toggles");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Toggle.png"), "CH.TabID." + 2, "CH: Bottom Toggles");

                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Move.png"), "CH.TabID." + 3, "CH: Top Positions");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Move.png"), "CH.TabID." + 4, "CH: Center Positions");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Move.png"), "CH.TabID." + 5, "CH: Bottom Positions");

                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Rotate.png"), "CH.TabID." + 6, "CH: Top Rotations");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Rotate.png"), "CH.TabID." + 7, "CH: Center Rotations");
                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("Rotate.png"), "CH.TabID." + 8, "CH: Bottom Rotations");

                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("CustomizableHUD.png"), "CH.TabID." + 9, "CH: Misc");
            }
            On.RoR2.UI.HUD.Awake += HUD_Awake;
            On.RoR2.UI.HUD.Update += HUD_Update;
        }

        private void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, HUD self)
        {
            orig(self);
            // Main.CHLogger.LogError("awake has run: " + hasRun);
            hasRun = false;
        }

        public static bool hasRun = false;
        public static HUD hud;

        private void HUD_Update(On.RoR2.UI.HUD.orig_Update orig, HUD self)
        {
            orig(self);
            if (!hasRun)
            {
                // Main.CHLogger.LogError("update pre set has run: " + hasRun);
                self.gameObject.AddComponent<HUDControllerComponent>();
                if (Run.instance && Run.instance is InfiniteTowerRun)
                {
                    self.gameObject.GetComponent<HUDControllerComponent>().isSimulacrum = true;
                }

                if (self.targetBodyObject && self.targetBodyObject.name == "VoidSurvivorBody(Clone)")
                {
                    self.gameObject.GetComponent<HUDControllerComponent>().isViend = true;
                }
                hud = self;

                hasRun = true;
                // Main.CHLogger.LogError("update POST set has run: " + hasRun);
            }
        }
    }
}