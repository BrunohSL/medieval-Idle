using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Responsabilidades
// atualizar valores do scriptableObject da construção
// definir 'sprite' da construção, predio demolido caso seja lvl 0 ou modelo certo da construção
public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private BuildingScriptableObject buildingScriptableObject;
    // [SerializeField]
    // private GameObject brokenBuildingPrefab;

    public BuildingScriptableObject getScriptableObject() {
        return buildingScriptableObject;
    }

    public Value getUpgradeCost() {
        return new Value();
    }

    public Value getNextProduction() {
        return new Value();
    }

    public Value getActualProduction() {
        return new Value();
    }
}



// produz 1
// custa 10
// paga 10
// atualizar custo de compra
// atualizar produção de recurso
// gerar valor de produção do próximo upgrade
