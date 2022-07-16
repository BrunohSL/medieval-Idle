using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishingWellUiHandler : MonoBehaviour {
    [SerializeField] private Button collectButton;
    [SerializeField] private Text timeLeftToCollectText;
    [SerializeField] private Text valueToCollectText;
    [SerializeField] private GameObject wishingWellUiPanel;
    [SerializeField] private GameController gameController;

    public void openWishingWellUi() {
        wishingWellUiPanel.SetActive(true);

        Value actualProduction = gameController.getBuildingTotalProduction();
        Value rewardValue = Currency.multiply(actualProduction, 5d);

        valueToCollectText.text = rewardValue.value.ToString();
    }

    public void closeWishingWellUi() {
        wishingWellUiPanel.SetActive(false);
    }

    public void disableCollectButton() {
        collectButton.interactable = false;
    }

    public void enableCollectButton() {
        collectButton.interactable = true;
    }

    public void setTimeLeftToCollectText(string text) {
        timeLeftToCollectText.text = text;
    }
}
