using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float time = 1f;
    public string lastTimeOnline;
    public static string wishingWellLastCollectedTime = "";

    public Text goldText;
    public Text wisdomText;
    public Text productionText;

    public Value offlineEarnings;

    public BuildingUiHandler buildingUiHandler;
    public GameObject upgradeBuildingUi;
    public GameObject wishingWellUi;
    public GameObject libraryUi;
    public bool buildingUpgradeUiOpen = false;

    // public BuildingScriptableObject houseScriptableObject;
    // public BuildingScriptableObject farmScriptableObject;
    // public BuildingScriptableObject animalFarmScriptableObject;
    // public BuildingScriptableObject foodShopScriptableObject;
    // public BuildingScriptableObject armorShopScriptableObject;
    // public BuildingScriptableObject weaponShopScriptableObject;
    // public BuildingScriptableObject cemeteryShopScriptableObject;
    // public BuildingScriptableObject churchShopScriptableObject;

    [SerializeField] private SaveController saveController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CurrencyController _currencyController;
    [SerializeField] private ModifierController _modifierController;
    [SerializeField] private GoldBuildingsController _goldBuildingsController;
    [SerializeField] private GameMath _gameMath;

    void Start() {
        _currencyController.setGold(new Value(5f, 0));

        loadGame();
    }

    void Update() {
        checkForScaleChange();

        List<BuildingScriptableObject> goldGeneratorBuildings = getGoldGeneratorBuildings();

        foreach (BuildingScriptableObject building in goldGeneratorBuildings) {
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
            productionText.text = "Production: " + totalProduction.value.ToString();
            wisdomText.text = _currencyController.getWisdom().value.ToString("N2");
            Value valueClass = Currency.add(_currencyController.getGold(), totalProduction);

            _currencyController.setGold(valueClass);

            time = 1f;
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended)  && !buildingUpgradeUiOpen) {
            Ray raycast = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.name == "house") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "farm") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "animalFarm") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "foodShop") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "armorShop") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "weaponShop") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "church") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "graveyrd") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }

                if (raycastHit.collider.name == "library") {
                    libraryUi.SetActive(true);
                }
                if (raycastHit.collider.name == "wishingWell") {
                    wishingWellUi.SetActive(true);
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

        return buildingController;
    }

    void debug() {
        if (Input.GetKeyDown(KeyCode.Q)) {
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
        if (_currencyController.getGold().value > 1000000) {
            Value tempValue = new Value(_currencyController.getGold().value, _currencyController.getGold().scale);
            tempValue.value /= 1000000;
            tempValue.scale++;

            _currencyController.setGold(tempValue);
        }

        goldText.text = _currencyController.getGold().value.ToString("N2") + Currency.suifx[_currencyController.getGold().scale];
    }

    public void reset() {
        _goldBuildingsController.setBuildingsOriginalValues();
        _currencyController.setGold(new Value(5f, 0));
    }

    public void resetButton() {
        _currencyController.setWisdom(new Value(0f, 0));
        _modifierController.setGlobalMultiplier(1f);
        Debug.Log("Setando outro global multiplier");
        _modifierController.setGoldMultiplier(1f);
        _modifierController.setWisdomMultiplier(1f);
        reset();
    }

    public void prestigeButton() {
        Value prestigeValue = new Value();

        if (_currencyController.getGold().scale > 0 || (_currencyController.getGold().scale == 0 && _currencyController.getGold().value >= 100000f)) {
            prestigeValue = _gameMath.getPrestige(_currencyController.getGold());
            prestigeValue = Currency.add(_currencyController.getWisdom(), prestigeValue);
            _currencyController.setWisdom(prestigeValue);
            reset();
        } else {
            Debug.Log("Not enough gold to prestige");
        }
    }

    public void saveGame() {
        saveController.saveGame();
    }

    public void loadGame() {
        // Debug.Log("LoadGame");
        PlayerData data = saveController.loadGame();

        if (data != null) {
            _currencyController.setGold(new Value(data.totalGoldValue, data.totalGoldScale));
            _currencyController.setWisdom(new Value(data.totalWisdomValue, data.totalWisdomScale));

            lastTimeOnline = data.lastTimeOnline;

            GameController.wishingWellLastCollectedTime = data.wishingWellLastCollectedTime != "" ? data.wishingWellLastCollectedTime : new System.DateTime(2000, 01, 01).ToString();

            _modifierController.setGlobalMultiplier(data.multiplier);

            List<BuildingScriptableObject> goldGeneratorBuildings = getGoldGeneratorBuildings();

            int counter = 0;
            foreach (BuildingScriptableObject building in goldGeneratorBuildings) {
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
            _goldBuildingsController.setBuildingsOriginalValues();
        }
    }

    public List<BuildingScriptableObject> getGoldGeneratorBuildings() {
        List<BuildingScriptableObject> goldGeneratorBuildings = new List<BuildingScriptableObject>();
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("GoldGeneratorBuilding");

        foreach (GameObject building in buildings) {
            goldGeneratorBuildings.Add(building.GetComponent<BuildingController>().getScriptableObject());
        }

        return goldGeneratorBuildings;
    }

    void getOfflineEarnings() {
        string currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(lastTimeOnline)).TotalSeconds.ToString();
        Value actualProduction = getBuildingTotalProduction();
        offlineEarnings.value = double.Parse(diffInSeconds) * actualProduction.value;
        offlineEarnings.scale = actualProduction.scale;

        Value valueClass = Currency.add(_currencyController.getGold(), offlineEarnings);

        _currencyController.setGold(valueClass);

        while (offlineEarnings.value > 1000000) {
            offlineEarnings.value /= 1000000;
            offlineEarnings.scale++;
        }
    }
}
