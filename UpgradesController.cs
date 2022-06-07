using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesController : MonoBehaviour {
    public LibraryUpgradeScriptableObject globalUpgrade;
    public LibraryUpgradeScriptableObject goldUpgrade;
    public LibraryUpgradeScriptableObject wisdomUpgrade;
    public LibraryUpgradeScriptableObject wishingWellUpgrade;
    public LibraryUpgradeScriptableObject housesUpgrade;
    public LibraryUpgradeScriptableObject graveyardUpgrade;
    public LibraryUpgradeScriptableObject churchUpgrade;
    public LibraryUpgradeScriptableObject farmUpgrade;
    public LibraryUpgradeScriptableObject animalFarmUpgrade;
    public LibraryUpgradeScriptableObject armorShopUpgrade;
    public LibraryUpgradeScriptableObject weaponShopUpgrade;
    public LibraryUpgradeScriptableObject foodShopUpgrade;

    public void setUpgradesOriginalValues() {
        // --------  GOLD  -------- //
        goldUpgrade.level = 0;
        goldUpgrade.maxLevel = 100;
        goldUpgrade.multiplierValue = 1;
        goldUpgrade.initialCost = 2;
        goldUpgrade.growthRate = 1.5f;

        goldUpgrade.nextCost.value = 2;
        goldUpgrade.nextCost.scale = 0;

        // --------  WISDOM  -------- //
        wisdomUpgrade.level = 0;
        wisdomUpgrade.maxLevel = 100;
        wisdomUpgrade.multiplierValue = 1;
        wisdomUpgrade.initialCost = 2;
        wisdomUpgrade.growthRate = 1.5f;

        wisdomUpgrade.nextCost.value = 2;
        wisdomUpgrade.nextCost.scale = 0;

        // --------  GLOBAL  -------- //
        globalUpgrade.level = 0;
        globalUpgrade.maxLevel = 100;
        globalUpgrade.multiplierValue = 1;
        globalUpgrade.initialCost = 2;
        globalUpgrade.growthRate = 1.5f;

        globalUpgrade.nextCost.value = 2;
        globalUpgrade.nextCost.scale = 0;

        // --------  WISHING WELL  -------- //
        wishingWellUpgrade.level = 0;
        wishingWellUpgrade.maxLevel = 100;
        wishingWellUpgrade.multiplierValue = 1;
        wishingWellUpgrade.initialCost = 2;
        wishingWellUpgrade.growthRate = 1.5f;

        wishingWellUpgrade.nextCost.value = 2;
        wishingWellUpgrade.nextCost.scale = 0;

        // --------  HOUSES  -------- //
        housesUpgrade.level = 0;
        housesUpgrade.maxLevel = 100;
        housesUpgrade.multiplierValue = 1;
        housesUpgrade.initialCost = 2;
        housesUpgrade.growthRate = 1.5f;

        housesUpgrade.nextCost.value = 2;
        housesUpgrade.nextCost.scale = 0;

        // --------  FARM  -------- //
        farmUpgrade.level = 0;
        farmUpgrade.maxLevel = 100;
        farmUpgrade.multiplierValue = 1;
        farmUpgrade.initialCost = 2;
        farmUpgrade.growthRate = 1.5f;

        farmUpgrade.nextCost.value = 2;
        farmUpgrade.nextCost.scale = 0;

        // --------  ANIMAL FARM  -------- //
        animalFarmUpgrade.level = 0;
        animalFarmUpgrade.maxLevel = 100;
        animalFarmUpgrade.multiplierValue = 1;
        animalFarmUpgrade.initialCost = 2;
        animalFarmUpgrade.growthRate = 1.5f;

        animalFarmUpgrade.nextCost.value = 2;
        animalFarmUpgrade.nextCost.scale = 0;

        // --------  FOOD SHOP  -------- //
        foodShopUpgrade.level = 0;
        foodShopUpgrade.maxLevel = 100;
        foodShopUpgrade.multiplierValue = 1;
        foodShopUpgrade.initialCost = 2;
        foodShopUpgrade.growthRate = 1.5f;

        foodShopUpgrade.nextCost.value = 2;
        foodShopUpgrade.nextCost.scale = 0;

        // --------  ARMOR SHOP  -------- //
        armorShopUpgrade.level = 0;
        armorShopUpgrade.maxLevel = 100;
        armorShopUpgrade.multiplierValue = 1;
        armorShopUpgrade.initialCost = 2;
        armorShopUpgrade.growthRate = 1.5f;

        armorShopUpgrade.nextCost.value = 2;
        armorShopUpgrade.nextCost.scale = 0;

        // --------  WEAPON SHOP  -------- //
        weaponShopUpgrade.level = 0;
        weaponShopUpgrade.maxLevel = 100;
        weaponShopUpgrade.multiplierValue = 1;
        weaponShopUpgrade.initialCost = 2;
        weaponShopUpgrade.growthRate = 1.5f;

        weaponShopUpgrade.nextCost.value = 2;
        weaponShopUpgrade.nextCost.scale = 0;

        // --------  GRAVEYARD  -------- //
        graveyardUpgrade.level = 0;
        graveyardUpgrade.maxLevel = 100;
        graveyardUpgrade.multiplierValue = 1;
        graveyardUpgrade.initialCost = 2;
        graveyardUpgrade.growthRate = 1.5f;

        graveyardUpgrade.nextCost.value = 2;
        graveyardUpgrade.nextCost.scale = 0;

        // --------  CHURCH  -------- //
        churchUpgrade.level = 0;
        churchUpgrade.maxLevel = 100;
        churchUpgrade.multiplierValue = 1;
        churchUpgrade.initialCost = 2;
        churchUpgrade.growthRate = 1.5f;

        churchUpgrade.nextCost.value = 2;
        churchUpgrade.nextCost.scale = 0;
    }
}
