using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private GameController _gameController;
    [SerializeField] private PlayerUiHandler playerUiHandler;
    [SerializeField] private SettingsUiHandler settingsUiHandler;

    public void openPlayerMenu() {
        playerUiHandler.openPlayerMenu();
        _gameController.uiOpen = true;
    }

    public void closePlayerMenu() {
        playerUiHandler.closePlayerMenu();
        _gameController.uiOpen = false;
    }

    public void openSettingsMenu() {
        _gameController.uiOpen = true;
        settingsUiHandler.openSettingsMenu();
    }

    public void closeSettingsMenu() {
        _gameController.uiOpen = false;
        settingsUiHandler.closeSettingsMenu();
    }
}
