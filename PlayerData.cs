﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public double totalGoldValue;
    public int totalGoldScale;
    public double totalWisdomValue;
    public int totalWisdomScale;

    public string lastTimeOnline;
    public string wishingWellLastCollectedTime;

    public double multiplier;

    public int[] buildingLevel = new int[8];
    public double[] buildingInitialProduction = new double[8];
    public double[] buildingInitialCost = new double[8];
    public double[] buildingGrowthRate = new double[8];
    public double[] buildingActualProductionValue = new double[8];
    public int[] buildingActualProductionScale = new int[8];
    public double[] buildingNextProductionValue = new double[8];
    public int[] buildingNextProductionScale = new int[8];
    public double[] buildingNextCostValue = new double[8];
    public int[] buildingNextCostScale = new int[8];
    public double[] buildingMultiplier = new double[8];

    public PlayerData() {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        List<BuildingScriptableObject> goldGeneratorBuildingsScriptableObject = gameController.GetComponent<GameController>().getGoldGeneratorBuildings();

        CurrencyController currencyController = gameController.GetComponent<CurrencyController>();
        ModifierController modifierController = gameController.GetComponent<ModifierController>();

        totalGoldValue = currencyController.getGold().value;
        totalGoldScale = currencyController.getGold().scale;
        totalWisdomValue = currencyController.getWisdom().value;
        totalWisdomScale = currencyController.getWisdom().scale;

        lastTimeOnline = System.DateTime.Now.ToString();
        wishingWellLastCollectedTime = GameController.wishingWellLastCollectedTime;

        multiplier = modifierController.getGlobalMultiplier();

        int counter = 0;

        foreach (BuildingScriptableObject building in goldGeneratorBuildingsScriptableObject) {
            buildingLevel[counter] = building.level;
            buildingInitialProduction[counter] = building.initialProduction;
            buildingInitialCost[counter] = building.initialCost;
            buildingGrowthRate[counter] = building.growthRate;
            buildingActualProductionValue[counter] = building.actualProduction.value;
            buildingActualProductionScale[counter] = building.actualProduction.scale;
            buildingNextProductionValue[counter] = building.nextProduction.value;
            buildingNextProductionScale[counter] = building.nextProduction.scale;
            buildingNextCostValue[counter] = building.nextCost.value;
            buildingNextCostScale[counter] = building.nextCost.scale;
            buildingMultiplier[counter] = building.buildingMultiplier;
            counter++;
        }
    }
}
