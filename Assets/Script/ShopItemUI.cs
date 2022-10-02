using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private string itemName;
    private ShopItemData itemData;
    [SerializeField] private Text costText;
    [SerializeField] private Text statusText;
    
    // Start is called before the first frame update
    void Start()
    {
        itemData = ShopManager.Instance.GetItemData(itemName);
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        costText.text = "Cost: " + itemData.GetCost();
        statusText.text = "Status: " + itemData.GetValue();
    }
}
