using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryController : MonoBehaviour {
    [SerializeField] private LibraryUpgradeScriptableObject goldMultiplierUpgradeScriptableObject;

    [SerializeField] private CurrencyController _currencyController;
    [SerializeField] private ModifierController _modifierController;
    [SerializeField] private GameMath _gameMath;

    public void buyGoldMultiplier() {
        Value valueClass = new Value();

        valueClass = Currency.subtract(_currencyController.getWisdom(), goldMultiplierUpgradeScriptableObject.nextCost);

        if (valueClass == null) {
            Debug.Log("Valor negativo aqui (valor de custo do próximo upgrade é muito caro)");
            // return false;
        } else {
            _currencyController.setWisdom(valueClass);
            goldMultiplierUpgradeScriptableObject.level++;

            goldMultiplierUpgradeScriptableObject.multiplierValue = goldMultiplierUpgradeScriptableObject.level >= 10
                                                                    ? double.Parse("1," + goldMultiplierUpgradeScriptableObject.level)
                                                                    : double.Parse("1,0" + goldMultiplierUpgradeScriptableObject.level);

            Value upgradeNextCost = new Value();
            upgradeNextCost = _gameMath.getNextUpgradeCost(
                goldMultiplierUpgradeScriptableObject.initialCost,
                goldMultiplierUpgradeScriptableObject.growthRate,
                goldMultiplierUpgradeScriptableObject.level,
                goldMultiplierUpgradeScriptableObject.nextCost.scale
            );

            goldMultiplierUpgradeScriptableObject.nextCost.value = upgradeNextCost.value;
            goldMultiplierUpgradeScriptableObject.nextCost.scale = upgradeNextCost.scale;

            _modifierController.setGoldMultiplier(goldMultiplierUpgradeScriptableObject.multiplierValue);
        }
    }

    public void buyWisdomMultiplier() {

    }

    public void buyWishingWellMultiplier() {

    }
}
