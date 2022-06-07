using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBuildingsController : MonoBehaviour {
    public BuildingScriptableObject houseScriptableObject;
    public BuildingScriptableObject farmScriptableObject;
    public BuildingScriptableObject animalFarmScriptableObject;
    public BuildingScriptableObject foodShopScriptableObject;
    public BuildingScriptableObject armorShopScriptableObject;
    public BuildingScriptableObject weaponShopScriptableObject;
    public BuildingScriptableObject graveyardScriptableObject;
    public BuildingScriptableObject churchScriptableObject;

    public void setBuildingsOriginalValues() {
        Debug.Log("Setando valores");
        // --------  HOUSES  -------- //
        houseScriptableObject.level = 0;
        houseScriptableObject.initialProduction = 2;
        houseScriptableObject.initialCost = 5;
        houseScriptableObject.growthRate = 1.07f;

        houseScriptableObject.actualProduction.value = 0;
        houseScriptableObject.actualProduction.scale = 0;

        houseScriptableObject.nextProduction.value = 2;
        houseScriptableObject.nextProduction.scale = 0;

        houseScriptableObject.nextCost.value = 5;
        houseScriptableObject.nextCost.scale = 0;

        houseScriptableObject.tierlistRank = 0;
        houseScriptableObject.buildingMultiplier = 1;

        // --------  FARM  -------- //
        farmScriptableObject.level = 0;
        farmScriptableObject.initialProduction = 20;
        farmScriptableObject.initialCost = 60;
        farmScriptableObject.growthRate = 1.15f;

        farmScriptableObject.actualProduction.value = 0;
        farmScriptableObject.actualProduction.scale = 0;

        farmScriptableObject.nextProduction.value = 20;
        farmScriptableObject.nextProduction.scale = 0;

        farmScriptableObject.nextCost.value = 60;
        farmScriptableObject.nextCost.scale = 0;

        farmScriptableObject.tierlistRank = 0;
        farmScriptableObject.buildingMultiplier = 1;

        // --------  ANIMAL FARM  -------- //
        animalFarmScriptableObject.level = 0;
        animalFarmScriptableObject.initialProduction = 90;
        animalFarmScriptableObject.initialCost = 720;
        animalFarmScriptableObject.growthRate = 1.14f;

        animalFarmScriptableObject.actualProduction.value = 0;
        animalFarmScriptableObject.actualProduction.scale = 0;

        animalFarmScriptableObject.nextProduction.value = 90;
        animalFarmScriptableObject.nextProduction.scale = 0;

        animalFarmScriptableObject.nextCost.value = 720;
        animalFarmScriptableObject.nextCost.scale = 0;

        animalFarmScriptableObject.tierlistRank = 0;
        animalFarmScriptableObject.buildingMultiplier = 1;

        // --------  FOOD SHOP  -------- //
        foodShopScriptableObject.level = 0;
        foodShopScriptableObject.initialProduction = 360;
        foodShopScriptableObject.initialCost = 8640;
        foodShopScriptableObject.growthRate = 1.13f;

        foodShopScriptableObject.actualProduction.value = 0;
        foodShopScriptableObject.actualProduction.scale = 0;

        foodShopScriptableObject.nextProduction.value = 360;
        foodShopScriptableObject.nextProduction.scale = 0;

        foodShopScriptableObject.nextCost.value = 8640;
        foodShopScriptableObject.nextCost.scale = 0;

        foodShopScriptableObject.tierlistRank = 0;
        foodShopScriptableObject.buildingMultiplier = 1;

        // --------  ARMOR SHOP  -------- //
        armorShopScriptableObject.level = 0;
        armorShopScriptableObject.initialProduction = 2160;
        armorShopScriptableObject.initialCost = 103680;
        armorShopScriptableObject.growthRate = 1.12f;

        armorShopScriptableObject.actualProduction.value = 0;
        armorShopScriptableObject.actualProduction.scale = 0;

        armorShopScriptableObject.nextProduction.value = 2160;
        armorShopScriptableObject.nextProduction.scale = 0;

        armorShopScriptableObject.nextCost.value = 103680;
        armorShopScriptableObject.nextCost.scale = 0;

        armorShopScriptableObject.tierlistRank = 0;
        armorShopScriptableObject.buildingMultiplier = 1;

        // --------  WEAPON SHOP  -------- //
        weaponShopScriptableObject.level = 0;
        weaponShopScriptableObject.initialProduction = 12240;
        weaponShopScriptableObject.initialCost = 1244160;
        weaponShopScriptableObject.growthRate = 1.11f;

        weaponShopScriptableObject.actualProduction.value = 0;
        weaponShopScriptableObject.actualProduction.scale = 0;

        weaponShopScriptableObject.nextProduction.value = 12240;
        weaponShopScriptableObject.nextProduction.scale = 0;

        weaponShopScriptableObject.nextCost.value = 1.244160f;
        weaponShopScriptableObject.nextCost.scale = 1;

        weaponShopScriptableObject.tierlistRank = 0;
        weaponShopScriptableObject.buildingMultiplier = 1;

        // --------  GRAVEYARD  -------- //
        graveyardScriptableObject.level = 0;
        graveyardScriptableObject.initialProduction = 57840;
        graveyardScriptableObject.initialCost = 14929920;
        graveyardScriptableObject.growthRate = 1.10f;

        graveyardScriptableObject.actualProduction.value = 0;
        graveyardScriptableObject.actualProduction.scale = 0;

        graveyardScriptableObject.nextProduction.value = 57840;
        graveyardScriptableObject.nextProduction.scale = 0;

        graveyardScriptableObject.nextCost.value = 14.929920f;
        graveyardScriptableObject.nextCost.scale = 1;

        graveyardScriptableObject.tierlistRank = 0;
        graveyardScriptableObject.buildingMultiplier = 1;

        // --------  CHURCH  -------- //
        churchScriptableObject.level = 0;
        churchScriptableObject.initialProduction = 247660;
        churchScriptableObject.initialCost = 179159040;
        churchScriptableObject.growthRate = 1.09f;

        churchScriptableObject.actualProduction.value = 0;
        churchScriptableObject.actualProduction.scale = 0;

        churchScriptableObject.nextProduction.value = 247660;
        churchScriptableObject.nextProduction.scale = 0;

        churchScriptableObject.nextCost.value = 179.159040f;
        churchScriptableObject.nextCost.scale = 1;

        churchScriptableObject.tierlistRank = 0;
        churchScriptableObject.buildingMultiplier = 1;
    }
}
