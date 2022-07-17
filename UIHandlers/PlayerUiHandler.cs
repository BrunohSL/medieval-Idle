using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiHandler : MonoBehaviour {
    [SerializeField] private GameObject playerUi;

    public void openPlayerMenu() {
        playerUi.SetActive(true);
    }

    public void closePlayerMenu() {
        playerUi.SetActive(false);
    }
}
