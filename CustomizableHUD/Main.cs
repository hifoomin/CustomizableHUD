#nullable enable

using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using RoR2;
using UnityEngine;
using UnityEngine.UI;
using RoR2.UI;
using System.Reflection;

namespace CustomizableHUD
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        public const string PluginAuthor = "HIFU";
        public const string PluginName = "CustomizableHUD";
        public const string PluginVersion = "1.1.0";

        private ConfigFile CHConfig;
        public static ManualLogSource CHLogger;

        private string version = PluginVersion;
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

            entireHUDalpha = Config.Bind("Entire HUD", "Transparency", 1f, "Decimal. Vanilla is 1");
            ModSettingsManager.AddOption(new StepSliderOption(entireHUDalpha, new StepSliderConfig() { min = 0f, max = 1f, increment = 0.01f }), "CH.TabID." + 9, "CH: Entire HUD");

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

                ModSettingsManager.SetModIcon(customizablehud.LoadAsset<Sprite>("CustomizableHUD.png"), "CH.TabID." + 9, "CH: Entire HUD");
            }
            On.RoR2.UI.HUD.Awake += HUD_Awake;
        }

        public static HUD hud;

        // hud here?

        private void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, HUD self)
        {
            orig(self);
            self.gameObject.AddComponent<HUDControllerComponent>();
            hud = self;
        }
    }

    public class HUDControllerComponent : MonoBehaviour
    {
        public HUD hud;

        private Transform mainContainer;

        private GameObject buildLabel;
        private GameObject scopeContainer;

        private GameObject notifArea;

        private GameObject mapNameCluster;

        private GameObject mapName;
        private GameObject mapSubtitle;

        private GameObject mainUIArea;

        private GameObject springCanvas;

        private GameObject leftCenter;
        private GameObject rightCenter;
        private GameObject bottomLeft;
        private GameObject upperRight;
        private GameObject upperRightReal;
        private GameObject bottomRight;

        private GameObject bottomCenter;
        private GameObject topCenter;

        private GameObject upperLeft;

        private Image hitmarker;
        private GameObject crosshair;

        private GameObject chatBox;
        private GameObject levelDisplayCluster;
        private GameObject hpLevelVal;
        private GameObject hpLevelBar;

        private GameObject timerPanel;

        private GameObject stupidWormgear;
        private GameObject timerText;
        private Image timerBg;
        private GameObject diffPanel;

        private GameObject stageAmbientPanel;

        private GameObject stage;
        private GameObject ambient;

        private GameObject diffBar;

        private GameObject coolWormgear;
        private Image scroller;
        private GameObject backdrop;
        private GameObject viewport;
        private GameObject marker;
        private GameObject outline;

        private GameObject objectiveArtifact;

        private GameObject artifact;
        private GameObject objective;
        private Image artifactBg;
        private Image objectiveBg;
        private Image evolutionBg;
        private GameObject evolutionLabel;

        private GameObject altEquipRoot;

        private GameObject altEquipBg;
        private GameObject altEquipText;

        private GameObject equipRoot;

        private GameObject equipBg;
        private GameObject equipText;

        private GameObject primaryRoot;

        private GameObject primaryText;

        private GameObject secondaryRoot;

        private GameObject secondaryText;

        private GameObject utilityRoot;

        private GameObject utilityText;

        private GameObject specialRoot;

        private GameObject specialText;

        private GameObject sprintCluster;

        private GameObject sprintText;
        private GameObject sprintIcon;

        private GameObject inventoryCluster;

        private GameObject inventoryText;
        private GameObject inventoryIcon;

        private Image upperLeftOutline;

        private GameObject moneyRoot;

        private GameObject moneyBg;
        private GameObject moneyIcon;

        private GameObject lunarRoot;

        private GameObject lunarBg;
        private GameObject lunarIcon;

        private GameObject spectatorLabel;

        private Image topCenterOutline;

        private GameObject bossRoot;
        private GameObject bossLabel;
        private GameObject bossSubtitle;

        private GameObject scoreboard;
        private GameObject scoreboardContainer;
        private GameObject scoreboardPlayerText;
        private GameObject scoreboardItemText;
        private GameObject scoreboardEquipmentText;
        private Image? scoreboardBackground;
        private Image? scoreboardOutlineLight;
        private Image? scoreboardOutlineDarkPlayer;
        private Image? scoreboardOutlineDarkItem;
        private Image? scoreboardOutlineDarkEquipment;

        private GameObject crosshairExtras;
        private GameObject viendCorruption;
        private GameObject viendFillRoot;
        private GameObject viendFill;
        private Image viendCorruptionTextBackdrop;
        private GameObject viendCorruptionText;
        private GameObject viendCorruptionFillBackdrop;
        private Image viendCorruptionFill;

        private GameObject? simulacrumNextWaveUI;
        private GameObject? simulacrumCurrentWaveUI;
        private GameObject? simulacrumWaveIcon;
        private GameObject? simulacrumVerticalLayout;
        private GameObject? simulacrumWaveTitle;
        private GameObject? simulacrumWaveOptionalWaveInfo;
        private GameObject? simulacrumBackdrop;
        private GameObject? simulacrumTitle;
        private GameObject? simulacrumOutline;

        private Vector3 topLeftV = new(0f, 0f, 0f);
        private Vector3 topCenterV = new(0f, 0f, 0f);
        private Vector3 topRightV = new(0f, 0f, 0f);

        private Vector3 leftCenterV = new(0f, 0f, 0f);
        private Vector3 rightCenterV = new(0f, 0f, 0f);
        private Vector3 bottomLeftV = new(0f, 0f, 0f);

        private Vector3 bottomCenterV = new(0f, 0f, 0f);
        private Vector3 bottomRightV = new(0f, 0f, 0f);

        private Vector3 topLeftRotV = new(0f, 0f, 0f);
        private Vector3 topCenterRotV = new(0f, 0f, 0f);
        private Vector3 topRightRotV = new(0f, 0f, 0f);

        private Vector3 leftCenterRotV = new(0f, 0f, 0f);
        private Vector3 rightCenterRotV = new(0f, 0f, 0f);

        private Vector3 bottomLeftRotV = new(0f, 0f, 0f);
        private Vector3 bottomCenterRotV = new(0f, 0f, 0f);
        private Vector3 bottomRightRotV = new(0f, 0f, 0f);

        private float timer;
        private float interval = 0.1f;

        private CanvasGroup canvasGroup;

        private void OnEnable()
        {
            InstanceTracker.Add(this);
        }

        private void OnDisable()
        {
            InstanceTracker.Remove(this);
        }

        private void Start()
        {
            hud = Main.hud;
            mainContainer = hud.mainContainer.transform;
            canvasGroup = hud.gameObject.AddComponent<CanvasGroup>();

            buildLabel = mainContainer.GetChild(0).gameObject;
            scopeContainer = mainContainer.GetChild(2).gameObject;

            notifArea = mainContainer.GetChild(4).gameObject;

            mapNameCluster = mainContainer.GetChild(3).gameObject;

            mapName = mapNameCluster.transform.GetChild(0).gameObject;
            mapSubtitle = mapNameCluster.transform.GetChild(1).gameObject;

            mainUIArea = mainContainer.GetChild(7).gameObject;
            springCanvas = mainUIArea.transform.GetChild(2).gameObject;
            bottomLeft = springCanvas.transform.GetChild(0).gameObject;
            leftCenter = springCanvas.transform.GetChild(6).gameObject;
            rightCenter = springCanvas.transform.GetChild(7).gameObject;
            upperRightReal = springCanvas.transform.GetChild(1).gameObject;
            upperRight = upperRightReal.transform.GetChild(0).gameObject;
            bottomRight = springCanvas.transform.GetChild(2).GetChild(0).gameObject;
            upperLeft = springCanvas.transform.GetChild(3).gameObject;
            bottomCenter = springCanvas.transform.GetChild(4).gameObject;
            topCenter = springCanvas.transform.GetChild(5).gameObject;

            hitmarker = mainUIArea.transform.GetChild(0).GetComponent<Image>();
            crosshair = mainUIArea.transform.GetChild(1).gameObject;

            crosshairExtras = crosshair.transform.GetChild(0).gameObject;

            /*
            viendCorruption = crosshairExtras.transform.GetChild(0).gameObject;
            viendFillRoot = viendCorruption.transform.GetChild(0).gameObject;
            viendCorruptionTextBackdrop = viendFillRoot.transform.GetChild(0).GetComponent<Image>();
            viendCorruptionText = viendCorruptionFillBackdrop.transform.GetChild(0).gameObject;
            viendFill = viendFillRoot.transform.GetChild(1).gameObject;
            viendCorruptionFillBackdrop = viendFill.transform.GetChild(0).gameObject;
            viendCorruptionFill = viendFill.transform.GetChild(1).GetComponent<Image>();
            */

            chatBox = bottomLeft.transform.GetChild(0).gameObject;
            levelDisplayCluster = bottomLeft.transform.GetChild(1).GetChild(0).gameObject;
            hpLevelVal = levelDisplayCluster.transform.GetChild(1).gameObject;
            hpLevelBar = levelDisplayCluster.transform.GetChild(2).gameObject;

            timerPanel = upperRight.transform.GetChild(0).gameObject;

            stupidWormgear = timerPanel.transform.GetChild(0).gameObject;
            timerText = timerPanel.transform.GetChild(1).gameObject;
            timerBg = timerPanel.GetComponent<Image>();
            diffPanel = upperRight.transform.GetChild(1).gameObject;

            stageAmbientPanel = upperRight.transform.GetChild(2).gameObject;

            stage = stageAmbientPanel.transform.GetChild(0).gameObject;
            ambient = stageAmbientPanel.transform.GetChild(1).gameObject;

            diffBar = upperRight.transform.GetChild(3).gameObject;

            coolWormgear = diffBar.transform.GetChild(0).gameObject;
            scroller = diffBar.transform.GetChild(1).GetComponent<Image>();
            backdrop = scroller.transform.GetChild(0).gameObject;
            viewport = scroller.transform.GetChild(1).gameObject;
            marker = diffBar.transform.GetChild(3).gameObject;

            outline = upperRight.transform.GetChild(4).gameObject;

            objectiveArtifact = upperRight.transform.GetChild(5).gameObject;

            artifact = objectiveArtifact.transform.GetChild(0).gameObject;
            objective = objectiveArtifact.transform.GetChild(1).gameObject;
            artifactBg = objectiveArtifact.transform.GetChild(0).GetComponent<Image>();
            objectiveBg = objectiveArtifact.transform.GetChild(1).GetComponent<Image>();

            altEquipRoot = bottomRight.transform.GetChild(1).GetChild(0).gameObject;

            altEquipBg = altEquipRoot.transform.GetChild(1).gameObject;
            altEquipText = altEquipRoot.transform.GetChild(4).gameObject;

            equipRoot = bottomRight.transform.GetChild(2).GetChild(1).gameObject;

            equipBg = equipRoot.transform.GetChild(1).gameObject;
            equipText = equipRoot.transform.GetChild(6).gameObject;

            primaryRoot = bottomRight.transform.GetChild(3).gameObject;

            primaryText = primaryRoot.transform.GetChild(5).gameObject;

            secondaryRoot = bottomRight.transform.GetChild(4).gameObject;

            secondaryText = secondaryRoot.transform.GetChild(5).gameObject;

            utilityRoot = bottomRight.transform.GetChild(5).gameObject;

            utilityText = utilityRoot.transform.GetChild(5).gameObject;

            specialRoot = bottomRight.transform.GetChild(6).gameObject;

            specialText = specialRoot.transform.GetChild(5).gameObject;

            sprintCluster = bottomRight.transform.GetChild(7).gameObject;

            sprintText = sprintCluster.transform.GetChild(1).gameObject;
            sprintIcon = sprintCluster.transform.GetChild(3).gameObject;

            inventoryCluster = bottomRight.transform.GetChild(8).gameObject;

            inventoryText = inventoryCluster.transform.GetChild(0).gameObject;
            inventoryIcon = inventoryCluster.transform.GetChild(2).gameObject;

            upperLeftOutline = upperLeft.GetComponent<Image>();

            moneyRoot = upperLeft.transform.GetChild(0).gameObject;

            moneyBg = moneyRoot.transform.GetChild(0).gameObject;
            moneyIcon = moneyRoot.transform.GetChild(4).gameObject;

            lunarRoot = upperLeft.transform.GetChild(2).gameObject;

            lunarBg = lunarRoot.transform.GetChild(0).gameObject;
            lunarIcon = lunarRoot.transform.GetChild(4).gameObject;

            spectatorLabel = bottomCenter.transform.GetChild(0).gameObject;

            topCenterOutline = topCenter.transform.GetChild(0).GetChild(0).GetComponent<Image>();

            bossRoot = topCenter.transform.GetChild(1).GetChild(0).gameObject;
            bossLabel = bossRoot.transform.GetChild(1).gameObject;
            bossSubtitle = bossRoot.transform.GetChild(2).gameObject;

            scoreboard = springCanvas.transform.GetChild(8).gameObject;
            scoreboardContainer = scoreboard.transform.GetChild(0).gameObject;
            scoreboardPlayerText = scoreboardContainer.transform.GetChild(0).gameObject;
            scoreboardItemText = scoreboardContainer.transform.GetChild(1).gameObject;
            scoreboardEquipmentText = scoreboardContainer.transform.GetChild(2).gameObject;

            /* NRE FIX LATER SOMEHOW
                scoreboardOutlineLight = scoreboardContainer.transform.GetChild(3).GetComponent<Image>();
                scoreboardBackground = scoreboardOutlineLight.transform.GetChild(0).GetChild(0).GetComponent<Image>();
                scoreboardOutlineDarkItem = scoreboardBackground.transform.GetChild(5).GetComponent<Image>();
                scoreboardOutlineDarkEquipment = scoreboardBackground.transform.GetChild(6).GetComponent<Image>();
                scoreboardOutlineDarkPlayer = scoreboardBackground.transform.GetChild(1).GetComponent<Image>();

                simulacrumNextWaveUI = crosshairExtras.transform.GetChild(1).gameObject;
                simulacrumCurrentWaveUI = crosshairExtras.transform.GetChild(2).gameObject;
                simulacrumWaveIcon = simulacrumCurrentWaveUI.transform.GetChild(0).GetChild(0).gameObject;
                simulacrumVerticalLayout = simulacrumCurrentWaveUI.transform.GetChild(0).GetChild(1).gameObject;
                simulacrumWaveTitle = simulacrumVerticalLayout.transform.GetChild(0).gameObject;
                simulacrumWaveOptionalWaveInfo = simulacrumVerticalLayout.transform.GetChild(1).gameObject;
                simulacrumBackdrop = simulacrumNextWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                simulacrumTitle = simulacrumNextWaveUI.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
                simulacrumOutline = simulacrumCurrentWaveUI.transform.GetChild(0).GetChild(2).gameObject;
            */

            topLeftV.z = 12.6537f;
            topCenterV.z = 12.6537f;
            topRightV.z = 12.6537f;

            leftCenterV.z = 12.6537f;
            rightCenterV.z = 12.6537f;

            bottomLeftV.z = 12.6537f;
            bottomCenterV.z = 12.6537f;
            bottomRightV.z = 12.6537f;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > interval)
            {
                canvasGroup.alpha = Main.entireHUDalpha.Value;
                // POSITIONS

                topLeftV.x = Main.changeTopLeftPosX.Value;
                topLeftV.y = Main.changeTopLeftPosY.Value;
                topCenterV.x = Main.changeTopCenterPosX.Value;
                topCenterV.y = Main.changeTopCenterPosY.Value;
                topRightV.x = Main.changeTopRightPosX.Value;
                topRightV.y = Main.changeTopRightPosY.Value;

                leftCenterV.x = Main.changeLeftCenterPosX.Value;
                leftCenterV.y = Main.changeLeftCenterPosY.Value;
                rightCenterV.x = Main.changeRightCenterPosX.Value;
                rightCenterV.y = Main.changeRightCenterPosY.Value;

                bottomLeftV.x = Main.changeBottomLeftPosX.Value;
                bottomLeftV.y = Main.changeBottomLeftPosY.Value;
                bottomCenterV.x = Main.changeBottomCenterPosX.Value;
                bottomCenterV.y = Main.changeBottomCenterPosY.Value;
                bottomRightV.x = Main.changeBottomRightPosX.Value;
                bottomRightV.y = Main.changeBottomRightPosY.Value;

                // ROTATIONS

                topLeftRotV.x = Main.changeTopLeftRotX.Value;
                topLeftRotV.y = Main.changeTopLeftRotY.Value;
                topLeftRotV.z = Main.changeTopLeftRotZ.Value;

                topCenterRotV.x = Main.changeTopCenterRotX.Value;
                topCenterRotV.y = Main.changeTopCenterRotY.Value;
                topCenterRotV.z = Main.changeTopCenterRotZ.Value;

                topRightRotV.x = Main.changeTopRightRotX.Value;
                topRightRotV.y = Main.changeTopRightRotY.Value;
                topRightRotV.z = Main.changeTopRightRotZ.Value;

                leftCenterRotV.x = Main.changeLeftCenterRotX.Value;
                leftCenterRotV.y = Main.changeLeftCenterRotY.Value;
                leftCenterRotV.z = Main.changeLeftCenterRotZ.Value;

                rightCenterRotV.x = Main.changeRightCenterRotX.Value;
                rightCenterRotV.y = Main.changeRightCenterRotY.Value;
                rightCenterRotV.z = Main.changeRightCenterRotZ.Value;

                bottomLeftRotV.x = Main.changeBottomLeftRotX.Value;
                bottomLeftRotV.y = Main.changeBottomLeftRotY.Value;
                bottomLeftRotV.z = Main.changeBottomLeftRotZ.Value;

                bottomCenterRotV.x = Main.changeBottomCenterRotX.Value;
                bottomCenterRotV.y = Main.changeBottomCenterRotY.Value;
                bottomCenterRotV.z = Main.changeBottomCenterRotZ.Value;

                bottomRightRotV.x = Main.changeBottomRightRotX.Value;
                bottomRightRotV.y = Main.changeBottomRightRotY.Value;
                bottomRightRotV.z = Main.changeBottomRightRotZ.Value;

                // POSITIONS

                upperLeft.transform.position = topLeftV;
                topCenter.transform.position = topCenterV;
                upperRightReal.transform.position = topRightV;

                leftCenter.transform.position = leftCenterV;
                rightCenter.transform.position = rightCenterV;

                bottomLeft.transform.position = bottomLeftV;
                bottomCenter.transform.position = bottomCenterV;
                bottomRight.transform.position = bottomRightV;

                // ROTATIONS

                upperLeft.transform.localEulerAngles = topLeftRotV;
                topCenter.transform.localEulerAngles = topCenterRotV;
                upperRightReal.transform.localEulerAngles = topRightRotV;

                leftCenter.transform.localEulerAngles = leftCenterRotV;
                rightCenter.transform.localEulerAngles = rightCenterRotV;

                bottomLeft.transform.localEulerAngles = bottomLeftRotV;
                bottomCenter.transform.localEulerAngles = bottomCenterRotV;
                bottomRight.transform.localEulerAngles = bottomRightRotV;

                // if (GameModeCatalog.FindGameModePrefabComponent("InfiniteTowerRun"))

                buildLabel.SetActive(Main.showBuildLabel.Value);

                scopeContainer.SetActive(Main.showScope.Value);

                notifArea.SetActive(Main.showNotifArea.Value);

                mapName.SetActive(Main.showMapName.Value);
                mapSubtitle.SetActive(Main.showMapSubtitle.Value);

                bottomLeft.SetActive(Main.showBottomLeft.Value);

                hitmarker.enabled = Main.showHitmarker.Value;
                crosshair.SetActive(Main.showCrosshair.Value);

                chatBox.SetActive(Main.showChatBox.Value);
                hpLevelVal.SetActive(Main.showHpLevelVal.Value);
                hpLevelBar.SetActive(Main.showHpLevelBar.Value);

                upperRight.SetActive(Main.showUpperRight.Value);

                stupidWormgear.SetActive(Main.showStupidWormgear.Value);
                timerText.SetActive(Main.showTimerText.Value);
                timerBg.enabled = Main.showTimerBg.Value;

                stage.SetActive(Main.showStage.Value);
                ambient.SetActive(Main.showAmbient.Value);

                diffBar.SetActive(Main.showDiffPanel.Value);

                coolWormgear.SetActive(Main.showCoolWormgear.Value);
                scroller.enabled = Main.showScroller.Value;
                backdrop.SetActive(Main.showBackdrop.Value);
                viewport.SetActive(Main.showViewport.Value);
                marker.SetActive(Main.showMarker.Value);

                outline.SetActive(Main.showOutline.Value);

                artifact.SetActive(Main.showArtifact.Value);
                objective.SetActive(Main.showObjective.Value);
                artifactBg.enabled = Main.showArtifactBg.Value;
                objectiveBg.enabled = Main.showObjectiveBg.Value;

                bottomRight.SetActive(Main.showBottomRight.Value);

                altEquipBg.SetActive(Main.showAltEquipBg.Value);
                altEquipText.SetActive(Main.showAltEquipText.Value);

                equipBg.SetActive(Main.showEquipBg.Value);
                equipText.SetActive(Main.showEquipText.Value);

                primaryText.SetActive(Main.showSkillText.Value);

                secondaryText.SetActive(Main.showSkillText.Value);

                utilityText.SetActive(Main.showSkillText.Value);

                specialText.SetActive(Main.showSkillText.Value);

                sprintText.SetActive(Main.showSprintText.Value);
                sprintIcon.SetActive(Main.showSprintIcon.Value);

                inventoryText.SetActive(Main.showInventoryText.Value);
                inventoryIcon.SetActive(Main.showInventoryIcon.Value);

                upperLeft.SetActive(Main.showUpperLeft.Value);
                upperLeftOutline.enabled = Main.showUpperLeftOutline.Value;

                moneyBg.SetActive(Main.showMoneyBg.Value);
                moneyIcon.SetActive(Main.showMoneyIcon.Value);

                lunarBg.SetActive(Main.showLunarBg.Value);
                lunarIcon.SetActive(Main.showLunarIcon.Value);

                bottomCenter.SetActive(Main.showBottomCenter.Value);

                spectatorLabel.SetActive(Main.showSpectatorLabel.Value);

                topCenter.SetActive(Main.showTopCenter.Value);
                topCenterOutline.enabled = Main.showTopCenterOutline.Value;

                bossLabel.SetActive(Main.showBossText.Value);
                bossSubtitle.SetActive(Main.showBossSubtitle.Value);

                /*
                scoreboardEquipmentText.SetActive(Main.showScoreboardText.Value);
                scoreboardItemText.SetActive(Main.showScoreboardText.Value);
                scoreboardPlayerText.SetActive(Main.showScoreboardText.Value);
                scoreboardBackground.enabled = Main.showScoreboardBackground.Value;
                scoreboardOutlineLight.enabled = Main.showScoreboardOutlineLight.Value;
                scoreboardOutlineDarkEquipment.enabled = Main.showScoreboardOutlinesDark.Value;
                scoreboardOutlineDarkItem.enabled = Main.showScoreboardOutlinesDark.Value;
                scoreboardOutlineDarkPlayer.enabled = Main.showScoreboardOutlinesDark.Value;
                */

                try
                {
                    if (objectiveArtifact.transform.GetChild(2).name == "EnemyInfoPanel(Clone)")
                    {
                        evolutionBg = objectiveArtifact.transform.GetChild(2).GetComponent<Image>();
                        // enemyinfopanel result
                        var innerFrame = evolutionBg.transform.GetChild(0).GetComponent<Image>();
                        var monsterBodiesLabel = innerFrame.transform.GetChild(0).GetChild(0).gameObject;
                        var monsterBodiesBg = innerFrame.transform.GetChild(0).GetChild(1).GetComponent<Image>();

                        evolutionLabel = innerFrame.transform.GetChild(1).GetChild(0).gameObject;
                        var inventoryDisplay = innerFrame.transform.GetChild(1).GetChild(1).GetComponent<Image>();

                        monsterBodiesBg.enabled = Main.showEvolutionBg.Value;
                        monsterBodiesLabel.SetActive(Main.showEvolutionLabel.Value);
                        inventoryDisplay.enabled = Main.showEvolutionBg.Value;
                        innerFrame.enabled = Main.showEvolutionBg.Value;
                        evolutionBg.enabled = Main.showEvolutionBg.Value;
                        evolutionLabel.SetActive(Main.showEvolutionLabel.Value);
                    }
                    simulacrumNextWaveUI.SetActive(Main.showSimulacrumNextWaveUI.Value);
                    simulacrumCurrentWaveUI.SetActive(Main.showSimulacrumCurrentWaveUI.Value);
                    simulacrumWaveIcon.SetActive(Main.showSimulacrumWaveIcon.Value);
                    simulacrumBackdrop.SetActive(Main.showSimulacrumBackdrop.Value);
                    simulacrumOutline.SetActive(Main.showSimulacrumOutline.Value);
                    simulacrumTitle.SetActive(Main.showSimulacrumTitle.Value);
                    if (Main.showSimulacrumWaveOptionalWaveInfo.Value == false && Main.showSimulacrumWaveTitle.Value == false)
                    {
                        simulacrumVerticalLayout.SetActive(false);
                    }
                    else
                    {
                        simulacrumVerticalLayout.SetActive(true);
                        simulacrumWaveOptionalWaveInfo.SetActive(Main.showSimulacrumWaveOptionalWaveInfo.Value);
                        simulacrumWaveTitle.SetActive(Main.showSimulacrumWaveTitle.Value);
                    }
                }
                catch { }
                /*
                viendCorruption.transform.position = new Vector3(Main.changeViendCorruptionPosX.Value, Main.changeViendCorruptionPosY.Value, 10.7745f);
                viendCorruptionFill.enabled = Main.showViendCorruptionColorfill.Value;
                viendCorruptionFillBackdrop.SetActive(Main.showViendCorruptionBackfill.Value);
                viendCorruptionText.SetActive(Main.showViendCorruptionText.Value);
                viendCorruptionTextBackdrop.enabled = Main.showViendCorruptionTextBackdrop.Value;
                */

                timer = 0;
            }
        }
    }
}