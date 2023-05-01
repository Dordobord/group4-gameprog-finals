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

    PlayerHealth PHealth;
    float dashPower = 20, dashTime = 2, dashCost = 20;
    float shootCD = 0.5f, shootCost = 5;
    public bool canShoot, canDash, hasKey;
    IEnumerator Coroutine;
    public GameObject Bar;
    Manabar manabar;
    public short BulletSizeBuff=0, DamageBuff=0, MaxHpBuff=0, MaxManaBuff=0;
    void Start()
    {
        PHealth = GetComponent<PlayerHealth>();
        canShoot = true;
        canDash= true;
        manabar = Bar.GetComponent<Manabar>();
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Shooting
        if (canShoot)
        {
            // Check if the fire button is pressed
            if (Input.GetButton("Fire1"))
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

        if (Input.GetKeyDown(KeyCode.O)) // ManaBar Tester
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

        if (hasKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator Dash(float CD)
    {
        if (manabar.manaAmount >= dashCost)
        {
            canDash = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            manabar.SpendMana(20);
            TopDownController.walkSpeed = dashPower;
            yield return new WaitForSeconds(0.3f);
            GetComponent<PolygonCollider2D>().enabled = true;
            TopDownController.walkSpeed = 5;
            yield return new WaitForSeconds(CD);
            canDash = true;
        }
        else
            Debug.Log("Insufficient Mana");
    }
}
