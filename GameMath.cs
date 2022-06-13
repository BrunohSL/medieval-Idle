using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMath : MonoBehaviour {
    [SerializeField] private ModifierController _modifierController;

    /**
     * Generate the next upgrade cost of click or employee
     *
     * @param double initialProduction production original value, the initial value
     * @param double level             click level or employee level
     * @param double multiplier        value of all bonuses that increases soul generation
     *
     * @return string nextProductionRate
     */
    public string getNextProductionRate(double initialProduction, int level, double buildingMultiplier = 1) {
        Debug.Log("initialProduction: " + initialProduction.ToString());
        Debug.Log("level: " + level.ToString());
        Debug.Log("buildingMultiplier: " + buildingMultiplier.ToString());
        Debug.Log("goldMultiplier: " + _modifierController.getGoldMultiplier().ToString());
        Debug.Log("globalMultiplier: " + _modifierController.getGlobalMultiplier().ToString());
        double nextProductionRate = (((initialProduction * level) * buildingMultiplier) * _modifierController.getGoldMultiplier()) * _modifierController.getGlobalMultiplier();

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

        Debug.Log(valueClass.value);
        valueClass = Currency.multiply(valueClass, _modifierController.getWisdomMultiplier());
        Debug.Log(valueClass.value);
        valueClass = Currency.multiply(valueClass, _modifierController.getGlobalMultiplier());
        Debug.Log(valueClass.value);

        return valueClass;
    }
}
