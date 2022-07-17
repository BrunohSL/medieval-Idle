using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    [SerializeField] private GameMath _gameMath;
    [SerializeField] private BuildingScriptableObject _buildingScriptableObject;
    [SerializeField] private CurrencyController _currencyController;
    [SerializeField] private GameObject _buySign;

    public BuildingScriptableObject getScriptableObject() {
        return _buildingScriptableObject;
    }

    public Value getUpgradeCost() {
        return _buildingScriptableObject.nextCost;
    }

    // public Value getNextProduction() {
    //     return new Value();
    // }

    // public Value getActualProduction() {
    //     return new Value();
    // }

    public void activateBuySign() {
        _buySign.SetActive(true);
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
        Material material = this.GetComponent<Renderer>().material;

        valueClass = Currency.subtract(new Value(_currencyController.getGold().value, _currencyController.getGold().scale), _buildingScriptableObject.nextCost);

        if (valueClass == null) {
            Debug.Log("Valor negativo aqui (valor de custo do próximo upgrade é muito caro)");
            return false;
        } else {
            _currencyController.setGold(valueClass);

            if (_buildingScriptableObject.level == 0) {
                material.color = _buildingScriptableObject.originalColor;
                _buildingScriptableObject.actualColor = material.color;
                _buySign.SetActive(false);
            }

            _buildingScriptableObject.actualProduction.value = _buildingScriptableObject.nextProduction.value;
            _buildingScriptableObject.level++;

            _buildingScriptableObject.nextProduction.value = double.Parse(
                _gameMath.getNextProductionRate(
                    _buildingScriptableObject.initialProduction,
                    _buildingScriptableObject.level,
                    _buildingScriptableObject.buildingMultiplier,
                    BuildingTierlist.rankMultiplier[_buildingScriptableObject.tierlistRank]
                )
            );

            Value buildingNextCost = new Value();
            buildingNextCost = _gameMath.getNextUpgradeCost(
                _buildingScriptableObject.initialCost,
                _buildingScriptableObject.growthRate,
                _buildingScriptableObject.level,
                _buildingScriptableObject.nextCost.scale
            );

            _buildingScriptableObject.nextCost.value = buildingNextCost.value;
            _buildingScriptableObject.nextCost.scale = buildingNextCost.scale;

            if (_buildingScriptableObject.level >= BuildingTierlist.rank[_buildingScriptableObject.tierlistRank]) {
                _buildingScriptableObject.tierlistRank++;
            }

            return true;
        }
    }
}
