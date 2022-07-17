using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUiHandler : MonoBehaviour {
    [SerializeField] private GameObject settingsUi;

    public void openSettingsMenu() {
        settingsUi.SetActive(true);
    }

    public void closeSettingsMenu() {
        settingsUi.SetActive(false);
    }
}
