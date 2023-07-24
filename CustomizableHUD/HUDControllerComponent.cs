using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace CustomizableHUD
{
    // ik it's spaghetti code but i'm lazy to come up with a better solution
    public class HUDControllerComponent : MonoBehaviour
    {
        public HUD hud;

        public bool isSimulacrum = false;
        public bool isViend = false;
        public bool hasGottenSimulacrumBullshit = false;

        public Material shadow3d = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombDropshadow3D.mat").WaitForCompletion();
        public Material hologram = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombDropshadowHologram.mat").WaitForCompletion();
        public Material outlined = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombDropshadowOutlined.mat").WaitForCompletion();
        public Material outlinedThicc = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombDropshadowOutlinedThick.mat").WaitForCompletion();
        public Material plain = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombPlain.mat").WaitForCompletion();
        public Material shadow = Addressables.LoadAssetAsync<Material>("RoR2/Base/Common/Fonts/Bombardier/tmpBombDropshadow.asset").WaitForCompletion();

        public Transform mainContainer;

        public GameObject buildLabel;
        public GameObject scopeContainer;

        public GameObject notifArea;

        public GameObject mapNameCluster;

        public GameObject mapName;
        public GameObject mapSubtitle;

        public GameObject mainUIArea;

        public GameObject springCanvas;

        public GameObject leftCenter;
        public GameObject rightCenter;
        public GameObject bottomLeft;
        public GameObject upperRight;
        public GameObject upperRightReal;
        public GameObject bottomRight;

        public GameObject bottomCenter;
        public GameObject topCenter;

        public GameObject upperLeft;

        public Image hitmarker;
        public GameObject crosshair;

        public GameObject chatBox;
        public GameObject barRoots;
        public GameObject levelDisplayCluster;
        public GameObject healthBarRoot;
        public GameObject hpLevelVal;
        public HGTextMeshProUGUI xpText;
        public GameObject hpLevelBar;
        public GameObject uselessHealthBar;
        public GameObject uselessHealthBar2;
        public Image healthBar;

        public GameObject timerPanel;

        public GameObject stupidWormgear;
        public GameObject timerText;
        public Image timerBg;
        public GameObject diffPanel;

        public GameObject stageAmbientPanel;

        public GameObject stage;
        public GameObject ambient;

        public GameObject diffBar;

        public GameObject coolWormgear;
        public Image scroller;
        public GameObject backdrop;
        public GameObject viewport;
        public GameObject marker;
        public GameObject outline;

        public GameObject objectiveArtifact;

        public GameObject artifact;
        public GameObject objective;
        public Image artifactBg;
        public Image objectiveBg;
        public Image evolutionBg;
        public GameObject evolutionLabel;

        public GameObject altEquipRoot;

        public GameObject altEquipBg;
        public GameObject altEquipText;

        public GameObject equipRoot;

        public GameObject equipBg;
        public GameObject equipText;

        public GameObject primaryRoot;

        public GameObject primaryText;

        public GameObject secondaryRoot;

        public GameObject secondaryText;

        public GameObject utilityRoot;

        public GameObject utilityText;

        public GameObject specialRoot;

        public GameObject specialText;

        public GameObject sprintCluster;

        public GameObject sprintText;
        public GameObject sprintIcon;

        public GameObject inventoryCluster;

        public GameObject inventoryText;
        public GameObject inventoryIcon;

        public Image upperLeftOutline;

        public GameObject moneyRoot;

        public GameObject moneyBg;
        public GameObject moneyIcon;

        public GameObject lunarRoot;

        public GameObject lunarBg;
        public GameObject lunarIcon;

        public GameObject spectatorLabel;

        public Image topCenterOutline;

        public GameObject bossRoot;
        public GameObject bossLabel;
        public GameObject bossSubtitle;

        public GameObject scoreboard;
        public GameObject scoreboardContainer;
        public GameObject scoreboardPlayerText;
        public GameObject scoreboardItemText;
        public GameObject scoreboardEquipmentText;
        public Image scoreboardBackground;
        public Image scoreboardOutlineLight;
        public Image scoreboardOutlineDarkPlayer;
        public Image scoreboardOutlineDarkItem;
        public Image scoreboardOutlineDarkEquipment;

        public GameObject crosshairExtras;
        public GameObject viendCorruption;
        public GameObject viendFillRoot;
        public GameObject viendFill;
        public Image viendCorruptionTextBackdrop;
        public GameObject viendCorruptionText;
        public GameObject viendCorruptionFillBackdrop;
        public Image viendCorruptionFill;

        public GameObject simulacrumNextWaveUI;
        public GameObject simulacrumCurrentWaveUI;
        public GameObject simulacrumWaveIcon;
        public GameObject simulacrumVerticalLayout;
        public GameObject simulacrumWaveTitle;
        public GameObject simulacrumWaveOptionalWaveInfo;
        public GameObject simulacrumBackdrop;
        public GameObject simulacrumTitle;
        public GameObject simulacrumOutline;

        public Vector3 topLeftV = new(0f, 0f, 0f);
        public Vector3 topCenterV = new(0f, 0f, 0f);
        public Vector3 topRightV = new(0f, 0f, 0f);

        public Vector3 leftCenterV = new(0f, 0f, 0f);
        public Vector3 rightCenterV = new(0f, 0f, 0f);
        public Vector3 bottomLeftV = new(0f, 0f, 0f);

        public Vector3 bottomCenterV = new(0f, 0f, 0f);
        public Vector3 bottomRightV = new(0f, 0f, 0f);

        public Vector3 topLeftRotV = new(0f, 0f, 0f);
        public Vector3 topCenterRotV = new(0f, 0f, 0f);
        public Vector3 topRightRotV = new(0f, 0f, 0f);

        public Vector3 leftCenterRotV = new(0f, 0f, 0f);
        public Vector3 rightCenterRotV = new(0f, 0f, 0f);

        public Vector3 bottomLeftRotV = new(0f, 0f, 0f);
        public Vector3 bottomCenterRotV = new(0f, 0f, 0f);
        public Vector3 bottomRightRotV = new(0f, 0f, 0f);

        public HGTextMeshProUGUI betterUIStatsDisplay;
        public GameObject betterUIStupidSTUPIDBuffer;
        public Image betterUIBackground;

        public float timer;
        public float interval = 0.5f;

        public CanvasGroup canvasGroup;

        public void OnEnable()
        {
            InstanceTracker.Add(this);
        }

        public void OnDisable()
        {
            InstanceTracker.Remove(this);
        }

        public void Start()
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

            chatBox = bottomLeft.transform.GetChild(0).gameObject;
            barRoots = bottomLeft.transform.GetChild(1).gameObject;
            levelDisplayCluster = barRoots.transform.GetChild(0).gameObject;
            healthBarRoot = barRoots.transform.GetChild(1).gameObject;
            hpLevelVal = levelDisplayCluster.transform.GetChild(1).gameObject;
            xpText = hpLevelVal.transform.GetChild(1).GetComponent<HGTextMeshProUGUI>();
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

            if (isViend)
            {
                viendCorruption = crosshairExtras.transform.GetChild(0).gameObject;
                viendFillRoot = viendCorruption.transform.GetChild(0).gameObject;
                viendFill = viendFillRoot.transform.GetChild(1).gameObject;
                viendCorruptionTextBackdrop = viendFillRoot.transform.GetChild(0).GetComponent<Image>();
                viendCorruptionFillBackdrop = viendFill.transform.GetChild(0).gameObject;
                viendCorruptionText = viendCorruptionTextBackdrop.transform.GetChild(0).gameObject;
                viendCorruptionFill = viendFill.transform.GetChild(1).GetComponent<Image>();
            }

            scoreboard = springCanvas.transform.GetChild(8).gameObject;
            scoreboardContainer = scoreboard.transform.GetChild(0).gameObject;
            scoreboardPlayerText = scoreboardContainer.transform.GetChild(0).gameObject;
            scoreboardItemText = scoreboardContainer.transform.GetChild(1).gameObject;
            scoreboardEquipmentText = scoreboardContainer.transform.GetChild(2).gameObject;
            /*
            scoreboardOutlineLight = scoreboardContainer.transform.GetChild(3).GetComponent<Image>();
            scoreboardBackground = scoreboardOutlineLight.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            scoreboardOutlineDarkItem = scoreboardBackground.transform.GetChild(5).GetComponent<Image>();
            scoreboardOutlineDarkEquipment = scoreboardBackground.transform.GetChild(6).GetComponent<Image>();
            scoreboardOutlineDarkPlayer = scoreboardBackground.transform.GetChild(1).GetComponent<Image>();
            */
            if (objectiveArtifact.transform.childCount >= 3)
            {
                if (isSimulacrum)
                {
                    betterUIStupidSTUPIDBuffer = objectiveArtifact.transform.GetChild(3).gameObject;
                    betterUIStatsDisplay = objectiveArtifact.transform.GetChild(4).GetChild(0).GetComponent<HGTextMeshProUGUI>();
                    betterUIBackground = objectiveArtifact.transform.GetChild(4).GetComponent<Image>();
                }
                else
                {
                    betterUIStupidSTUPIDBuffer = objectiveArtifact.transform.GetChild(2).gameObject;
                    betterUIStatsDisplay = objectiveArtifact.transform.GetChild(3).GetChild(0).GetComponent<HGTextMeshProUGUI>();
                    betterUIBackground = objectiveArtifact.transform.GetChild(3).GetComponent<Image>();
                }
            }

            topLeftV.z = 12.6537f;
            topCenterV.z = 12.6537f;
            topRightV.z = 12.6537f;

            leftCenterV.z = 12.6537f;
            rightCenterV.z = 12.6537f;

            bottomLeftV.z = 12.6537f;
            bottomCenterV.z = 12.6537f;
            bottomRightV.z = 12.6537f;
        }

        public void FixedUpdate()
        {
            timer += Time.fixedDeltaTime;
            if (timer > interval)
            {
                if (canvasGroup)
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

                upperLeft.SetActive(Main.showUpperLeft.Value);
                upperLeft.transform.position = topLeftV;
                upperLeft.transform.localEulerAngles = topLeftRotV;

                topCenter.SetActive(Main.showTopCenter.Value);
                topCenter.transform.position = topCenterV;
                topCenter.transform.localEulerAngles = topCenterRotV;

                upperRight.SetActive(Main.showUpperRight.Value);

                upperRightReal.transform.position = topRightV;
                upperRightReal.transform.localEulerAngles = topRightRotV;

                leftCenter.transform.position = leftCenterV;
                leftCenter.transform.localEulerAngles = leftCenterRotV;

                rightCenter.transform.position = rightCenterV;
                rightCenter.transform.localEulerAngles = rightCenterRotV;

                bottomLeft.SetActive(Main.showBottomLeft.Value);
                bottomLeft.transform.position = bottomLeftV;
                bottomLeft.transform.localEulerAngles = bottomLeftRotV;

                bottomCenter.SetActive(Main.showBottomCenter.Value);
                bottomCenter.transform.position = bottomCenterV;
                bottomCenter.transform.localEulerAngles = bottomCenterRotV;

                bottomRight.SetActive(Main.showBottomRight.Value);
                bottomRight.transform.position = bottomRightV;
                bottomRight.transform.localEulerAngles = bottomRightRotV;

                buildLabel.SetActive(Main.showBuildLabel.Value);

                scopeContainer.SetActive(Main.showScope.Value);

                notifArea.SetActive(Main.showNotifArea.Value);

                mapName.SetActive(Main.showMapName.Value);
                mapSubtitle.SetActive(Main.showMapSubtitle.Value);

                hitmarker.enabled = Main.showHitmarker.Value;
                crosshair.SetActive(Main.showCrosshair.Value);

                chatBox.SetActive(Main.showChatBox.Value);
                hpLevelVal.SetActive(Main.showHpLevelVal.Value);
                hpLevelBar.SetActive(Main.showHpLevelBar.Value);

                stupidWormgear.SetActive(Main.showStupidWormgear.Value);
                timerText.SetActive(Main.showTimerText.Value);
                timerBg.enabled = Main.showTimerBg.Value;

                stage.SetActive(Main.showStage.Value);
                ambient.SetActive(Main.showAmbient.Value);

                if (isSimulacrum)
                {
                    diffBar.SetActive(false);
                    if (simulacrumNextWaveUI == null) hasGottenSimulacrumBullshit = false;
                    if (!hasGottenSimulacrumBullshit && crosshairExtras.transform.childCount >= 2)
                    {
                        simulacrumNextWaveUI = crosshairExtras.transform.GetChild(0).gameObject;
                        simulacrumCurrentWaveUI = crosshairExtras.transform.GetChild(1).gameObject;
                        simulacrumWaveIcon = simulacrumCurrentWaveUI?.transform.GetChild(0).GetChild(0).gameObject;
                        simulacrumVerticalLayout = simulacrumCurrentWaveUI?.transform.GetChild(0).GetChild(1).gameObject;
                        simulacrumWaveTitle = simulacrumVerticalLayout?.transform.GetChild(0).gameObject;
                        simulacrumWaveOptionalWaveInfo = simulacrumVerticalLayout?.transform.GetChild(1).gameObject;
                        simulacrumBackdrop = simulacrumNextWaveUI?.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
                        simulacrumTitle = simulacrumNextWaveUI?.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
                        simulacrumOutline = simulacrumCurrentWaveUI?.transform.GetChild(0).GetChild(2).gameObject;

                        hasGottenSimulacrumBullshit = true;
                    }
                    if (simulacrumNextWaveUI)
                    {
                        simulacrumNextWaveUI.SetActive(Main.showSimulacrumNextWaveUI.Value);
                        simulacrumCurrentWaveUI.SetActive(Main.showSimulacrumCurrentWaveUI.Value);
                        simulacrumWaveIcon.SetActive(Main.showSimulacrumWaveIcon.Value);
                        simulacrumBackdrop.SetActive(Main.showSimulacrumBackdrop.Value);
                        simulacrumOutline.SetActive(Main.showSimulacrumOutline.Value);
                        simulacrumTitle.SetActive(Main.showSimulacrumTitle.Value);
                    }

                    if (Main.showSimulacrumWaveOptionalWaveInfo.Value == false && Main.showSimulacrumWaveTitle.Value == false)
                    {
                        simulacrumVerticalLayout?.SetActive(false);
                    }
                    else
                    {
                        simulacrumVerticalLayout?.SetActive(true);
                        simulacrumWaveOptionalWaveInfo?.SetActive(Main.showSimulacrumWaveOptionalWaveInfo.Value);
                        simulacrumWaveTitle?.SetActive(Main.showSimulacrumWaveTitle.Value);
                    }
                }
                else
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

                upperLeftOutline.enabled = Main.showUpperLeftOutline.Value;

                moneyBg.SetActive(Main.showMoneyBg.Value);
                moneyIcon.SetActive(Main.showMoneyIcon.Value);

                lunarBg.SetActive(Main.showLunarBg.Value);
                lunarIcon.SetActive(Main.showLunarIcon.Value);

                spectatorLabel.SetActive(Main.showSpectatorLabel.Value);

                topCenterOutline.enabled = Main.showTopCenterOutline.Value;

                bossLabel.SetActive(Main.showBossText.Value);
                bossSubtitle.SetActive(Main.showBossSubtitle.Value);

                xpText.fontSharedMaterial.SetColor("_FaceColor", Main.fontTint.Value);

                shadow3d.SetColor("_FaceColor", Main.fontTint.Value);
                hologram.SetColor("_FaceColor", Main.fontTint.Value);
                outlined.SetColor("_FaceColor", Main.fontTint.Value);
                outlinedThicc.SetColor("_FaceColor", Main.fontTint.Value);
                plain.SetColor("_FaceColor", Main.fontTint.Value);
                shadow.SetColor("_FaceColor", Main.fontTint.Value);

                if (betterUIStupidSTUPIDBuffer)
                    betterUIStupidSTUPIDBuffer.SetActive(Main.showBetteruiStupidSTUPIDBuffer.Value);

                if (betterUIStatsDisplay)
                    betterUIStatsDisplay.fontMaterial.SetColor("_FaceColor", Main.fontTint.Value);

                if (betterUIBackground)
                    betterUIBackground.enabled = Main.showBetteruiBackground.Value;

                if (healthBarRoot.transform.childCount >= 1 && healthBarRoot.transform.GetChild(0).childCount >= 2)
                {
                    if (healthBar == null)
                    {
                        healthBar = healthBarRoot.transform.GetChild(0).GetChild(2).GetComponent<Image>();
                    }

                    if (healthBar)
                    {
                        healthBar.material.color = Main.entireHUDtint.Value;
                    }

                    if (uselessHealthBar == null)
                    {
                        uselessHealthBar = healthBarRoot.transform.GetChild(0).GetChild(1).gameObject;
                    }
                    if (uselessHealthBar.activeSelf)
                    {
                        uselessHealthBar.SetActive(false);
                    }

                    if (uselessHealthBar2 == null)
                    {
                        uselessHealthBar2 = healthBarRoot.transform.GetChild(0).GetChild(0).gameObject;
                    }
                    if (uselessHealthBar2.activeSelf)
                    {
                        uselessHealthBar2.SetActive(false);
                    }
                }

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
                        if (evolutionBg)
                        {
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
                    }
                }
                catch { }
                if (isViend && viendCorruption)
                {
                    viendCorruption.transform.position = new Vector3(Main.changeViendCorruptionPosX.Value, Main.changeViendCorruptionPosY.Value, 10.7745f);
                    viendCorruptionFill.enabled = Main.showViendCorruptionColorfill.Value;
                    viendCorruptionFillBackdrop.SetActive(Main.showViendCorruptionBackfill.Value);
                    viendCorruptionText.SetActive(Main.showViendCorruptionText.Value);
                    viendCorruptionTextBackdrop.enabled = Main.showViendCorruptionTextBackdrop.Value;
                }

                timer = 0;
            }
        }
    }
}