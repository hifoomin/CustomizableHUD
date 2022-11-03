using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Mono.Cecil;
using R2API;
using R2API.Utils;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RiskOfOptions;
using RoR2;
using UnityEngine;
using UnityEngine.UI;
using RoR2.UI;
using JetBrains.Annotations;
using Rewired.Utils;

namespace CustomizableHUD
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [R2APISubmoduleDependency(nameof(LanguageAPI))]
    public class Main : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;

        public const string PluginAuthor = "HIFU";
        public const string PluginName = "CustomizableHUD";
        public const string PluginVersion = "1.0.0";

        private ConfigFile CHConfig;
        private ManualLogSource CHLogger;

        private string version = PluginVersion;

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
        public static ConfigEntry<bool> showDiffPanel { get; set; }
        public static ConfigEntry<bool> showStage { get; set; }
        public static ConfigEntry<bool> showAmbient { get; set; }
        public static ConfigEntry<bool> showCoolWormgear { get; set; }
        public static ConfigEntry<bool> showScroller { get; set; }
        public static ConfigEntry<bool> showMarker { get; set; }
        public static ConfigEntry<bool> showOutline { get; set; }
        public static ConfigEntry<bool> showArtifact { get; set; }
        public static ConfigEntry<bool> showObjective { get; set; }
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

        public void Awake()
        {
            CHLogger = Logger;
            CHConfig = Config;

            showUpperLeft = Config.Bind("Top Left", "Top left", true, "Show top left holder?");
            showUpperLeftOutline = Config.Bind("Top Left", "Outline", true, "Show outline?");
            showMoneyBg = Config.Bind("Top Left", "Money bg", true, "Show money background?");
            showMoneyIcon = Config.Bind("Top Left", "Money icon", true, "Show money icon?");
            showLunarBg = Config.Bind("Top Left", "Lunar bg", true, "Show lunar background?");
            showLunarIcon = Config.Bind("Top Left", "Lunar icon", true, "Show lunar icon?");

            showTopCenter = Config.Bind("Top Center", "Top center", true, "Show top center holder?");
            showTopCenterOutline = Config.Bind("Top Center", "Outline", true, "Show outline?");
            showMapName = Config.Bind("Top Center", "Map name", true, "Show stage name on entry?");
            showMapSubtitle = Config.Bind("Top Center", "Map subtitle", true, "Show stage subtitle on entry?");
            showBossText = Config.Bind("Top Center", "Boss text", true, "Show boss name?");
            showBossSubtitle = Config.Bind("Top Center", "Boss subtitle", true, "Show boss subtitle?");

            showBuildLabel = Config.Bind("Top Right", "Build label", true, "Show game version number?");
            showUpperRight = Config.Bind("Top Right", "Top right", true, "Show top right holder?");
            showStupidWormgear = Config.Bind("Top Right", "Stupid wormgear", true, "Show rightmost wormgear?");
            showTimerText = Config.Bind("Top Right", "Timer text", true, "Show timer value?");
            showDiffPanel = Config.Bind("Top Right", "Difficulty panel", true, "Show difficulty icon?");
            showAmbient = Config.Bind("Top Right", "Stage ambient panel", true, "Show ambient level?");
            showStage = Config.Bind("Top Right", "Stage", true, "Show stage number?");
            showCoolWormgear = Config.Bind("Top Right", "Cool wormgear", true, "Show the based wormgear?");
            showScroller = Config.Bind("Top Right", "Scroller", true, "Show scrolling difficulty bar?");
            showMarker = Config.Bind("Top Right", "Marker", true, "Show current difficulty marker?");
            showOutline = Config.Bind("Top Right", "Outline", true, "Show difficulty holder outline?");
            showArtifact = Config.Bind("Top Right", "Artifact", true, "Show artifact holder?");
            showObjective = Config.Bind("Top Right", "Objective", true, "Show objective holder?");

            showHitmarker = Config.Bind("Center", "Hitmarker", true, "Show hitmarker?");
            showCrosshair = Config.Bind("Center", "Crosshair", true, "Show crosshair holder?");
            showScope = Config.Bind("Center", "Scope", true, "Show Railgunner's scope?");

            showBottomLeft = Config.Bind("Bottom Left", "Bottom left", true, "Show bottom left holder?");
            showChatBox = Config.Bind("Bottom Left", "Chatbox", true, "Show chatbox holder?");
            showHpLevelVal = Config.Bind("Bottom Left", "Hp level val", true, "Show experience bar?");
            showHpLevelBar = Config.Bind("Bottom Left", "Hp level bar", true, "Show your level?");

            showBottomCenter = Config.Bind("Bottom Center", "Bottom Center", true, "Show bottom center holder?");
            showSpectatorLabel = Config.Bind("Bottom Center", "Spectator label", true, "Show spectator text and background?");

            showNotifArea = Config.Bind("Bottom Center", "Transformation notification", true, "Show transformation notifications such as corrupting or consuming an item?");

            showBottomRight = Config.Bind("Bottom Right", "Bottom Right", true, "Show bottom right holder?");
            showAltEquipBg = Config.Bind("Bottom Right", "Alt equip bg", true, "Show alt equipment background?");
            showAltEquipText = Config.Bind("Bottom Right", "Alt equip text", true, "Show alt equipment keybind text?");
            showEquipBg = Config.Bind("Bottom Right", "Equip bg", true, "Show equipment background?");
            showEquipText = Config.Bind("Bottom Right", "Equip text", true, "Show equipment keybind text?");
            showSkillText = Config.Bind("Bottom Right", "Skill text", true, "Show skill keybind text?");
            showSprintText = Config.Bind("Bottom Right", "Sprint text", true, "Show sprint keybind text?");
            showSprintIcon = Config.Bind("Bottom Right", "Sprint icon", true, "Show sprint icon?");
            showInventoryText = Config.Bind("Bottom Right", "Inventory text", true, "Show inventory keybind text?");
            showInventoryIcon = Config.Bind("Bottom Right", "Inventory icon", true, "Show inventory icon?");

            var tabID = 0;
            foreach (ConfigEntryBase ceb in Config.GetConfigEntries())
            {
                var Name = ceb.Definition.Section;
                if (Name.Contains("Top Left") || Name.Contains("Top Center") || Name.Contains("Top Right"))
                {
                    tabID = 0;
                    Name = "Top";
                    //ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, Name);
                }
                if (Name.Contains("Center"))
                {
                    tabID = 1;
                    Name = "Center";
                    // ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (Name.Contains("Bottom Left") || Name.Contains("Bottom Center") || Name.Contains("Bottom Right"))
                {
                    tabID = 2;
                    Name = "Bottom";
                    // ModSettingsManager.SetModIcon(Main.stageaesthetic.LoadAsset<Sprite>("texModIcon.png"), "StageAesthetic.TabID." + tabID, "SA: " + Name);
                }
                if (ceb.DefaultValue.GetType() == typeof(bool))
                {
                    ModSettingsManager.AddOption(new CheckBoxOption((ConfigEntry<bool>)ceb, new CheckBoxConfig()), "CH.TabID." + tabID, "CH: " + Name);
                }
            }
            On.RoR2.UI.HUD.Awake += HUD_Awake;
        }

        public static HUD hud;

        // hud here?

        private void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, HUD self)
        {
            orig(self);
            self.targetBodyObject.AddComponent<HUDControllerComponent>();
            hud = self;
        }

        private void WithinDestructionLotusAlbum()
        {
        }

        private void SpaceOfVariationsImagoAlbum()
        {
        }
    }

    public class HUDControllerComponent : MonoBehaviour
    {
        public HUD hud;
        // is this gonna be useful at all

        private Transform mainContainer;

        private GameObject buildLabel;
        private GameObject scopeContainer;

        private GameObject notifArea;

        private GameObject mapNameCluster;

        private GameObject mapName;
        private GameObject mapSubtitle;

        private GameObject mainUIArea;

        private GameObject hitmarker;
        private GameObject crosshair;

        private GameObject mainCanvas2;
        private GameObject bottomLeft;

        private GameObject chatBox;
        private GameObject hpLevelVal;
        private GameObject hpLevelBar;

        private GameObject upperRight;
        private GameObject timerPanel;

        private GameObject stupidWormgear;
        private GameObject timerText;
        private GameObject diffPanel;

        private GameObject stageAmbientPanel;

        private GameObject stage;
        private GameObject ambient;

        private GameObject diffBar;

        private GameObject coolWormgear;
        private GameObject scroller;
        private GameObject marker;
        private GameObject outline;

        private GameObject objectiveArtifact;

        private GameObject artifact;
        private GameObject objective;

        private GameObject bottomRight;
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

        private GameObject upperLeft;
        private Image upperLeftOutline;

        private GameObject moneyRoot;

        private GameObject moneyBg;
        private GameObject moneyIcon;

        private GameObject lunarRoot;

        private GameObject lunarBg;
        private GameObject lunarIcon;

        private GameObject bottomCenter;

        private GameObject spectatorLabel;

        private GameObject topCenter;
        private Image topCenterOutline;

        private GameObject bossRoot;
        private GameObject bossLabel;
        private GameObject bossSubtitle;
        private float timer;
        private float interval = 2f;

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
            buildLabel = mainContainer.GetChild(0).gameObject;
            scopeContainer = mainContainer.GetChild(2).gameObject;

            notifArea = mainContainer.GetChild(4).gameObject;

            mapNameCluster = mainContainer.GetChild(3).gameObject;

            mapName = mapNameCluster.transform.GetChild(0).gameObject;
            mapSubtitle = mapNameCluster.transform.GetChild(1).gameObject;

            mainUIArea = mainContainer.GetChild(7).gameObject;

            hitmarker = mainUIArea.transform.GetChild(0).gameObject;
            crosshair = mainUIArea.transform.GetChild(1).gameObject;

            mainCanvas2 = mainUIArea.transform.GetChild(2).gameObject;
            bottomLeft = mainCanvas2.transform.GetChild(0).gameObject;

            chatBox = bottomLeft.transform.GetChild(0).gameObject;
            hpLevelVal = bottomLeft.transform.GetChild(1).GetChild(0).gameObject;
            hpLevelBar = bottomLeft.transform.GetChild(1).GetChild(1).gameObject;

            upperRight = mainCanvas2.transform.GetChild(1).GetChild(0).gameObject;
            timerPanel = upperRight.transform.GetChild(0).gameObject;

            stupidWormgear = timerPanel.transform.GetChild(0).gameObject;
            timerText = timerPanel.transform.GetChild(1).gameObject;
            diffPanel = upperRight.transform.GetChild(1).gameObject;

            stageAmbientPanel = upperRight.transform.GetChild(2).gameObject;

            stage = stageAmbientPanel.transform.GetChild(0).gameObject;
            ambient = stageAmbientPanel.transform.GetChild(1).gameObject;

            diffBar = upperRight.transform.GetChild(3).gameObject;

            coolWormgear = diffBar.transform.GetChild(0).gameObject;
            scroller = diffBar.transform.GetChild(1).gameObject;
            marker = diffBar.transform.GetChild(3).gameObject;

            outline = upperRight.transform.GetChild(4).gameObject;

            objectiveArtifact = upperRight.transform.GetChild(5).gameObject;

            artifact = objectiveArtifact.transform.GetChild(0).gameObject;
            objective = objectiveArtifact.transform.GetChild(1).gameObject;

            bottomRight = mainCanvas2.transform.GetChild(2).GetChild(0).gameObject;
            altEquipRoot = bottomRight.transform.GetChild(1).GetChild(0).gameObject;

            altEquipBg = altEquipRoot.transform.GetChild(1).gameObject;
            altEquipText = altEquipRoot.transform.GetChild(4).gameObject;

            equipRoot = bottomRight.transform.GetChild(2).GetChild(1).gameObject;

            equipBg = equipRoot.transform.GetChild(1).gameObject;
            equipText = equipRoot.transform.GetChild(4).gameObject;

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

            upperLeft = mainCanvas2.transform.GetChild(3).gameObject;

            // special case
            upperLeftOutline = upperLeft.GetComponent<Image>();
            //

            moneyRoot = upperLeft.transform.GetChild(0).gameObject;

            moneyBg = moneyRoot.transform.GetChild(0).gameObject;
            moneyIcon = moneyRoot.transform.GetChild(4).gameObject;

            lunarRoot = upperLeft.transform.GetChild(2).gameObject;

            lunarBg = lunarRoot.transform.GetChild(0).gameObject;
            lunarIcon = lunarRoot.transform.GetChild(4).gameObject;

            bottomCenter = mainCanvas2.transform.GetChild(4).gameObject;

            spectatorLabel = bottomCenter.transform.GetChild(0).gameObject;

            topCenter = mainCanvas2.transform.GetChild(5).gameObject;

            // special case
            topCenterOutline = topCenter.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            //

            bossRoot = topCenter.transform.GetChild(1).GetChild(0).gameObject;
            bossLabel = bossRoot.transform.GetChild(1).gameObject;
            bossSubtitle = bossRoot.transform.GetChild(2).gameObject;
        }

        private void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > interval)
            {
                buildLabel.SetActive(Main.showBuildLabel.Value);

                scopeContainer.SetActive(Main.showScope.Value);

                notifArea.SetActive(Main.showNotifArea.Value);

                mapName.SetActive(Main.showMapName.Value);
                mapSubtitle.SetActive(Main.showMapSubtitle.Value);

                hitmarker.SetActive(Main.showHitmarker.Value);
                crosshair.SetActive(Main.showCrosshair.Value);

                bottomLeft.SetActive(Main.showBottomLeft.Value);
                /*
                chatBox.SetActive(Main.showChatBox.Value);
                hpLevelVal.SetActive(Main.showHpLevelVal.Value);
                hpLevelBar.SetActive(Main.showHpLevelBar.Value);

                upperRight.SetActive(Main.showUpperRight.Value);

                stupidWormgear.SetActive(Main.showStupidWormgear.Value);
                timerText.SetActive(Main.showTimerText.Value);

                stage.SetActive(Main.showStage.Value);
                ambient.SetActive(Main.showAmbient.Value);

                diffBar.SetActive(Main.showDiffPanel.Value);

                coolWormgear.SetActive(Main.showCoolWormgear.Value);
                scroller.SetActive(Main.showScroller.Value);
                marker.SetActive(Main.showMarker.Value);

                outline.SetActive(Main.showOutline.Value);

                artifact.SetActive(Main.showArtifact.Value);
                objective.SetActive(Main.showObjective.Value);

                bottomRight.SetActive(Main.showBottomRight.Value);

                altEquipBg.SetActive(Main.showAltEquipBg.Value);
                altEquipText.SetActive(Main.showAltEquipText.Value);

                equipBg.SetActive(Main.showEquipBg.Value);
                equipText.SetActive(Main.showAltEquipText.Value);

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

                timer = 0;
                */
            }
        }
    }
}