using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItemData
{
    public string itemName;
    public int startingCost;
    private int currentCost;
    public float costMultiplier;
    public int startingValue;
    private int currentValue;
    public float valueMultiplier;

    public void Initialize()
    {
        currentCost = startingCost;
        currentValue = startingValue;
    }
    public void IncrementCost()
    {
        currentCost = Mathf.CeilToInt(currentCost * costMultiplier);
    }
    
    public int GetCost()
    {
        return currentCost;
    }

    public void IncrementValue()
    {
        currentValue = Mathf.CeilToInt(currentValue * valueMultiplier);
    }

    public int GetValue()
    {
        return currentValue;
    }
}
