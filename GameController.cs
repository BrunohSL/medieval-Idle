using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private float time = 1f;
    public string lastTimeOnline;
    public static string wishingWellLastCollectedTime = "";

    // public BuildingController buildingController;

    public Text soulsText;
    public Value offlineEarnings;

    public GameObject upgradeBuildingUi;
    public GameObject wishingWellUi;
    public bool buildingUpgradeUiOpen = false;

    // [SerializeField] private GameObject plotPrefab;
    // [SerializeField] private GameObject upgradeGraveyardPrefab;
    // [SerializeField] private GameObject upgradeChurchPrefab;
    // [SerializeField] private GameObject upgradeGuillotinePrefab;
    // [SerializeField] private GameObject upgradeFarmPrefab;
    // [SerializeField] private GameObject upgradeArmorShopPrefab;
    // [SerializeField] private GameObject upgradeChickenNestPrefab;
    // [SerializeField] private GameObject upgradeWeaponShopPrefab;
    // [SerializeField] private GameObject upgradeFoodShopPrefab;
    // [SerializeField] private GameObject upgradeBattleArenaPrefab;

    void Start() {
        Souls.totalSouls.value = 5;
        Souls.totalSouls.scale = 0;

        loadGame();

        // instantiateBuildings();
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
            Value valueClass = Currency.add(Souls.totalSouls.value, Souls.totalSouls.scale, totalProduction.value, totalProduction.scale);

            Souls.totalSouls.value = valueClass.value;
            Souls.totalSouls.scale = valueClass.scale;
            time = 1f;
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Ended)  && !buildingUpgradeUiOpen) {
        // if (Input.GetMouseButtonDown(0) && !buildingUpgradeUiOpen) {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            // Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit)) {
                if (raycastHit.collider.name == "church") {
                    Debug.Log("click na igreja");
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("church"), upgradeBuildingUi);
                }
                // if (raycastHit.collider.name == "guillotine") {
                //     upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("guillotine"), upgradeBuildingUi);
                // }
                if (raycastHit.collider.name == "farm") {
                    Debug.Log("click na fazenda");
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("farm"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "armorShop") {
                    Debug.Log("click na loja de armaduras");
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("armorShop"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "animalFarm") {
                    Debug.Log("click na fazenda de animais");
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("chickenNest"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "foodShop") {
                    Debug.Log("click na loja de comida");
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("foodShop"), upgradeBuildingUi);
                }

                if (raycastHit.collider.name == "weaponShop") {
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("weaponShop"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "graveyard") {
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("graveyard"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "battleArena") {
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("battleArena"), upgradeBuildingUi);
                }

                if (raycastHit.collider.name == "library") {
                    Debug.Log("click na biblioteca");
                    wishingWellUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("library"), upgradeBuildingUi);
                }
                if (raycastHit.collider.name == "wishingWell") {
                    wishingWellUi.GetComponent<WishingWellUiHandler>().openWishingWellUi();
                }
                if (raycastHit.collider.name == "castle") {
                    upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("castle"), upgradeBuildingUi);
                }
                // if (raycastHit.collider.name == "castleWall") {
                //     upgradeBuildingUi.GetComponent<BuildingUiHandler>().openUpgradeUi(Buildings.getBuildingByName("castleWall"), upgradeBuildingUi);
                // }
            }
        }
    }

    void FixedUpdate() {
        saveGame();
    }

    // public void instantiateBuildings() {
    //     foreach (Building building in Buildings.buildingsList) {
    //         if (GameObject.Find(building.buildingName)) {
    //             if (building.buildingName == "castle" || building.buildingName == "castleWall" || building.buildingName == "wishingWell") {
    //                 return;
    //             }

    //             GameObject obj = GameObject.Find(building.buildingName);
    //             Destroy(obj);
    //         }

    //         switch (building.buildingName) {
    //             case "graveyard":
    //                 if (building.level == 0) {
    //                     GameObject buyGraveyardObject = Instantiate(plotPrefab, new Vector3(365, 0.5f, 400), Quaternion.Euler(0, 270, 0));
    //                     buyGraveyardObject.name = "graveyard";
    //                 } else {
    //                     GameObject upgradeGraveyardObject = Instantiate(upgradeGraveyardPrefab, new Vector3(400, 0.3f, 410), Quaternion.Euler(0, 0, 0));
    //                     upgradeGraveyardObject.name = "graveyard";
    //                 }
    //             break;

    //             case "church":
    //                 if (building.level == 0) {
    //                     GameObject buyChurchObject = Instantiate(plotPrefab, new Vector3(-90, 0.5f, -300), Quaternion.Euler(0, 270, 0));
    //                     buyChurchObject.name = "church";
    //                 } else {
    //                     GameObject upgradeChurchObject = Instantiate(upgradeChurchPrefab, new Vector3(-120, 6, -300), Quaternion.Euler(-90, 0, 310));
    //                     upgradeChurchObject.name = "church";
    //                 }
    //             break;

    //             // case "guillotine":
    //             //     if (building.level == 0) {
    //             //         GameObject buyGuillotineObject = Instantiate(plotPrefab, new Vector3(262, 0.5f, -98), Quaternion.Euler(0, 270, 0));
    //             //         buyGuillotineObject.name = "guillotine";
    //             //     } else {
    //             //         GameObject upgradeGuillotineObject = Instantiate(upgradeGuillotinePrefab, new Vector3(245, 8.5f, -110), Quaternion.Euler(-90, 0, -75));
    //             //         upgradeGuillotineObject.name = "guillotine";
    //             //     }
    //             // break;

    //             case "farm":
    //                 if (building.level == 0) {
    //                     GameObject buyFarmObject = Instantiate(plotPrefab, new Vector3(-226, 0.5f, 305), Quaternion.Euler(0, 270, 0));
    //                     buyFarmObject.name = "farm";
    //                 } else {
    //                     GameObject upgradeFarmObject = Instantiate(upgradeFarmPrefab, new Vector3(-225, 3, 305), Quaternion.Euler(-90, 0, 0));
    //                     upgradeFarmObject.name = "farm";
    //                 }
    //             break;

    //             case "armorShop":
    //                 if (building.level == 0) {
    //                     GameObject buyArmorShopObject = Instantiate(plotPrefab, new Vector3(80, 0.5f, 155), Quaternion.Euler(0, 270, 0));
    //                     buyArmorShopObject.name = "armorShop";
    //                 } else {
    //                     GameObject upgradeArmorShopObject = Instantiate(upgradeArmorShopPrefab, new Vector3(80, 4, 145), Quaternion.Euler(-90, 0, 180));
    //                     upgradeArmorShopObject.name = "armorShop";
    //                 }
    //             break;

    //             case "chickenNest":
    //                 if (building.level == 0) {
    //                     GameObject buyChickenNestObject = Instantiate(plotPrefab, new Vector3(-250, 0.5f, 170), Quaternion.Euler(0, 270, 0));
    //                     buyChickenNestObject.name = "chickenNest";
    //                 } else {
    //                     GameObject upgradeChickenNestObject = Instantiate(upgradeChickenNestPrefab, new Vector3(-250, 0, 170), Quaternion.Euler(0, 270, 0));
    //                     upgradeChickenNestObject.name = "chickenNest";
    //                 }
    //             break;

    //             case "weaponShop":
    //                 if (building.level == 0) {
    //                     GameObject buyWeaponShopObject = Instantiate(plotPrefab, new Vector3(215, 0.5f, 70), Quaternion.Euler(0,270,0));
    //                     buyWeaponShopObject.name = "weaponShop";
    //                 } else {
    //                     GameObject upgradeWeaponShopObject = Instantiate(upgradeWeaponShopPrefab, new Vector3(215, 0, 70), Quaternion.Euler(0, 200,0));
    //                     upgradeWeaponShopObject.name = "weaponShop";
    //                 }
    //             break;

    //             case "foodShop":
    //                 if (building.level == 0) {
    //                     GameObject buyFoodShopObject = Instantiate(plotPrefab, new Vector3(-225, 0.5f, 470), Quaternion.Euler(0,270,0));
    //                     buyFoodShopObject.name = "foodShop";
    //                 } else {
    //                     GameObject upgradeFoodShopObject = Instantiate(upgradeFoodShopPrefab, new Vector3(-225, 3, 470), Quaternion.Euler(-90, 0, 145));
    //                     upgradeFoodShopObject.name = "foodShop";
    //                 }
    //             break;

    //             case "battleArena":
    //                 if (building.level == 0) {
    //                     GameObject buyBattleArenaObject = Instantiate(plotPrefab, new Vector3(110, 0.5f, -60), Quaternion.Euler(0,270,0));
    //                     buyBattleArenaObject.name = "battleArena";
    //                 } else {
    //                     GameObject upgradeBattleArenaObject = Instantiate(upgradeBattleArenaPrefab, new Vector3(110, 3, -60), Quaternion.Euler(-90, 0, 270));
    //                     upgradeBattleArenaObject.name = "battleArena";
    //                 }
    //             break;

    //             default:
    //             break;
    //         }
    //     }
    // }

    public BuildingScriptableObject[] getthing() {
        BuildingScriptableObject[] test = GameObject.FindObjectsOfType<BuildingScriptableObject>();

        Debug.Log(test.Length);
        return test;
    }

    void debug() {
        if (Input.GetKeyDown(KeyCode.T)) {
            // string today = System.DateTime.Now.ToString();
            // string tomorrow = System.DateTime.Parse(today).AddDays(1).ToString();

            // var diffInSeconds = (System.DateTime.Parse(tomorrow) - System.DateTime.Parse(today)).TotalSeconds;

            getthing();
        }

        // if (Input.GetKeyDown(KeyCode.R)) {
        //     Modifiers.globalMultiplier += 0.10f;
        //     Buildings.updateBuildingsActualProduction();
        // }

        // if (Input.GetKeyDown(KeyCode.E)) {
        //     Modifiers.globalMultiplier -= 0.10f;
        //     Buildings.updateBuildingsActualProduction();
        // }

        // if (Input.GetKeyDown(KeyCode.W)) {
        //     Debug.Log(Modifiers.globalMultiplier);
        // }

        // if (Input.GetKeyDown(KeyCode.Q)) {
        //     Value actualProduction = getBuildingTotalProduction();
        //     Debug.Log(actualProduction.value);
        //     Debug.Log(actualProduction.scale);
        // }
    }

    /**
     * Run through the employees array and return the sum of all employees actual production values
     */
    public Value getBuildingTotalProduction() {
        Value valueClass = new Value();

        foreach (Building building in Buildings.buildingsList) {
            if (building.level > 0) {
                valueClass.value += building.actualProduction.value;
                valueClass.scale = 0;
            }
        }

        return valueClass;
    }

    /**
     * Handles the transition in scale of values
     */
    public void checkForScaleChange() {
        if (Souls.totalSouls.value > 1000000) {
            Souls.totalSouls.value /= 1000000;
            Souls.totalSouls.scale++;
        }

        soulsText.text = "Souls: " + Souls.totalSouls.value.ToString("N2") + Currency.suifx[Souls.totalSouls.scale];
    }

    public void saveGame() {
        SaveController.saveGame();
    }

    public void loadGame() {
        Buildings.setBuildingsList();
        Debug.Log("LoadGame");
        PlayerData data = SaveController.loadGame();

        if (data != null) {
            Souls.totalSouls.value = data.totalSoulsValue;
            Souls.totalSouls.scale = data.totalSoulsScale;

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
            Debug.Log("No game to load");
            GameController.wishingWellLastCollectedTime = new System.DateTime(2000, 01, 01).ToString();
        }
    }

    void getOfflineEarnings() {
        string currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(lastTimeOnline)).TotalSeconds.ToString();
        Value actualProduction = getBuildingTotalProduction();
        offlineEarnings.value = double.Parse(diffInSeconds) * actualProduction.value;
        offlineEarnings.scale = actualProduction.scale;
        // offlineEarnings.multiplier = 2;

        Value valueClass = Currency.add(Souls.totalSouls.value, Souls.totalSouls.scale, offlineEarnings.value, offlineEarnings.scale);

        Souls.totalSouls.value = valueClass.value;
        Souls.totalSouls.scale = valueClass.scale;

        while (offlineEarnings.value > 1000000) {
            offlineEarnings.value /= 1000000;
            offlineEarnings.scale++;
        }
    }
}
