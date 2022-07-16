using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUiHandler : MonoBehaviour {
    public Text buildingNameText;
    public TMP_Text discriptionText;
    public Text BuyValueText;
    public Text levelText;
    public Text soulsPerSecondText;

    public Slider levelSlider;

    public Button upgradeButton;
    public Button invisibleCloseButton;
    public Button closeBuildingUpgradeUiButton;

    public GameObject buildingUpgradeUi;
    [SerializeField] private GameController gameController;

    public void openUpgradeUi(BuildingController buildingController) {
        gameController.buildingUpgradeUiOpen = true;
        buildingUpgradeUi.SetActive(true);

        BuildingScriptableObject buildingScriptableObject = buildingController.getScriptableObject();

        buildingNameText.text = buildingScriptableObject.buildingName;
        discriptionText.text = buildingScriptableObject.description;
        BuyValueText.text = buildingScriptableObject.nextCost.value.ToString("N2") + Sufix.sufix[buildingScriptableObject.nextCost.scale];
        levelText.text = buildingScriptableObject.level.ToString() + "/" + buildingScriptableObject.tierlist.rank[buildingScriptableObject.tierlistRank].ToString();
        soulsPerSecondText.text = buildingScriptableObject.actualProduction.value.ToString("N1") + buildingScriptableObject.actualProduction.scale;

        levelSlider.maxValue = buildingScriptableObject.tierlist.rank[buildingScriptableObject.tierlistRank];
        // if (building.tierlistRank > 0) {
        //     levelSlider.minValue = building.tierlist.rank[building.tierlistRank--];
        // }
        // levelSlider.value = building.level;
        levelSlider.value = buildingScriptableObject.level;

        clearButtonListeners();

        upgradeButton.onClick.AddListener(delegate { upgradeBuilding(buildingController); });
        invisibleCloseButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
        closeBuildingUpgradeUiButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
    }

    void clearButtonListeners() {
        upgradeButton.onClick.RemoveAllListeners();
        invisibleCloseButton.onClick.RemoveAllListeners();
        closeBuildingUpgradeUiButton.onClick.RemoveAllListeners();
    }

    private void upgradeBuilding(BuildingController buildingController) {
        bool response = buildingController.levelUpBuilding();

        updateUi(buildingController);
    }

    private void closeBuildingUpgradeUi() {
        this.gameObject.SetActive(false);

        gameController.buildingUpgradeUiOpen = false;
    }

    private void updateUi(BuildingController buildingController) {
        BuildingScriptableObject buildingScriptableObject = buildingController.getScriptableObject();

        BuyValueText.text = buildingScriptableObject.nextCost.value.ToString("N1") + buildingScriptableObject.nextCost.scale;
        levelText.text = buildingScriptableObject.level.ToString() + "/" + buildingScriptableObject.tierlist.rank[buildingScriptableObject.tierlistRank].ToString();
        soulsPerSecondText.text = buildingScriptableObject.actualProduction.value.ToString("N1") + buildingScriptableObject.actualProduction.scale;
        levelSlider.maxValue = buildingScriptableObject.tierlist.rank[buildingScriptableObject.tierlistRank];
        // if (building.tierlistRank > 0) {
        //     levelSlider.minValue = building.tierlist.rank[building.tierlistRank--];
        // }
        levelSlider.value = buildingScriptableObject.level;
    }
}
