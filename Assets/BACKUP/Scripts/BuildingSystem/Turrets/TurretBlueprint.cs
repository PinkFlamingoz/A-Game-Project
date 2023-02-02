using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public GameObject upgradedPrefablvl3;
    public int upgradeCostlvl3;

    public int GetSellAmount()
    {
        return cost / 2;
    }

}
