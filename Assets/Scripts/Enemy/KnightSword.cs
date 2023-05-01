using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KnightSword : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
        {
            if (hit.gameObject.tag == "Enemy")
            {
                Debug.Log("Attacked");
            }
            Destroy(gameObject);
        }
    }
}
