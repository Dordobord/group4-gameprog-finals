using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteEnemyShooting : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;
    [SerializeField] public Transform BulletSpawnSpot;
    void Start()
    {
        InvokeRepeating("Shoot", 0.01f, 3f);
    }

    public void Shoot()
    {
        Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
        Instantiate(Bullet, new Vector3(BulletSpawnSpot.position.x + 0.5f,BulletSpawnSpot.position.y,BulletSpawnSpot.position.z), Quaternion.Euler(0, 0, 10));
        Instantiate(Bullet, new Vector3(BulletSpawnSpot.position.x - 0.5f, BulletSpawnSpot.position.y, BulletSpawnSpot.position.z), Quaternion.Euler(0,0,-10));
    }
}
