using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMath : MonoBehaviour {
    [SerializeField] private ModifierController _modifierController;

    /// <summary>
    /// Generate the next production value.
    /// </summary>
    /// <param name="initialProduction">The <see cref="System.Double"/> instance that represents the original production value, the initial production value.</param>
    /// <param name="level">The <see cref="System.Int32"/> instance that represents the level of the building or upgrde.</param>
    /// <param name="buildingMultiplier">The <see cref="System.Double"/> instance that represents the value of all bonuses multipliers that increases generation.</param>
    /// <returns>The next production value in a instance of <see cref="Value"/> Class.</returns>
    public string getNextProductionRate(double initialProduction, int level, double buildingMultiplier = 1, int buildingRankMultiplier = 1) {
        double nextProductionRate = (((initialProduction * level) * buildingMultiplier) * _modifierController.getGoldMultiplier()) * _modifierController.getGlobalMultiplier() * buildingRankMultiplier;

        return nextProductionRate.ToString("N2");
    }

    /// <summary>
    /// Generate the next upgrade cost.
    /// </summary>
    /// <param name="initialCost">The <see cref="System.Double"/> instance that represents the original cost value, the initial cost value.</param>
    /// <param name="growthRate">The <see cref="System.Int32"/> instance that represents the coefficient used to increase the next upgrade value for each level up.</param>
    /// <param name="level">The <see cref="System.Int32"/> instance that represents the level of the building or upgrde.</param>
    /// <param name="scale">The <see cref="System.Double"/> instance that represents the scale value of the next upgarde cost.</param>
    /// <returns>The next cost value in a instance of <see cref="Value"/> Class.</returns>
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
        valueClass = Currency.multiply(valueClass, _modifierController.getWisdomMultiplier());
        valueClass = Currency.multiply(valueClass, _modifierController.getGlobalMultiplier());

        return valueClass;
    }
}
