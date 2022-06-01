using UnityEngine;

[CreateAssetMenu(fileName = "BuildingScriptableObject", menuName = "ScriptableObjects/Building")]
public class BuildingScriptableObject : ScriptableObject
{
    public string buildingName;
    public string description;
    public int level;                      // Actual level

    public double initialProduction;       // Production value of the first upgrade
    public double initialCost;             // First value after buying
    public double growthRate;              // Rate of growth, used to calculate the cost of the next upgrde

    public Value actualProduction = new Value();         // Holds the actual production value and its scale
    public Value nextProduction = new Value();           // Holds the next production value and its scale
    public Value nextCost = new Value();                 // Holds the next cost value and its scale

    public BuildingTierlist tierlist;
    public int tierlistRank;
    public double buildingMultiplier = 1f;

    // public MeshRenderer brokenBuildingMesh;
    // public MeshRenderer buildingMesh;
}
