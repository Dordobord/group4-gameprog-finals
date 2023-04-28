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
        if (Input.GetKeyDown(KeyCode.P)) // HealthBar Tester
        {
            TakeDamage(20);
        }

    }

    public void Heal(int hpPlus)
    {
        currentHealth += hpPlus;
        healthBar.setHealth(currentHealth);
    }

    public void AddMaxHP(int hpMaxAdd)
    {
        maxHealth += hpMaxAdd;
        healthBar.setMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
