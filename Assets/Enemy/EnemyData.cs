
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Color Color;
    public float HP = 100;
    public float screenTime = 5;
    public float shield = 0;
    public float SpawnChance = 50;
}
