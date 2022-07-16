using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Value {
    public double value;
    public int scale;

    public Value() {
        value = 0;
        scale = 0;
    }

    public Value(double value, int scale) {
        this.value = value;
        this.scale = scale;
    }
}
