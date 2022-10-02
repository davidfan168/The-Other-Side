using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    private static WeaponManager _instance;
    public static WeaponManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    public List<Weapon> weapons;
    public int currentWeaponIndex;
    [NonSerialized] public UnityAction PostChangeWeaponAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextWeapon()
    {
        ChangeWeapon(currentWeaponIndex + 1);
    }

    public void PreviousWeapon()
    {
        ChangeWeapon(currentWeaponIndex - 1);
    }

    public void ChangeWeapon(int weaponIndex)
    {
        currentWeaponIndex = (weaponIndex + weapons.Count) % weapons.Count;
        PostChangeWeaponAction.Invoke();
    }

    public Weapon GetCurrentWeapon()
    {
        return GetWeapon(currentWeaponIndex);
    }

    public Weapon GetWeapon(int weaponIndex)
    {
        int normalizedIndex = (weaponIndex + weapons.Count) % weapons.Count;
        return weapons[normalizedIndex];
    }
}
