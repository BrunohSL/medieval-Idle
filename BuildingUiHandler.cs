using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUiHandler : MonoBehaviour {
    public Text buildingNameText;
    public Text BuyValueText;
    public Text levelText;
    public Text soulsPerSecondText;

    public Slider levelSlider;

    public Button upgradeButton;
    public Button invisibleCloseButton;
    public Button closeBuildingUpgradeUiButton;

    public void openUpgradeUi(Building building, GameObject buildingUpgradeUi) {
        GameObject.Find("gameController").GetComponent<GameController>().buildingUpgradeUiOpen = true;
        buildingUpgradeUi.SetActive(true);

        buildingNameText.text = building.buildingName;
        BuyValueText.text = building.nextCost.value.ToString("N2") + building.nextCost.scale;
        levelText.text = building.level.ToString() + "/" + building.tierlist.rank[building.tierlistRank].ToString();
        soulsPerSecondText.text = building.actualProduction.value.ToString("N2") + building.actualProduction.scale;

        levelSlider.maxValue = building.tierlist.rank[building.tierlistRank];
        // if (building.tierlistRank > 0) {
        //     levelSlider.minValue = building.tierlist.rank[building.tierlistRank--];
        // }
        levelSlider.value = building.level;

        clearButtonListeners();

        upgradeButton.onClick.AddListener(delegate { upgradeBuilding(building); });
        invisibleCloseButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
        closeBuildingUpgradeUiButton.onClick.AddListener(delegate { closeBuildingUpgradeUi(); });
    }

    void clearButtonListeners() {
        upgradeButton.onClick.RemoveAllListeners();
        invisibleCloseButton.onClick.RemoveAllListeners();
        closeBuildingUpgradeUiButton.onClick.RemoveAllListeners();
    }

    private void upgradeBuilding(Building building) {
        bool response = Buildings.levelUpBuilding(building);

        if (building.level == 1 && response) {
            GameObject buildingObj = GameObject.Find(building.buildingName);
            Destroy(buildingObj);

            GameObject.Find("gameController").GetComponent<GameController>().instantiateBuildings();
        }

        updateUi(building);
    }

    private void closeBuildingUpgradeUi() {
        this.gameObject.SetActive(false);

        GameObject.Find("gameController").GetComponent<GameController>().buildingUpgradeUiOpen = false;
    }

    private void updateUi(Building building) {
        buildingNameText.text = building.buildingName;
        BuyValueText.text = building.nextCost.value.ToString("N2") + building.nextCost.scale;
        levelText.text = building.level.ToString() + "/" + building.tierlist.rank[building.tierlistRank].ToString();
        soulsPerSecondText.text = building.actualProduction.value.ToString("N2") + building.actualProduction.scale;
        levelSlider.maxValue = building.tierlist.rank[building.tierlistRank];
        // if (building.tierlistRank > 0) {
        //     levelSlider.minValue = building.tierlist.rank[building.tierlistRank--];
        // }
        levelSlider.value = building.level;
    }
}
