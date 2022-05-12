using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    [SerializeField] private GameMath gameMath;
    [SerializeField] private BuildingScriptableObject buildingScriptableObject;
    [SerializeField] private CurrencyController _currencyController;

    public BuildingScriptableObject getScriptableObject() {
        return buildingScriptableObject;
    }

    public Value getUpgradeCost() {
        return new Value();
    }

    public Value getNextProduction() {
        return new Value();
    }

    public Value getActualProduction() {
        return new Value();
    }

    /**
     * Upgrade building button behavior
     *
     * First check if the player can buy the next upgrade,
     * Then reduces the cost of the upgrade from your number of souls,
     * Then set the actual production as the next production value
     * Then increase the player level by one
     * Then generates the new next production value
     * Then generates the new next cost for the upgrade
     */
    public bool levelUpBuilding() {
        Value valueClass = new Value();

        valueClass = Currency.subtract(_currencyController.getGold().value, _currencyController.getGold().scale, buildingScriptableObject.nextCost.value, buildingScriptableObject.nextCost.scale);

        if (valueClass == null) {
            Debug.Log("Valor negativo aqui (valor de custo do próximo upgrade é muito caro)");
            return false;
        } else {
            _currencyController.setGold(valueClass);

            buildingScriptableObject.actualProduction.value = buildingScriptableObject.nextProduction.value;
            buildingScriptableObject.level++;

            buildingScriptableObject.nextProduction.value = double.Parse(
                gameMath.getNextProductionRate(
                    buildingScriptableObject.initialProduction,
                    buildingScriptableObject.level,
                    Modifiers.globalMultiplier,
                    buildingScriptableObject.tierlist.rankMultiplier[buildingScriptableObject.tierlistRank]
                )
            );

            Value buildingNextCost = new Value();
            buildingNextCost = gameMath.getNextUpgradeCost(
                buildingScriptableObject.initialCost,
                buildingScriptableObject.growthRate,
                buildingScriptableObject.level,
                buildingScriptableObject.nextCost.scale
            );

            buildingScriptableObject.nextCost.value = buildingNextCost.value;
            buildingScriptableObject.nextCost.scale = buildingNextCost.scale;

            if (buildingScriptableObject.level >= buildingScriptableObject.tierlist.rank[buildingScriptableObject.tierlistRank]) {
                buildingScriptableObject.tierlistRank++;
            }

            return true;
        }
    }
}
