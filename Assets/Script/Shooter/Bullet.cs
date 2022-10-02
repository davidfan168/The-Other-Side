using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected int damage;
    
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        float angle = transform.rotation.eulerAngles.z + 90;
        direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
