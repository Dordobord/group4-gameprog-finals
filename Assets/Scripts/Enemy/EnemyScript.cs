using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu (fileName = "Enemy")]
public class EnemyScript : ScriptableObject
{
    public GameObject target;
    public Sprite sprite;
    public float speed;
    public int Health;
    public int AtkDmg;
    public float AtkRange;
    public float Range = 10;

    public void TargetPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

}
