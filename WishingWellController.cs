using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WishingWellController : MonoBehaviour {
    string currentTime;
    public WishingWellUiHandler wishingWellUiHandler;

    void Update() {
        currentTime = System.DateTime.Now.ToString();
        string diffInSeconds = (System.DateTime.Parse(currentTime) - System.DateTime.Parse(GameController.wishingWellLastCollectedTime)).TotalSeconds.ToString();

        // string timeLeft = (14400f - double.Parse(diffInSeconds)).ToString();
        string timeLeft = (15f - double.Parse(diffInSeconds)).ToString();

        string hours = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Hours.ToString();
        string minutes = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Minutes.ToString();
        string seconds = System.TimeSpan.FromSeconds(double.Parse(timeLeft)).Seconds.ToString();
        string timeLeftText = hours + "h " + minutes + "m " + seconds + "s";

        if (double.Parse(timeLeft) < 0) {
            timeLeftText = "You can collect your reward";
        }

        wishingWellUiHandler.setTimeLeftToCollectText(timeLeftText);

        // 1440 = 4h
        if (double.Parse(diffInSeconds) > 15) {
        // if (double.Parse(diffInSeconds) > 14400) {
            wishingWellUiHandler.enableCollectButton();
        } else {
            wishingWellUiHandler.disableCollectButton();
        }
    }

    public string getCollectedTime() {
        return GameController.wishingWellLastCollectedTime;
    }

    public void setCollectedTime(string collectedTime) {
        GameController.wishingWellLastCollectedTime = collectedTime;
    }
}
