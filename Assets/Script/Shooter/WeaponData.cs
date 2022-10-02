using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public GameObject bullet;
    public Sprite icon;
    public float interval;
    
    public override bool Equals(System.Object obj)
    {
        return Equals(obj as WeaponData);
    }
    
    public bool Equals(WeaponData other)
    {
        if ((object) other == null)
        {
            return false;
        }

        return this.weaponName.Equals(other.weaponName);
    }

    public override int GetHashCode()
    {
        return weaponName.GetHashCode();
    }
}
