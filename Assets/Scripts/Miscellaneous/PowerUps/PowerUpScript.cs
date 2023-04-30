using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Basic PowerUp", menuName = "PowerUps")]
public class PowerUpScript : ScriptableObject
{
    public Sprite Sprite;
    public float DropChance;
    public string LootName;
    public int amount;
}
