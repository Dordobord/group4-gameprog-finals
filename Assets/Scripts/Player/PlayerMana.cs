using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{

    public static int maxMana;
    public static int currentMana;
    //public Healthbar ManaBar;

    void Start()
    {
        maxMana = 100;
        currentMana = maxMana;
        //ManaBar.setMaxMana(maxMana);
    }

    void Update()
    {
        currentMana = Mathf.Clamp(currentMana, 0, 100);
        if (Input.GetKeyDown(KeyCode.P)) // ManaBar Tester
        {
            TakeDamage(20);
        }

    }

    public void Recover(int mpPlus)
    {
        currentMana += mpPlus;
        //ManaBar.setHealth(currentMana);
    }

    public void AddMaxMP(int mpMaxAdd)
    {
        maxMana += mpMaxAdd;
        //manaBar.setMaxMana(maxMana);
    }

    public void TakeDamage(int damage)
    {
        currentMana -= damage;
        //ManaBar.setMana(currentMana);
    }
}
