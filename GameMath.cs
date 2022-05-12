using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMath : MonoBehaviour {
    /**
     * Generate the next upgrade cost of click or employee
     *
     * @param double initialProduction production original value, the initial value
     * @param double level             click level or employee level
     * @param double multiplier        value of all bonuses that increases soul generation
     *
     * @return string nextProductionRate
     */
    public string getNextProductionRate(double initialProduction, int level, double globalMultiplier = 1, double buildingMultiplier = 1) {
        double nextProductionRate = ((initialProduction * level) * globalMultiplier) * buildingMultiplier;

        return nextProductionRate.ToString("N2");
    }

    /**
     * Generate the next upgrade cost of click or employee
     *
     * @param double initialCost original cost of click or employee, the initial value
     * @param double growthRate  the coefficient used to increase the next upgrade value for each level up
     * @param double level       click level or employee level
     *
     * @return Value nextCost
     */
    public Value getNextUpgradeCost(double initialCost, double growthRate, int level, int scale) {
        Value nextCost = new Value();
        nextCost.scale = 0;

        double nextCostValue = initialCost * (Mathf.Pow((float)growthRate, level));
        nextCost.value = nextCostValue;

        if (nextCost.value > 1000000) {
            Debug.Log("Subiu scale do próximo custo");
            nextCost.value /= 1000000;
            nextCost.scale++;
        }

        return nextCost;
    }

    /**
     * Apenas se valor de gold for maior que 100.000
     */
    public Value getPrestige(Value currentGold) {
        Value valueClass = new Value();

        valueClass = Currency.divide(currentGold, 100000);

        valueClass = Currency.multiply(valueClass, 1);

        return valueClass;
    }
}
