using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryController : MonoBehaviour {
    [SerializeField] private CurrencyController _currencyController;
    [SerializeField] private ModifierController _modifierController;
    [SerializeField] private GoldBuildingsController _goldBuildingsController;
    [SerializeField] private GameMath _gameMath;

    public void buyLibraryUpgrade(LibraryUpgradeScriptableObject libraryUpgradeScriptableObject) {
        Value valueClass = new Value();

        valueClass = Currency.subtract(_currencyController.getWisdom(), libraryUpgradeScriptableObject.nextCost);

        if (valueClass == null) {
            Debug.Log("Valor negativo aqui (valor de custo do próximo upgrade é muito caro)");
            // return false;
        } else {
            _currencyController.setWisdom(valueClass);
            libraryUpgradeScriptableObject.level++;

            libraryUpgradeScriptableObject.multiplierValue = libraryUpgradeScriptableObject.level >= 10
                                                                ? double.Parse("1," + libraryUpgradeScriptableObject.level)
                                                                : double.Parse("1,0" + libraryUpgradeScriptableObject.level);

            switch (libraryUpgradeScriptableObject.name) {
                case "HousesUpgrade":
                Debug.Log("Setando house multiplier");
                    _goldBuildingsController.houseScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "FarmUpgrade":
                    _goldBuildingsController.farmScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "AnimalFarmUpgrade":
                    _goldBuildingsController.animalFarmScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "FoodShopUpgrade":
                    _goldBuildingsController.foodShopScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "ArmorShopUpgrade":
                    _goldBuildingsController.armorShopScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "WeaponShopUpgrade":
                    _goldBuildingsController.weaponShopScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "GraveyardUpgrade":
                    _goldBuildingsController.graveyardScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;
                case "ChurchUpgrade":
                    _goldBuildingsController.churchScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                    break;

                case "GoldUpgrade":
                    _modifierController.setGoldMultiplier(libraryUpgradeScriptableObject.multiplierValue);
                    break;
                case "WisdomUpgrade":
                    _modifierController.setWisdomMultiplier(libraryUpgradeScriptableObject.multiplierValue);
                    break;
                case "GlobalUpgrade":
                Debug.Log("setando global multiplier");
                    _modifierController.setGlobalMultiplier(libraryUpgradeScriptableObject.multiplierValue);
                    break;
                // case "WishingWellUpgrade":
                //     _goldBuildingsController.churchScriptableObject.buildingMultiplier = libraryUpgradeScriptableObject.multiplierValue;
                //     break;
                default:
                    break;
            }

            Value upgradeNextCost = new Value();
            upgradeNextCost = _gameMath.getNextUpgradeCost(
                libraryUpgradeScriptableObject.initialCost,
                libraryUpgradeScriptableObject.growthRate,
                libraryUpgradeScriptableObject.level,
                libraryUpgradeScriptableObject.nextCost.scale
            );

            libraryUpgradeScriptableObject.nextCost.value = upgradeNextCost.value;
            libraryUpgradeScriptableObject.nextCost.scale = upgradeNextCost.scale;
        }
    }
}
