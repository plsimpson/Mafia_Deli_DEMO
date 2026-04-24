using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : DemoSceneLoader
{
    [Header("Health UI")]
    [SerializeField] private List<GameObject> healthIcons = new List<GameObject>();

    private int currentHealth;

    private void Start()
    {
        currentHealth = healthIcons.Count;
    }

    public void TakeDamage(int damage = 1)
    {
        for (int i = 0; i < damage; i++)
        {
            if (currentHealth <= 0)
                return;

            currentHealth--;

            // Disable the last active heart
            healthIcons[currentHealth].SetActive(false);

            // Check for death
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");

        // Load Game Over scene
        SceneLoader();
    }
}