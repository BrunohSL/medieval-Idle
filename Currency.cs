using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class Currency {
    public static string[] suifx = new string[] {
        "",
        "k",
        "kk",
        "a",
        "b",
        "c",
        "d",
        "e",
        "f",
        "g",
        "h",
        "i",
        "j",
        "l",
        "m",
        "n",
        "o",
        "p",
        "q",
        "r",
        "s",
        "t",
        "u",
        "v",
        "w",
        "x",
        "y",
        "z",
    };

    /**
     * Handles the sum of two values, uses the scale to make the sum
     *
     * @param Value firstValue     First value of the operation
     * @param Value secondValue    Second value of the operation
     *
     * @return Value valueClass    Return a instance of valueClass with the sum result of the two values
     */
    public static Value add(Value firstValue, Value secondValue) {
        Value valueClass = new Value();

        if (firstValue.scale != secondValue.scale) {
            int scaleDiff = firstValue.scale - secondValue.scale;

            if (scaleDiff < -2 || scaleDiff > 2) {
                if (scaleDiff > 0) {
                    valueClass.value = double.Parse(firstValue.value.ToString("N3"));
                    valueClass.scale = firstValue.scale;

                    return valueClass;
                } else {
                    valueClass.value = secondValue.value;
                    valueClass.scale = secondValue.scale;

                    return valueClass;
                }
            }

            if (firstValue.scale > secondValue.scale) {
                if (firstValue.scale > 0) {
                    firstValue.scale--;
                    firstValue.value *= 1000000;
                    valueClass = add(firstValue, secondValue);
                }
            }

            if (firstValue.scale < secondValue.scale) {
                if (secondValue.scale > 0) {
                    secondValue.scale--;
                    secondValue.value *= 1000000;
                    valueClass = add(firstValue, secondValue);
                }
            }

            if (valueClass.value > 1000000) {
                valueClass.value /= 1000000;
                valueClass.scale++;
            }

            return valueClass;
        } else {
            firstValue.value += secondValue.value;

            valueClass.value = double.Parse(firstValue.value.ToString("N3"));
            valueClass.scale = firstValue.scale;

            if (valueClass.value > 1000000) {
                valueClass.value /= 1000000;
                valueClass.scale++;
            }

            return valueClass;
        }
    }

    /**
     * Handles the subtraction of two values, uses the scale to make the subtraction
     *
     * @param Value firstValue     First value of the operation
     * @param Value secondValue    Second value of the operation
     *
     * @return Value valueClass    Return a instance of valueClass with the subtraction result of the two values
     * @return Value null          Return null if the subtraction result is lower than 0
     */
    public static Value subtract(Value firstValue, Value secondValue) {
        Value valueClass = new Value();

        if (firstValue.scale != secondValue.scale) {
            int scaleDiff = firstValue.scale - secondValue.scale;

            if ((scaleDiff < -2 || scaleDiff > 2) && scaleDiff > 0) {
                valueClass.value = double.Parse(firstValue.value.ToString("N3"));
                valueClass.scale = firstValue.scale;

                return valueClass;
            }

            if (firstValue.scale > secondValue.scale) {
                if (firstValue.scale > 0) {
                    firstValue.scale--;
                    firstValue.value *= 1000000;
                    valueClass = subtract(firstValue, secondValue);
                }
            }

            if (firstValue.scale < secondValue.scale) {
                return null;
            }

            return valueClass;
        } else {
            if ((firstValue.value - secondValue.value) < 0) {
                return null;
            } else {
                firstValue.value -= secondValue.value;
                valueClass.value = firstValue.value;
                valueClass.scale = firstValue.scale;
            }

            return valueClass;
        }
    }

    public static Value multiply(Value value, double multiplier) {
        Value valueClass = new Value();

        value.value *= multiplier;

        while (value.value > 1000000) {
            value.value /= 1000000;
            value.scale++;
        }

        valueClass = value;

        return valueClass;
    }

    public static Value divide(Value dividend, double divisor) {
        Value valueClass = new Value();

        if (dividend.value < divisor && dividend.scale <= 0) {
            throw new System.Exception("Valor do dividendo menor que o divisor");
        }

        if (dividend.value < divisor && dividend.scale > 0) {
            dividend.scale--;
            dividend.value *= 1000000;
        }

        valueClass.value = dividend.value / divisor;

        if (valueClass.value > 1000000) {
            valueClass.value /= 1000000;
            valueClass.scale++;
        }

        return valueClass;
    }
}
