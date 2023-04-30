using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.CompilerServices;
using UnityEngine.U2D;

public class PlayerMech : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;
    [SerializeField] public Transform BulletSpawnSpot;
    Rigidbody2D rb;

    PlayerHealth PlayerHealth;
    float dashPower = 100, dashTime = 2, dashCost = 20;
    float shootCD = 1f, shootCost = 5;
    public bool canShoot, canDash;
    IEnumerator Coroutine;
    public GameObject Bar;
    Manabar manabar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canDash= true;
        manabar = Bar.GetComponent<Manabar>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shooting
        if (canShoot)
        {
            // Check if the fire button is pressed
            if (Input.GetButtonDown("Fire1"))
            {
                    Coroutine = Shoot(shootCD);
                    StartCoroutine(Coroutine);
            }
        }
        else
            Debug.Log("In Cooldown.");

        //Dashing
        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    Coroutine = Dash(dashTime);
                    StartCoroutine(Coroutine);
            }
        }
        else
            Debug.Log("In Cooldown.");

        if (Input.GetKeyDown(KeyCode.O)) // HealthBar Tester
        {
            manabar.SpendMana(20);
        }
        else if (Input.GetKeyDown(KeyCode.P))
            manabar.RecoverMana(20);
    }

    IEnumerator Shoot(float CD)
    {
        if (manabar.manaAmount >= shootCost)
        {
            canShoot = false;
            manabar.SpendMana(shootCost);
            Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
            yield return new WaitForSeconds(CD);
            canShoot = true;
        }
        else
            Debug.Log("Insufficient Mana");
    }

    IEnumerator Dash(float CD)
    {
        if (manabar.manaAmount >= dashCost)
        {
            canDash = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            manabar.SpendMana(20);
            rb.velocity = TopDownController.BulletDir * dashPower;
            GetComponent<PolygonCollider2D>().enabled = true;
            yield return new WaitForSeconds(CD);
            canDash = true;
        }
        else
            Debug.Log("Insufficient Mana");
    }


    public void TakeDamage(int damage)
    {
        PlayerHealth.currentHealth -= damage;
        Debug.Log("Player Hit! HP = " + PlayerHealth.currentHealth);
        PlayerPrefs.SetFloat("PlayerHP", PlayerHealth.currentHealth);
        PlayerPrefs.Save();

        if (PlayerHealth.currentHealth == 0)
            Die();
    }

    void Die()
    {
        Debug.Log("You have Died. T^T");
        Debug.Log("Level Restart.");
        Scene CurrentLvl = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrentLvl.name);
    }
}
