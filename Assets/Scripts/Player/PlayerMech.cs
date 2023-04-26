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
    public static float Health, Mana, MaxHP, MaxMP;
    float dashDist = 8, dashTime = 2, timer;

    void Start()
    {
        Health = 100; MaxHP= 100;
        Mana = 100; MaxMP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    //Player HP Updates
        Health = PlayerPrefs.GetFloat("PlayerHP");
        Health = Mathf.Clamp(Health, 0, 100);
        PlayerPrefs.SetFloat("PlayerHP", Health);
        PlayerPrefs.Save();
    //Clamping HP and MP
        Health = Mathf.Clamp(Health, 0,100);
        Mana = Mathf.Clamp(Health, 0, 100);
    //Shooting
        Shoot();
    //Dashing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Mana >= 20)
                Dash();
            else
                Debug.Log("Insufficient Mana");
        }
    }
    private void FixedUpdate()
    {
        Mana += 1;
    }
    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
    }

    void Dash()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
        Mana -= 20;
        Vector2 CurrentPos = transform.position;
        Vector2 FinalPos = CurrentPos + new Vector2(TopDownController.BulletDir.x * dashDist, TopDownController.BulletDir.y * dashDist);
        transform.position = FinalPos;
        Mana -= 20;
        GetComponent<PolygonCollider2D>().enabled = true;
    }


    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Player Hit! HP = " + Health);
        PlayerPrefs.SetFloat("PlayerHP", Health);
        PlayerPrefs.Save();

        if (Health == 0)
            Die();
    }
    void Die()
    {
        Debug.Log("You have Died. T^T");
        GetComponent<ScoreManager>().FinalScore();
        SceneManager.LoadScene(0);
    }
}
