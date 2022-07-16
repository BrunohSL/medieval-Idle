using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierController : MonoBehaviour {
    [SerializeField] private double _globalMultiplier = 1d;
    [SerializeField] private double _goldMultiplier = 1d;
    [SerializeField] private double _wisdomMultiplier = 1d;

    public double getGlobalMultiplier() {
        return this._globalMultiplier;
    }

    public void setGlobalMultiplier(double globalMultiplier) {
        this._globalMultiplier = globalMultiplier;
    }

    public double getGoldMultiplier() {
        return this._goldMultiplier;
    }

    public void setGoldMultiplier(double goldMultiplier) {
        this._goldMultiplier = goldMultiplier;
    }

    public double getWisdomMultiplier() {
        return this._wisdomMultiplier;
    }

    public void setWisdomMultiplier(double wisdomMultiplier) {
        this._wisdomMultiplier = wisdomMultiplier;
    }
}
