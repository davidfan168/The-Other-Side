using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private static ShopManager _instance;
    public static ShopManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    [SerializeField] private Transform turretPosition;
    [SerializeField] private GameObject turret;
    public List<ShopItemData> itemDatas;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var itemData in itemDatas)
        {
            itemData.Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ShopItemData GetItemData(string name)
    {
        foreach (var itemData in itemDatas)
        {
            if (itemData.itemName.Equals(name))
            {
                return itemData;
            }
        }

        return null;
    }

    public void OnPurchaseTurret()
    {
        ShopItemData turretItem = GetItemData("Turret");
        if (LevelManager.Instance.gold >= turretItem.GetCost())
        {
            LevelManager.Instance.ChangeGold(-turretItem.GetCost());
            turretItem.IncrementCost();
            GameObject newTurret = GameObject.Instantiate(turret, turretPosition.transform.position, Quaternion.identity);
            LevelManager.Instance.turrets.Add(newTurret.GetComponent<Turret>());
        }
    }

    public void OnPurchaseShooterAttack()
    {
        
    }

    public void OnPurchaseShooterWeapon()
    {
        
    }

    public void OnPurchaseTurretHealth()
    {
        
    }

    public void OnPurchaseTurretAttack()
    {
        
    }

    public void OnPurchaseTurretDamageMultiplier()
    {
        
    }

    public void OnPurchasePoison()
    {
        
    }
}
