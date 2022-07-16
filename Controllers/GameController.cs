using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float time = 1f;
    public string lastTimeOnline;

    public Text goldText;
    public Text wisdomText;
    public Text productionText;

    public Value offlineEarnings;

    public BuildingUiHandler buildingUiHandler;
    public GameObject libraryUi;
    public GameObject offlineEarningsUi;
    public bool buildingUpgradeUiOpen = false;

    [SerializeField] private SaveController _saveController;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CurrencyController _currencyController;
    [SerializeField] private ModifierController _modifierController;
    [SerializeField] private GoldBuildingsController _goldBuildingsController;
    [SerializeField] private UpgradesController _upgradesController;
    [SerializeField] private WishingWellController _wishingWellController;
    [SerializeField] private GameMath _gameMath;

    void Awake() {
        _currencyController.setGold(new Value(5d, 0));

        loadGame();
    }

    void Update() {
        checkForScaleChange();

        time -= Time.deltaTime;
        if (time <= 0) {
            Value totalProduction = getBuildingTotalProduction();
            productionText.text = "Production: " + totalProduction.value.ToString("N2") + Sufix.sufix[totalProduction.scale];
            wisdomText.text = _currencyController.getWisdom().value.ToString("N2") + Sufix.sufix[_currencyController.getWisdom().scale];
            Value valueClass = Currency.add(_currencyController.getGold(), totalProduction);

            _currencyController.setGold(valueClass);

            time = 1f;
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).phase != TouchPhase.Moved) && !buildingUpgradeUiOpen) {
            Ray raycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.name == "House Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Farm Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Animal Farm Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Food Shop Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Armor Shop Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Weapon Shop Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Church Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }
                if (raycastHit.collider.name == "Graveyard Model") {
                    buildingUiHandler.openUpgradeUi(getBuildingByName(raycastHit.collider.name));
                }

                if (raycastHit.collider.name == "library") {
                    libraryUi.SetActive(true);
                    buildingUpgradeUiOpen = true;
                }
                if (raycastHit.collider.name == "wishingWell") {
                    _wishingWellController.openWishingWellUi();
                    buildingUpgradeUiOpen = true;
                }
            }
        }
    }

    void OnApplicationQuit() {
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

        goldText.text = _currencyController.getGold().value.ToString("N2") + Sufix.sufix[_currencyController.getGold().scale];
    }

    public void reset() {
        _goldBuildingsController.setBuildingsOriginalValues();
        _currencyController.setGold(new Value(5f, 0));

        foreach (BuildingController buildingController in getBuildings()) {
            buildingController.activateBuySign();
        }
    }

    public void resetMultipliersButton() {
        _upgradesController.setUpgradesOriginalValues();
        _modifierController.setGlobalMultiplier(1f);
        _modifierController.setGoldMultiplier(1f);
        _modifierController.setWisdomMultiplier(1f);
    }

    public void resetButton() {
        _currencyController.setWisdom(new Value(0f, 0));
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
        _saveController.saveGame();
    }

    public void loadGame() {
        Debug.Log("LoadGame");
        PlayerData data = _saveController.loadGame();

        if (data != null) {
            Debug.Log("Has a saved game");
            _currencyController.setGold(new Value(data.totalGoldValue, data.totalGoldScale));
            _currencyController.setWisdom(new Value(data.totalWisdomValue, data.totalWisdomScale));

            lastTimeOnline = data.lastTimeOnline;

            _wishingWellController.setLastCollectedTime(data.wishingWellLastCollectedTime != "" ? data.wishingWellLastCollectedTime : new System.DateTime(2000, 01, 01).ToString());

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
                building.originalColor.r = data.buildingOriginalColor_r[counter];
                building.originalColor.g = data.buildingOriginalColor_g[counter];
                building.originalColor.b = data.buildingOriginalColor_b[counter];
                building.originalColor.a = data.buildingOriginalColor_a[counter];
                building.actualColor.r = data.buildingActualColor_r[counter];
                building.actualColor.g = data.buildingActualColor_g[counter];
                building.actualColor.b = data.buildingActualColor_b[counter];
                building.actualColor.a = data.buildingActualColor_a[counter];
                counter++;
            }

            _goldBuildingsController.setBuildingsColor();

            offlineEarningsUi.SetActive(true);
            buildingUpgradeUiOpen = true;
        } else {
            Debug.Log("No game to load");
            _wishingWellController.setLastCollectedTime(new System.DateTime(2000, 01, 01).ToString());
            _goldBuildingsController.setBuildingsOriginalColor();
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

    public void getOfflineEarnings() {
        Value offlineEarnings = getOfflineEarningsValue();
        Value valueClass = Currency.add(_currencyController.getGold(), offlineEarnings);

        _currencyController.setGold(valueClass);

        offlineEarningsUi.SetActive(false);
        buildingUpgradeUiOpen = false;
    }

    public Value getOfflineEarningsValue() {
        string currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(lastTimeOnline)).TotalSeconds.ToString();
        Value actualProduction = getBuildingTotalProduction();
        offlineEarnings.value = double.Parse(diffInSeconds) * actualProduction.value;
        offlineEarnings.scale = actualProduction.scale;

        while (offlineEarnings.value > 1000000) {
            offlineEarnings.value /= 1000000;
            offlineEarnings.scale++;
        }

        return offlineEarnings;
    }
}
