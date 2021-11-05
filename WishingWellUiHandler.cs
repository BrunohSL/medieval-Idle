using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishingWellUiHandler : MonoBehaviour {
    public Button collectButton;
    public Text timeLeftToCollectText;
    public GameObject wishingWellUiPanel;

    void Start() {
        collectButton.onClick.AddListener(delegate { collectWishingWellReward(); });
    }

    public void openWishingWellUi() {
        wishingWellUiPanel.SetActive(true);
    }

    public void disableCollectButton() {
        collectButton.interactable = false;
    }

    public void enableCollectButton() {
        collectButton.interactable = true;
    }

    void collectWishingWellReward() {
        WishingWellController wishingWellController = GameObject.Find("wishingWell").GetComponent<WishingWellController>();
        wishingWellController.setCollectedTime(System.DateTime.Now.ToString());

        Value actualProduction = GameObject.Find("gameController").GetComponent<GameController>().getBuildingTotalProduction();
        Value rewardValue = Currency.multiply(actualProduction.value, actualProduction.scale, 5f);

        rewardValue = Currency.add(Souls.totalSouls.value, Souls.totalSouls.scale, rewardValue.value, rewardValue.scale);

        Souls.totalSouls.value = rewardValue.value;
        Souls.totalSouls.scale = rewardValue.scale;
    }

    public void setTimeLeftToCollectText(string text) {
        timeLeftToCollectText.text = text;
    }
}
