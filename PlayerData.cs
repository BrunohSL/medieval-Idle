using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {
    public double totalSoulsValue;
    public int totalSoulsScale;

    public string lastTimeOnline;
    public string wishingWellLastCollectedTime;

    public double multiplier;

    public int[] buildingLevel = new int[12];
    public double[] buildingInitialProduction = new double[12];
    public double[] buildingInitialCost = new double[12];
    public double[] buildingGrowthRate = new double[12];
    public double[] buildingActualProductionValue = new double[12];
    public int[] buildingActualProductionScale = new int[12];
    public double[] buildingNextProductionValue = new double[12];
    public int[] buildingNextProductionScale = new int[12];
    public double[] buildingNextCostValue = new double[12];
    public int[] buildingNextCostScale = new int[12];
    public double[] buildingMultiplier = new double[12];

    public PlayerData() {
        totalSoulsValue = getGoldValue().value;
        totalSoulsScale = getGoldValue().scale;

        lastTimeOnline = System.DateTime.Now.ToString();
        wishingWellLastCollectedTime = GameController.wishingWellLastCollectedTime;

        multiplier = Modifiers.globalMultiplier;

        int counter = 0;

        foreach (Building building in Buildings.buildingsList) {
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

    private Value getGoldValue() {
        return GameObject.FindGameObjectWithTag("CurrencyController").GetComponent<GoldController>().getGold();
    }
}
