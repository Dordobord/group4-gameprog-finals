using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public string buff;
    PowerUp powerUp;
    private void Start()
    {
        powerUp = GetComponent<PowerUp>();
        buff = powerUp.buffEffect.name;
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            PlayerMech player = hit.gameObject.GetComponent<PlayerMech>();
            if (buff == "AddBulletSize" && player.BulletSizeBuff <= 10)
            {
                player.Bullet.transform.localScale = player.Bullet.transform.localScale + new Vector3(1, 1, 0);
                player.BulletSizeBuff++;
                Destroy(this.gameObject);
            }

            else if (buff == "AddDmg" && player.DamageBuff <= 10)
            {
                player.Bullet.GetComponent<PlayerBullet>().damage += 5;
                player.DamageBuff++;
                Destroy(this.gameObject);
            }
            else if (buff == "MaxHpUP" && player.MaxHpBuff <= 10)
            {
                hit.gameObject.GetComponent<PlayerHealth>().AddMaxHP();
                player.MaxHpBuff++;
                Destroy(this.gameObject);
            }
            else if (buff == "MaxMana" && player.MaxManaBuff <= 10)
            {
                player.Bar.GetComponent<Manabar>().AddMaxMana();
                player.MaxManaBuff++;
                Destroy(this.gameObject);
            }
            else if (buff == "HealthUP")
            {
                hit.gameObject.GetComponent<PlayerHealth>().Heal();
                Destroy(this.gameObject);
            }
            else if (buff == "Invincibility")
            {
                StartCoroutine(Invincibility(hit.gameObject));
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (buff == "Key")
            {
                player.hasKey = true; 
                Destroy(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
    }
    IEnumerator Invincibility(GameObject player)
    {
        player.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        player.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}