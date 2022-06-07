using UnityEngine;

[CreateAssetMenu(fileName = "LibraryUpgradeScriptableObject", menuName = "ScriptableObjects/LibraryUpgrade")]
public class LibraryUpgradeScriptableObject : ScriptableObject {
    public string upgradeName;
    public string upgradeDescription;
    public int level;
    public int maxLevel;
    public double multiplierValue;
    public Value nextCost;
    public double initialCost;
    public double growthRate;
}
