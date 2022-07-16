using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OfflineEarningsUiHandler : MonoBehaviour {
    [SerializeField] private Text _earningsText;
    [SerializeField] private GameController _gameController;
    private Value earningsValue;

    void Start() {
        earningsValue = _gameController.getOfflineEarningsValue();
        _earningsText.text = earningsValue.value.ToString("N2") + Sufix.sufix[earningsValue.scale];
    }
}
