using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public int maxHp;
    private int currentHp;
    private bool attacking;
    private float timePassed = 0;
    private IDamageable otherObject;
    protected Rigidbody2D rb;

    [SerializeField] private float attackInterval;
    [SerializeField] private int value;
    [SerializeField] private int damage;
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private Image health;
    [SerializeField] protected float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Attack();
    }

    public void TakeDamage(float damage)
    {
        TakeDamage(Mathf.CeilToInt(damage));
    }
    
    public void TakeDamage(int damage)
    {
        healthCanvas.SetActive(true);
        
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            health.fillAmount = (float) currentHp / maxHp;
        }
    }

    public void Die()
    {
        LevelManager.Instance.ChangeGold(value);
        List<Enemy> enemies = LevelManager.Instance.enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            if (enemy == this)
            {
                enemies.Remove(enemy);
                break;
            }
        }
        Destroy(gameObject);
    }

    public void Attack()
    {
        timePassed += Time.deltaTime;
        if (attacking && timePassed > attackInterval)
        {
            otherObject.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            otherObject = other.gameObject.GetComponent<IDamageable>();
            attacking = true;
        }
        
        if (other.gameObject.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        otherObject = null;
        attacking = false;
    }

}
