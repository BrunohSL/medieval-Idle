using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishingWellController : MonoBehaviour {
    [SerializeField] private WishingWellUiHandler wishingWellUiHandler;
    [SerializeField] private GameController _gameController;
    [SerializeField] private float timeToCollect; // 14400 = 4h
    [SerializeField] private CurrencyController _currencyController;

    private string _wishingWellLastCollectedTime;

    void Update() {
        string currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(_wishingWellLastCollectedTime)).TotalSeconds.ToString();

        string timeLeft = (timeToCollect - double.Parse(diffInSeconds)).ToString();

        string hours = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Hours.ToString();
        string minutes = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Minutes.ToString();
        string seconds = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Seconds.ToString();
        string timeLeftText = hours + "h " + minutes + "m " + seconds + "s";

        if (double.Parse(timeLeft) < 0) {
            timeLeftText = "You can collect your reward";
        }

        wishingWellUiHandler.setTimeLeftToCollectText(timeLeftText);

        if (double.Parse(diffInSeconds) > timeToCollect) {
            wishingWellUiHandler.enableCollectButton();
        } else {
            wishingWellUiHandler.disableCollectButton();
        }
    }

    public void collectWishingWellReward() {
        _wishingWellLastCollectedTime = System.DateTime.Now.ToString();

        Value actualProduction = _gameController.getBuildingTotalProduction();
        Value rewardValue = Currency.multiply(actualProduction, 5d);

        rewardValue = Currency.add(_currencyController.getGold(), rewardValue);

        _currencyController.setGold(rewardValue);
    }

    public string getLastCollectedTime() {
        return _wishingWellLastCollectedTime;
    }

    public void setLastCollectedTime(string collectedTime) {
        _wishingWellLastCollectedTime = collectedTime;
    }

    public void closeWishingWellUi() {
        wishingWellUiHandler.closeWishingWellUi();
        _gameController.uiOpen = false;
    }

    public void openWishingWellUi() {
        wishingWellUiHandler.openWishingWellUi();
    }
}
