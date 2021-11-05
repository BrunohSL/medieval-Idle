using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Buildings {
    public static List<Building> buildingsList = new List<Building>();
    public static Building buildingClass = new Building();

    public static void setNewBuildingValues(string buildingName, Building updatedBuilding) {
        foreach (Building building in buildingsList) {
            if (building.buildingName == buildingName) {
                building.actualProduction.value = building.nextProduction.value;
                building.level = updatedBuilding.level;

                building.nextProduction.value = updatedBuilding.nextProduction.value;

                building.nextCost.value = updatedBuilding.nextCost.value;
                building.nextCost.scale = updatedBuilding.nextCost.scale;

                building.tierlistRank = updatedBuilding.tierlistRank;
            }
        }
    }

    /**
     * Search on the employees vector for a match with the name passed
     *
     * @param string employeeName Name to look for
     *
     * @return Employee employee or null if nothing found
     */
    public static Building getBuildingByName(string buildingName) {
        foreach (Building building in buildingsList) {
            if (building.buildingName == buildingName) {
                return building;
            }
        }

        return null;
    }

    /**
     * Generate the next upgrade cost of click or employee
     *
     * @param double initialProduction production original value, the initial value
     * @param double level             click level or employee level
     * @param double multiplier        value of all bonuses that increases soul generation
     *
     * @return string nextProductionRate
     */
    private static string getNextProductionRate(double initialProduction, int level, double multiplier = 1) {
        double nextProductionRate = (initialProduction * level) * multiplier;

        return nextProductionRate.ToString("N2");
    }

    /**
     * Upgrade employee button behavior
     *
     * @param string employeeName Name of the employee that will level up
     *
     * First check if the player can buy the next upgrade,
     * Then reduces the cost of the upgrade from your number of souls,
     * Then set the actual production as the next production value
     * Then increase the player level by one
     * Then generates the new next production value
     * Then generates the new next cost for the upgrade
     */
    public static bool levelUpBuilding(Building building) {
        Value valueClass = new Value();

        valueClass = Currency.subtract(Souls.totalSouls.value, Souls.totalSouls.scale, building.nextCost.value, building.nextCost.scale);

        if (valueClass == null) {
            Debug.Log("Valor negativo aqui (valor de custo do próximo upgrade é muito caro)");
            return false;
        } else {
            Souls.totalSouls.value = valueClass.value;
            Souls.totalSouls.scale = valueClass.scale;

            building.actualProduction.value = building.nextProduction.value;
            building.level++;

            building.nextProduction.value = double.Parse(getNextProductionRate(building.initialProduction, building.level, Modifiers.multiplier));

            Value buildingNextCost = new Value();
            buildingNextCost = getNextUpgradeCost(building.initialCost, building.growthRate, building.level, building.nextCost.scale);

            building.nextCost.value = buildingNextCost.value;
            building.nextCost.scale = buildingNextCost.scale;

            if (building.level >= building.tierlist.rank[building.tierlistRank]) {
                building.tierlistRank++;
            }

            setNewBuildingValues(building.buildingName, building);

            return true;
        }
    }

    /**
     * Gets the difference of nextProduction and actualProduction to show how much it will increase after the upgrade
     *
     * @param double nextProduction
     * @param double actualProduction
     *
     * @return double diff
     */
    public static double getNextProductionDiff(double nextProduction, double actualProduction) {
        double diff = nextProduction - actualProduction;

        return diff;
    }

    /**
     * Generate the next upgrade cost of click or employee
     *
     * @param double initialCost original cost of click or employee, the initial value
     * @param double growthRate  the coefficient used to increase the next upgrade value for each level up
     * @param double level       click level or employee level
     *
     * @return Value nextCost
     */
    private static Value getNextUpgradeCost(double initialCost, double growthRate, int level, int scale) {
        Value nextCost = new Value();
        nextCost.scale = 0;

        double nextCostValue = initialCost * (Mathf.Pow((float)growthRate, level));
        nextCost.value = nextCostValue;

        if (nextCost.value > 1000000) {
            Debug.Log("Subiu scale do próximo custo");
            nextCost.value /= 1000000;
            nextCost.scale++;
        }

        return nextCost;
    }

    public static void updateBuildingsActualProduction() {
        foreach (Building building in Buildings.buildingsList) {
            building.nextProduction.value = double.Parse(getNextProductionRate(building.initialProduction, building.level + 1, Modifiers.multiplier));
            building.actualProduction.value = double.Parse(getNextProductionRate(building.initialProduction, building.level, Modifiers.multiplier));
        }
    }

    public static void setBuildingsList() {
        // Graveyard
        buildingClass.buildingName = "graveyard";
        buildingClass.description = "Just a simple cemetery.";
        buildingClass.level = 0;

        buildingClass.initialProduction = 1.67f;
        buildingClass.initialCost = 5f;
        buildingClass.growthRate = 1.07f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 5f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // church
        buildingClass.buildingName = "church";
        buildingClass.description = "Just a simple church";
        buildingClass.level = 0;

        buildingClass.initialProduction = 20f;
        buildingClass.initialCost = 60f;
        buildingClass.growthRate = 1.15f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 60f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // guillotine
        buildingClass.buildingName = "guillotine";
        buildingClass.description = "Just a simple guillotine";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // farm
        buildingClass.buildingName = "farm";
        buildingClass.description = "Just a simple farm";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // armor shop
        buildingClass.buildingName = "armorShop";
        buildingClass.description = "Just a simple armor shop";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Chicken nest
        buildingClass.buildingName = "chickenNest";
        buildingClass.description = "Just a simple chicken nest";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Food shop
        buildingClass.buildingName = "foodShop";
        buildingClass.description = "Just a simple food shop";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Weapon shop
        buildingClass.buildingName = "weaponShop";
        buildingClass.description = "Just a simple weapon shop";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Battle arena
        buildingClass.buildingName = "battleArena";
        buildingClass.description = "Just a simple battle arena";
        buildingClass.level = 0;

        buildingClass.initialProduction = 90f;
        buildingClass.initialCost = 720f;
        buildingClass.growthRate = 1.14f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 720f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Wishing Well
        buildingClass.buildingName = "wishingWell";
        buildingClass.description = "Just a simple wishing well";
        buildingClass.level = 0;

        buildingClass.initialProduction = 0f;
        buildingClass.initialCost = 0f;
        buildingClass.growthRate = 0f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 0f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Castle
        buildingClass.buildingName = "castle";
        buildingClass.description = "Just a castle";
        buildingClass.level = 0;

        buildingClass.initialProduction = 0f;
        buildingClass.initialCost = 0f;
        buildingClass.growthRate = 0f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 0f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();

        // Castle Walls
        buildingClass.buildingName = "castleWall";
        buildingClass.description = "Just a simple castle wall";
        buildingClass.level = 0;

        buildingClass.initialProduction = 0f;
        buildingClass.initialCost = 0f;
        buildingClass.growthRate = 0f;

        buildingClass.actualProduction.value = 0f;
        buildingClass.actualProduction.scale = 0;
        buildingClass.actualProduction.multiplier = 1f;

        buildingClass.nextProduction.value = 0f;
        buildingClass.nextProduction.scale = 0;
        buildingClass.nextProduction.multiplier = 1f;

        buildingClass.nextCost.value = 0f;
        buildingClass.nextCost.scale = 0;
        buildingClass.nextCost.multiplier = 1f;

        buildingClass.tierlist = new BuildingTierlist();
        buildingClass.tierlistRank = 0;

        buildingsList.Add(buildingClass);
        buildingClass = new Building();
    }
}
