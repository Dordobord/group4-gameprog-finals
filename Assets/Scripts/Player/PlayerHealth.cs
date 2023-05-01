using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public static int maxHealth;
    public static int currentHealth;
    public Healthbar healthBar;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);
        if (Input.GetKeyDown(KeyCode.Minus)) // HealthBar Tester
        {
            TakeDamage(20);
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
            Heal();

    }

    public void Heal()
    {
        currentHealth += 20;
        healthBar.setHealth(currentHealth);
    }

    public void AddMaxHP()
    {
        maxHealth += 50;
        currentHealth += 50;
        healthBar.setMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
