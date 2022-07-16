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

    [SerializeField] private GameObject houseObj;
    [SerializeField] private GameObject farmObj;
    [SerializeField] private GameObject animalFarmObj;
    [SerializeField] private GameObject foodShopObj;
    [SerializeField] private GameObject armorShopObj;
    [SerializeField] private GameObject weaponShopObj;
    [SerializeField] private GameObject graveyardObj;
    [SerializeField] private GameObject churchObj;
    private Material materialColor;

    public void setBuildingsColor() {
        houseObj.GetComponent<Renderer>().material.color = houseScriptableObject.actualColor;
        farmObj.GetComponent<Renderer>().material.color = farmScriptableObject.actualColor;
        animalFarmObj.GetComponent<Renderer>().material.color = animalFarmScriptableObject.actualColor;
        foodShopObj.GetComponent<Renderer>().material.color = foodShopScriptableObject.actualColor;
        armorShopObj.GetComponent<Renderer>().material.color = armorShopScriptableObject.actualColor;
        weaponShopObj.GetComponent<Renderer>().material.color = weaponShopScriptableObject.actualColor;
        graveyardObj.GetComponent<Renderer>().material.color = graveyardScriptableObject.actualColor;
        churchObj.GetComponent<Renderer>().material.color = churchScriptableObject.actualColor;
    }

    public void setBuildingsOriginalColor() {
        houseScriptableObject.originalColor = houseObj.GetComponent<Renderer>().material.color;
        farmScriptableObject.originalColor = farmObj.GetComponent<Renderer>().material.color;
        animalFarmScriptableObject.originalColor = animalFarmObj.GetComponent<Renderer>().material.color;
        foodShopScriptableObject.originalColor = foodShopObj.GetComponent<Renderer>().material.color;
        armorShopScriptableObject.originalColor = armorShopObj.GetComponent<Renderer>().material.color;
        weaponShopScriptableObject.originalColor = weaponShopObj.GetComponent<Renderer>().material.color;
        graveyardScriptableObject.originalColor = graveyardObj.GetComponent<Renderer>().material.color;
        churchScriptableObject.originalColor = churchObj.GetComponent<Renderer>().material.color;
    }

    public void setBuildingsOriginalValues() {
        Debug.Log("Setando valores");
        // --------  HOUSES  -------- //
        houseScriptableObject.level = 0;
        houseScriptableObject.initialProduction = 2d;
        houseScriptableObject.initialCost = 5d;
        houseScriptableObject.growthRate = 1.07d;

        houseScriptableObject.actualProduction.value = 0d;
        houseScriptableObject.actualProduction.scale = 0;

        houseScriptableObject.nextProduction.value = 2d;
        houseScriptableObject.nextProduction.scale = 0;

        houseScriptableObject.nextCost.value = 5;
        houseScriptableObject.nextCost.scale = 0;

        houseScriptableObject.tierlistRank = 0;
        houseScriptableObject.buildingMultiplier = 1;

        houseScriptableObject.actualColor = Color.black;
        houseObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  FARM  -------- //
        farmScriptableObject.level = 0;
        farmScriptableObject.initialProduction = 20d;
        farmScriptableObject.initialCost = 60d;
        farmScriptableObject.growthRate = 1.15d;

        farmScriptableObject.actualProduction.value = 0d;
        farmScriptableObject.actualProduction.scale = 0;

        farmScriptableObject.nextProduction.value = 20d;
        farmScriptableObject.nextProduction.scale = 0;

        farmScriptableObject.nextCost.value = 60;
        farmScriptableObject.nextCost.scale = 0;

        farmScriptableObject.tierlistRank = 0;
        farmScriptableObject.buildingMultiplier = 1;

        farmScriptableObject.actualColor = Color.black;
        farmObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  ANIMAL FARM  -------- //
        animalFarmScriptableObject.level = 0;
        animalFarmScriptableObject.initialProduction = 90d;
        animalFarmScriptableObject.initialCost = 720d;
        animalFarmScriptableObject.growthRate = 1.14d;

        animalFarmScriptableObject.actualProduction.value = 0d;
        animalFarmScriptableObject.actualProduction.scale = 0;

        animalFarmScriptableObject.nextProduction.value = 90d;
        animalFarmScriptableObject.nextProduction.scale = 0;

        animalFarmScriptableObject.nextCost.value = 720;
        animalFarmScriptableObject.nextCost.scale = 0;

        animalFarmScriptableObject.tierlistRank = 0;
        animalFarmScriptableObject.buildingMultiplier = 1;

        animalFarmScriptableObject.actualColor = Color.black;
        animalFarmObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  FOOD SHOP  -------- //
        foodShopScriptableObject.level = 0;
        foodShopScriptableObject.initialProduction = 360d;
        foodShopScriptableObject.initialCost = 8640d;
        foodShopScriptableObject.growthRate = 1.13d;

        foodShopScriptableObject.actualProduction.value = 0d;
        foodShopScriptableObject.actualProduction.scale = 0;

        foodShopScriptableObject.nextProduction.value = 360d;
        foodShopScriptableObject.nextProduction.scale = 0;

        foodShopScriptableObject.nextCost.value = 8640;
        foodShopScriptableObject.nextCost.scale = 0;

        foodShopScriptableObject.tierlistRank = 0;
        foodShopScriptableObject.buildingMultiplier = 1;

        foodShopScriptableObject.actualColor = Color.black;
        foodShopObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  ARMOR SHOP  -------- //
        armorShopScriptableObject.level = 0;
        armorShopScriptableObject.initialProduction = 2160d;
        armorShopScriptableObject.initialCost = 103680d;
        armorShopScriptableObject.growthRate = 1.12d;

        armorShopScriptableObject.actualProduction.value = 0d;
        armorShopScriptableObject.actualProduction.scale = 0;

        armorShopScriptableObject.nextProduction.value = 2160d;
        armorShopScriptableObject.nextProduction.scale = 0;

        armorShopScriptableObject.nextCost.value = 103680;
        armorShopScriptableObject.nextCost.scale = 0;

        armorShopScriptableObject.tierlistRank = 0;
        armorShopScriptableObject.buildingMultiplier = 1;

        armorShopScriptableObject.actualColor = Color.black;
        armorShopObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  WEAPON SHOP  -------- //
        weaponShopScriptableObject.level = 0;
        weaponShopScriptableObject.initialProduction = 12240d;
        weaponShopScriptableObject.initialCost = 1244160d;
        weaponShopScriptableObject.growthRate = 1.11d;

        weaponShopScriptableObject.actualProduction.value = 0d;
        weaponShopScriptableObject.actualProduction.scale = 0;

        weaponShopScriptableObject.nextProduction.value = 12240d;
        weaponShopScriptableObject.nextProduction.scale = 0;

        weaponShopScriptableObject.nextCost.value = 1244160d;
        weaponShopScriptableObject.nextCost.scale = 1;

        weaponShopScriptableObject.tierlistRank = 0;
        weaponShopScriptableObject.buildingMultiplier = 1;

        weaponShopScriptableObject.actualColor = Color.black;
        weaponShopObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  GRAVEYARD  -------- //
        graveyardScriptableObject.level = 0;
        graveyardScriptableObject.initialProduction = 57840d;
        graveyardScriptableObject.initialCost = 14929920d;
        graveyardScriptableObject.growthRate = 1.10d;

        graveyardScriptableObject.actualProduction.value = 0d;
        graveyardScriptableObject.actualProduction.scale = 0;

        graveyardScriptableObject.nextProduction.value = 57840d;
        graveyardScriptableObject.nextProduction.scale = 0;

        graveyardScriptableObject.nextCost.value = 14929920d;
        graveyardScriptableObject.nextCost.scale = 1;

        graveyardScriptableObject.tierlistRank = 0;
        graveyardScriptableObject.buildingMultiplier = 1;

        graveyardScriptableObject.actualColor = Color.black;
        graveyardObj.GetComponent<Renderer>().material.color = Color.black;

        // --------  CHURCH  -------- //
        churchScriptableObject.level = 0;
        churchScriptableObject.initialProduction = 247660d;
        churchScriptableObject.initialCost = 179159040d;
        churchScriptableObject.growthRate = 1.09d;

        churchScriptableObject.actualProduction.value = 0d;
        churchScriptableObject.actualProduction.scale = 0;

        churchScriptableObject.nextProduction.value = 247660d;
        churchScriptableObject.nextProduction.scale = 0;

        churchScriptableObject.nextCost.value = 179159040d;
        churchScriptableObject.nextCost.scale = 1;

        churchScriptableObject.tierlistRank = 0;
        churchScriptableObject.buildingMultiplier = 1;

        churchScriptableObject.actualColor = Color.black;
        churchObj.GetComponent<Renderer>().material.color = Color.black;
    }
}
