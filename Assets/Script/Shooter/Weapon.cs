using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    protected AudioSource audioData;
    public WeaponData weaponData;
    [NonSerialized] public float direction;
    protected float elapsedInterval;
    [SerializeField] protected GameObject firePosition;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioData = gameObject.GetComponent<AudioSource>();
        elapsedInterval = weaponData.interval;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        elapsedInterval += Time.deltaTime;
    }
    
    public override bool Equals(System.Object obj)
    {
        return Equals(obj as Weapon);
    }
    
    public bool Equals(Weapon other)
    {
        if ((object) other == null)
        {
            return false;
        }

        return this.weaponData.Equals(other.weaponData);
    }

    public override int GetHashCode()
    {
        return weaponData.GetHashCode();
    }

    public virtual void Fire()
    {
        if (elapsedInterval > weaponData.interval)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject.Instantiate(weaponData.bullet, firePosition.transform.position, transform.rotation);
                animator.SetTrigger("Attack");
                audioData.Play();
                elapsedInterval = 0;
            }
        }
    }
}
