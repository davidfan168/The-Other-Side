using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void Fire()
    {
        if (elapsedInterval > weaponData.interval)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject.Instantiate(weaponData.bullet, firePosition.transform.position, transform.rotation);
                GameObject.Instantiate(weaponData.bullet, firePosition.transform.position, transform.rotation * Quaternion.Euler(0, 0, 15));
                GameObject.Instantiate(weaponData.bullet, firePosition.transform.position, transform.rotation * Quaternion.Euler(0, 0, -15));
                animator.SetTrigger("Attack");
                audioData.Play();
                elapsedInterval = 0;
            }
        }
    }
}
