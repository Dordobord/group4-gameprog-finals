using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu (fileName ="PowerUPs", menuName ="BasicPowerUp")]
public abstract class PowerUPScriptable : ScriptableObject
{
    public Sprite Sprite;
    public string PowerUP_Type;
    public float DropChance;
    public float amount;

}

