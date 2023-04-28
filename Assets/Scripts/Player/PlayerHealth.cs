using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // HealthBar Tester
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
