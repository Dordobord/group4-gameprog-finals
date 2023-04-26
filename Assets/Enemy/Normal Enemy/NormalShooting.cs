using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShooting : MonoBehaviour
{
    [SerializeField] public GameObject Bullet;
    [SerializeField] public Transform BulletSpawnSpot;
    void Start()
    {
        InvokeRepeating("Shoot", 0.1f, 1f);
    }

    public void Shoot()
    {
        Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
    }
}
