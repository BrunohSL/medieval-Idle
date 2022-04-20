using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float time = 1f;
    public string lastTimeOnline;
    public static string wishingWellLastCollectedTime = "";

    public Text soulsText;
    public Text productionText;
    public Value offlineEarnings;

    public GameObject upgradeBuildingUi;
    public BuildingUiHandler buildingUiHandler;
    public GameObject wishingWellUi;
    public bool buildingUpgradeUiOpen = false;

    public BuildingScriptableObject houseScriptableObject;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GoldController goldController;

    void Start() {
        goldController.setGold(new Value(5f, 0));
        // Souls.totalSouls.value = 5;
        // Souls.totalSouls.scale = 0;

        loadGame();
    }

    void Update() {
        checkForScaleChange();

        foreach (Building building in Buildings.buildingsList) {
            if (building.actualProduction.value > 1000000) {
                building.actualProduction.value /= 1000000;
                building.actualProduction.scale++;
            }

            if (building.nextProduction.value > 1000000) {
                building.nextProduction.value /= 1000000;
                building.nextProduction.scale++;
            }
        }

        debug();

        time -= Time.deltaTime;
        if (time <= 0) {
            Value totalProduction = getBuildingTotalProduction();
            productionText.text = totalProduction.value.ToString();
            Value valueClass = Currency.add(goldController.getGold().value, goldController.getGold().scale, totalProduction.value, totalProduction.scale);

            goldController.setGold(valueClass);

            // Souls.totalSouls.value = valueClass.value;
            // Souls.totalSouls.scale = valueClass.scale;
            time = 1f;
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended)  && !buildingUpgradeUiOpen) {
            Ray raycast = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.name == "house") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "church") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "farm") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "armorShop") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "animalFarm") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "foodShop") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
            }
        }
    }

    void FixedUpdate() {
        saveGame();
    }

    private BuildingController[] getBuildings() {
        return GameObject.FindObjectsOfType<BuildingController>();
    }

    private BuildingController getBuildingByName(string buildingName) {
        BuildingController[] buildings = getBuildings();
        BuildingController buildingController = null;

        foreach (BuildingController building in buildings) {
            if (building.name == buildingName) {
                buildingController = building;
            }
        }

        if (buildingController == null) {
            throw new System.Exception("Building not found");
        }

        // BuildingScriptableObject buildingScriptableObject = buildingController.getScriptableObject();

        return buildingController;
    }

    void debug() {
        if (Input.GetKeyDown(KeyCode.T)) {
            // string today = System.DateTime.Now.ToString();
            // string tomorrow = System.DateTime.Parse(today).AddDays(1).ToString();

            // var diffInSeconds = (System.DateTime.Parse(tomorrow) - System.DateTime.Parse(today)).TotalSeconds;
        }
    }

    /**
     * Run through the buildings and sum the production value
     */
    public Value getBuildingTotalProduction() {
        Value valueClass = new Value();

        foreach (GameObject building in GameObject.FindGameObjectsWithTag("GoldGeneratorBuilding")) {
            BuildingController buildingController = building.GetComponent<BuildingController>();
            if (buildingController.getScriptableObject().level > 0) {
                valueClass.value += buildingController.getScriptableObject().actualProduction.value;
                valueClass.scale = buildingController.getScriptableObject().actualProduction.scale;
            }
        }

        return valueClass;
    }

    /**
     * Handles the transition in scale of values
     */
    public void checkForScaleChange() {
        if (goldController.getGold().value > 1000000) {
            Value tempValue = new Value(goldController.getGold().value, goldController.getGold().scale);
            tempValue.value /= 1000000;
            tempValue.scale++;

            goldController.setGold(tempValue);
            // goldController.setGold(new Value(goldController.getGold().value /= 1000000), goldController.getGold().scale++);
            // Souls.totalSouls.value /= 1000000;
            // Souls.totalSouls.scale++;
        }

        soulsText.text = "Souls: " + goldController.getGold().value.ToString("N2") + Currency.suifx[goldController.getGold().scale];
    }

    public void resetButton() {
        houseScriptableObject.actualProduction.value = 0;
        houseScriptableObject.nextProduction.value = 2;
        houseScriptableObject.nextCost.value = 5;
        houseScriptableObject.level = 0;

        goldController.setGold(new Value(5f, 0));
    }

    public void saveGame() {
        SaveController.saveGame();
    }

    public void loadGame() {
        // Debug.Log("LoadGame");
        PlayerData data = SaveController.loadGame();

        if (data != null) {
            goldController.setGold(new Value(data.totalSoulsValue, data.totalSoulsScale));
            // Souls.totalSouls.value = data.totalSoulsValue;
            // Souls.totalSouls.scale = data.totalSoulsScale;

            lastTimeOnline = data.lastTimeOnline;

            GameController.wishingWellLastCollectedTime = data.wishingWellLastCollectedTime != "" ? data.wishingWellLastCollectedTime : new System.DateTime(2000, 01, 01).ToString();

            Modifiers.globalMultiplier = data.multiplier;

            int counter = 0;
            foreach (Building building in Buildings.buildingsList) {
                building.level = data.buildingLevel[counter];
                building.initialProduction = data.buildingInitialProduction[counter];
                building.initialCost = data.buildingInitialCost[counter];
                building.growthRate = data.buildingGrowthRate[counter];
                building.actualProduction.value = data.buildingActualProductionValue[counter];
                building.actualProduction.scale = data.buildingActualProductionScale[counter];
                building.nextProduction.value = data.buildingNextProductionValue[counter];
                building.nextProduction.scale = data.buildingNextProductionScale[counter];
                building.nextCost.value = data.buildingNextCostValue[counter];
                building.nextCost.scale = data.buildingNextCostScale[counter];
                building.buildingMultiplier = data.buildingMultiplier[counter];
                counter++;
            }

            getOfflineEarnings();
        } else {
            // Debug.Log("No game to load");
            GameController.wishingWellLastCollectedTime = new System.DateTime(2000, 01, 01).ToString();
        }
    }

    void getOfflineEarnings() {
        string currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(lastTimeOnline)).TotalSeconds.ToString();
        Value actualProduction = getBuildingTotalProduction();
        offlineEarnings.value = double.Parse(diffInSeconds) * actualProduction.value;
        offlineEarnings.scale = actualProduction.scale;

        Value valueClass = Currency.add(goldController.getGold().value, goldController.getGold().scale, offlineEarnings.value, offlineEarnings.scale);

        goldController.setGold(valueClass);

        // Souls.totalSouls.value = valueClass.value;
        // Souls.totalSouls.scale = valueClass.scale;

        while (offlineEarnings.value > 1000000) {
            offlineEarnings.value /= 1000000;
            offlineEarnings.scale++;
        }
    }
}
