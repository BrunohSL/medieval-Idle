using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour {
    [SerializeField] private Value _gold;
    [SerializeField] private Value _wisdom;

    public Value getGold() {
        return this._gold;
    }

    public void setGold(Value gold) {
        this._gold.value = gold.value;
        this._gold.scale = gold.scale;
    }

    public Value getWisdom() {
        return this._wisdom;
    }

    public void setWisdom(Value wisdom) {
        this._wisdom.value = wisdom.value;
        this._wisdom.scale = wisdom.scale;
    }
}
