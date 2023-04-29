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
    PlayerMana PlayerMana;

    float dashPower = 60, dashTime = 2;
    public float shootCD = 2;
    public bool canShoot, canDash;
    IEnumerator Coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        canDash= true;
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
    }
    private void FixedUpdate()
    {
        PlayerMana.currentMana += 1;
    }
    IEnumerator Shoot(float CD)
    {
        if (PlayerMana.currentMana >= 20)
        {
            canShoot = false;
            Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
            yield return new WaitForSeconds(CD);
            canShoot = true;
        }
        else if (PlayerMana.currentMana < 20)
            Debug.Log("Insufficient Mana");
    }

    IEnumerator Dash(float CD)
    {
        if (PlayerMana.currentMana >= 20)
        {
            canDash = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            PlayerMana.currentMana -= 20;
            rb.velocity = TopDownController.BulletDir * dashPower;
            PlayerMana.currentMana -= 20;
            GetComponent<PolygonCollider2D>().enabled = true;
            yield return new WaitForSeconds(CD);
            canDash = true;
        }
        else if (PlayerMana.currentMana < 20)
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
        GetComponent<ScoreManager>().FinalScore();
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
