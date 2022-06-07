using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LibraryUpgradeButtonHandler : MonoBehaviour {
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _nextCostText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _multiplierValueText;
    [SerializeField] private LibraryUpgradeScriptableObject _libraryUpgradeScriptableObject;

    void Start() {
        refreshUi();
    }

    void Update() {
        refreshUi();
    }

    public void refreshUi() {
        _upgradeNameText.text = _libraryUpgradeScriptableObject.upgradeName;
        _upgradeDescriptionText.text = _libraryUpgradeScriptableObject.upgradeDescription;
        _multiplierValueText.text = _libraryUpgradeScriptableObject.multiplierValue.ToString() + "X";
        _nextCostText.text = _libraryUpgradeScriptableObject.nextCost.value.ToString("N2");
        _levelText.text = _libraryUpgradeScriptableObject.level.ToString() + "/" + _libraryUpgradeScriptableObject.maxLevel.ToString();
    }
}
