using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    BossMechanics boss;
    public void OnTriggerEnter2D(Collider2D hit)
    {
        boss = GetComponentInParent<BossMechanics>();

        if(this.gameObject.tag == "Bsword")
        {
            if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
            {
                if (hit.gameObject.tag == "Player")
                {
                    hit.gameObject.GetComponent<PlayerHealth>().TakeDamage(boss.MeleeDmg);
                }
                Destroy(gameObject);
            }
        }
        if (this.gameObject.tag == "Baoe")
        {
            if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
            {
                if (hit.gameObject.tag == "Player")
                {
                    hit.gameObject.GetComponent<PlayerHealth>().TakeDamage(boss.AxeDmg);
                }
                Destroy(gameObject);
            }
        }

    }
}
