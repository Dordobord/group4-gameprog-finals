using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUPScriptable : ScriptableObject
{
    public Sprite Sprite;
    public string PowerUP_Type;
    public float DropChance;
    public Color color;

    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}
