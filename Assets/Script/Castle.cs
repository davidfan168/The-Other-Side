using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Castle : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Image health;
    [SerializeField] private GameObject healthCanvas;

    private int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.PostActivateLeft += OnDeactivate;
        LevelManager.Instance.PostActivateRight += OnActivate;
        currentHealth = maxHealth;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.PostActivateLeft -= OnDeactivate;
        LevelManager.Instance.PostActivateRight -= OnActivate;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnActivate()
    {
        currentHealth = maxHealth;
        healthCanvas.SetActive(true);
    }

    void OnDeactivate()
    {
        healthCanvas.SetActive(false);
    }

    public void Die()
    {
        SceneManager.LoadScene("death");
    }
}
