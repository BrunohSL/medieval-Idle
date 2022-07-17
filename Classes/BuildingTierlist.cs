using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class BuildingTierlist {
    public static int[] rank = new int[] {
        10,
        25,
        50,
        100,
        150,
        200,
        250,
        300,
        350,
        400,
        450,
        500,
        600,
        700,
        800,
        900,
        1000,
        1250,
        1500,
        1750,
        2000
    };

    public static int[] rankMultiplier = new int[] {
        1,
        2,
        2,
        2,
        2,
        3,
        3,
        3,
        3,
        3,
        4,
        4,
        4,
        4,
        4,
        5,
        5,
        5,
        5,
        5,
        10
    };
}
