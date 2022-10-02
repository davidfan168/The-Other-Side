using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBullet : Bullet
{
    [SerializeField] private float duration;
    private float durationPassed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        durationPassed += Time.deltaTime;
        if (durationPassed > duration)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
