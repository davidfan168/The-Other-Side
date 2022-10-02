using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private Transform deactivatedLocation;
    [SerializeField] private int maxHealth;
    [SerializeField] private Image health;
    [SerializeField] private GameObject healthCanvas;

    private Rigidbody2D rb;
    
    private Vector3 direction;
    private Weapon currentWeapon;
    private Weapon autoAttackWeapon;
    private bool activated = true;
    private int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        WeaponManager.Instance.PostChangeWeaponAction += OnChangeWeapon;
        currentWeapon = WeaponManager.Instance.GetCurrentWeapon();
        autoAttackWeapon = WeaponManager.Instance.GetWeapon(0);

        LevelManager.Instance.PostActivateLeft += OnActivate;
        LevelManager.Instance.PostActivateRight += OnDeactivate;

        rb = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void OnDestroy()
    {
        WeaponManager.Instance.PostChangeWeaponAction -= OnChangeWeapon;
        LevelManager.Instance.PostActivateLeft -= OnActivate;
        LevelManager.Instance.PostActivateRight -= OnDeactivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Move();
            Rotate();

            if (Input.GetKey(KeyCode.Space))
            {
                Fire();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                WeaponManager.Instance.PreviousWeapon();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                WeaponManager.Instance.NextWeapon();
            }
        }
        else
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);
            Vector2 targ = new Vector2(deactivatedLocation.position.x, deactivatedLocation.position.y);
            if (Vector2.Distance(pos, targ) < 0.3)
            {
                rb.velocity = Vector2.zero;
                AutoAttack();
            }
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement *= speed * Time.deltaTime;
        gameObject.transform.position += movement;
    }

    void Rotate()
    {
        Vector3 playerPos = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // mousePos - playerPos
        direction = new Vector3(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle - 90));
        currentWeapon.direction = angle;
    }

    void Fire()
    {
        currentWeapon.Fire();
    }

    void AutoAttack()
    {
        // aim turret
        autoAttackWeapon.Fire();
    }
    
    void OnChangeWeapon()
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = WeaponManager.Instance.GetCurrentWeapon();
        currentWeapon.gameObject.SetActive(true);
    }

    void OnActivate()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        currentHealth = maxHealth;
        healthCanvas.SetActive(true);
        activated = true;
    }

    void OnDeactivate()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 targ = new Vector2(deactivatedLocation.position.x, deactivatedLocation.position.y);
        Vector2 dir = (targ - pos).normalized;
        rb.velocity = dir * speed;
        gameObject.layer = LayerMask.NameToLayer("AutoAttack");
        healthCanvas.SetActive(false);
        activated = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.fillAmount = (float) currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("death");
    }
}
