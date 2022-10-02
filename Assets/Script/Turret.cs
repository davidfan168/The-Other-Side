using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamageable
{
    private GameObject selectedObject;
    private Vector3 offset;
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject healthCanvas;
    private int currentHealth;
    private bool activated;
    [SerializeField] private float attackInterval;
    private float timeElapsed = 0;
    [SerializeField] private GameObject bullet;

    void Start()
    {
        LevelManager.Instance.PostActivateLeft += OnActivate;
        LevelManager.Instance.PostActivateRight += OnActivate;
        currentHealth = maxHealth;
        activated = true;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.PostActivateLeft -= OnActivate;
        LevelManager.Instance.PostActivateRight -= OnActivate;
    }

    void Update()
    {
        ProcessDrag();
        if (activated)
        {
            Attack();
        }
    }

    void ProcessDrag()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        // https://gamedevbeginner.com/how-to-move-an-object-with-the-mouse-in-unity-in-2d/
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject != null)
            {
                selectedObject = targetObject.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
        }

        if (selectedObject != null)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeactivate();
        }
    }

    private void OnActivate()
    {
        timeElapsed = 0;
        healthCanvas.SetActive(true);
        activated = true;
    }

    private void OnDeactivate()
    {
        healthCanvas.SetActive(false);
        activated = false;
    }

    private void Fire(Transform target)
    {
        Vector3 turretPos = transform.position;
        Vector3 targetPos = target.position;

        Vector2 direction = new Vector2(targetPos.x - turretPos.x, targetPos.y - turretPos.y).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
        GameObject.Instantiate(bullet, turretPos, rotation);
    }
    
    private void Attack()
    {
        timeElapsed -= Time.deltaTime;
        if (timeElapsed > attackInterval)
        {
            Fire(LevelManager.Instance.GetTargetForTurret());
            timeElapsed = 0;
        }
    }

    public bool isAlive()
    {
        return currentHealth >= 0;
    }
}
