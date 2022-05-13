using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishingWellUiHandler : MonoBehaviour {
    public Button collectButton;
    public Text timeLeftToCollectText;
    public GameObject wishingWellUiPanel;

    [SerializeField] private CurrencyController _currencyController;

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
        Value rewardValue = Currency.multiply(actualProduction, 5f);

        rewardValue = Currency.add(_currencyController.getGold(), rewardValue);

        _currencyController.setGold(rewardValue);
    }

    public void setTimeLeftToCollectText(string text) {
        timeLeftToCollectText.text = text;
    }
}
