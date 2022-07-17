using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUiHandler : MonoBehaviour {
    [SerializeField] private Text buildingNameText;
    [SerializeField] private TMP_Text discriptionText;
    [SerializeField] private Text BuyValueText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text soulsPerSecondText;
    [SerializeField] private Slider levelSlider;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button invisibleCloseButton;
    [SerializeField] private Button closeBuildingUpgradeUiButton;
    [SerializeField] private GameObject buildingUpgradeUi;

    private BuildingController _buildingController;
    [SerializeField] private GameController _gameController;
    [SerializeField] private CurrencyController _currencyController;

    private void Update() {
        Value value = new Value(_currencyController.getGold().value, _currencyController.getGold().scale);
        value = Currency.subtract(value, _buildingController.getUpgradeCost());

        if (value != null) {
            upgradeButton.interactable = true;
        } else {
            upgradeButton.interactable = false;
        }
    }

    public void openUpgradeUi(BuildingController buildingController) {
        _buildingController = buildingController;
        _gameController.buildingUpgradeUiOpen = true;
        buildingUpgradeUi.SetActive(true);

        BuildingScriptableObject buildingScriptableObject = buildingController.getScriptableObject();

        buildingNameText.text = buildingScriptableObject.buildingName;
        discriptionText.text = buildingScriptableObject.description;
        BuyValueText.text = buildingScriptableObject.nextCost.value.ToString("N2") + Sufix.sufix[buildingScriptableObject.nextCost.scale];
        levelText.text = buildingScriptableObject.level.ToString() + "/" + BuildingTierlist.rank[buildingScriptableObject.tierlistRank].ToString();
        soulsPerSecondText.text = buildingScriptableObject.actualProduction.value.ToString("N1") + buildingScriptableObject.actualProduction.scale;

        updateSliderUi(buildingScriptableObject);

        clearButtonListeners();

        upgradeButton.onClick.AddListener(delegate { upgradeBuilding(buildingController); });
        invisibleCloseButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
        closeBuildingUpgradeUiButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
    }

    private void updateUi(BuildingController buildingController, BuildingScriptableObject buildingScriptableObject) {
        BuyValueText.text = buildingScriptableObject.nextCost.value.ToString("N1") + buildingScriptableObject.nextCost.scale;
        levelText.text = buildingScriptableObject.level.ToString() + "/" + BuildingTierlist.rank[buildingScriptableObject.tierlistRank].ToString();
        soulsPerSecondText.text = buildingScriptableObject.actualProduction.value.ToString("N1") + buildingScriptableObject.actualProduction.scale;
        levelSlider.maxValue = BuildingTierlist.rank[buildingScriptableObject.tierlistRank];
        updateSliderUi(buildingScriptableObject);
    }

    void updateSliderUi(BuildingScriptableObject buildingScriptableObject) {
        levelSlider.minValue = buildingScriptableObject.tierlistRank == 0 ? 0 : BuildingTierlist.rank[buildingScriptableObject.tierlistRank - 1];
        levelSlider.maxValue = BuildingTierlist.rank[buildingScriptableObject.tierlistRank];

        levelSlider.value = buildingScriptableObject.level;
    }

    void clearButtonListeners() {
        upgradeButton.onClick.RemoveAllListeners();
        invisibleCloseButton.onClick.RemoveAllListeners();
        closeBuildingUpgradeUiButton.onClick.RemoveAllListeners();
    }

    private void upgradeBuilding(BuildingController buildingController) {
        BuildingScriptableObject buildingScriptableObject = buildingController.getScriptableObject();
        bool response = buildingController.levelUpBuilding();

        updateUi(buildingController, buildingScriptableObject);
    }

    private void closeBuildingUpgradeUi() {
        this.gameObject.SetActive(false);

        _buildingController = null;
        _gameController.buildingUpgradeUiOpen = false;
    }
}
