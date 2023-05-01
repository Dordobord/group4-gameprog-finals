using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    public GameObject playerBullet;
    void Start()
    {
        playerBullet.GetComponent<PlayerBullet>().damage = 15;
        playerBullet.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
