using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image uiImage;
    [SerializeField] private int diff;
    
    // Start is called before the first frame update
    void Start()
    {
        WeaponManager.Instance.PostChangeWeaponAction += OnChangeWeapon;
        OnChangeWeapon();
    }

    private void OnDestroy()
    {
        WeaponManager.Instance.PostChangeWeaponAction -= OnChangeWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnChangeWeapon()
    {
        int weaponIndex = WeaponManager.Instance.currentWeaponIndex;
        Weapon weapon = WeaponManager.Instance.GetWeapon(weaponIndex + diff);
        uiImage.sprite = weapon.weaponData.icon;
    }
}
