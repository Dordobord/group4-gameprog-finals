using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KnightSword : MonoBehaviour
{
    EnemyMechanics enemy;
    public void OnTriggerEnter2D(Collider2D hit)
    {
        enemy = GetComponentInParent<EnemyMechanics>();

        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
        {
            if (hit.gameObject.tag == "Player")
            {
                hit.gameObject.GetComponent<PlayerMech>().TakeDamage(enemy.damage);
            }
            Destroy(gameObject);
        }
    }
}
