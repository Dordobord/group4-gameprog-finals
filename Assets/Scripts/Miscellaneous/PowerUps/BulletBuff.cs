using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "BulletBuff", menuName = "PowerUps/BulletBuff")]
public class BulletBuff : PowerUPScriptable
{
    public override void Apply(GameObject target)
    {
        PlayerMech Player = target.GetComponent<PlayerMech>();   
        GameObject bullet = Player.Bullet;
        PlayerBullet bulletSetting = bullet.GetComponent<PlayerBullet>();
        for (int i = 0; i < bullet.transform.childCount; i++)
        {
            bullet.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public override void Remove(GameObject target)
    {
        PlayerMech Player = target.GetComponent<PlayerMech>();
        GameObject bullet = Player.Bullet;
        PlayerBullet bulletSetting = bullet.GetComponent<PlayerBullet>();
        for (int i = 0; i < bullet.transform.childCount; i++)
        {
            bullet.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
