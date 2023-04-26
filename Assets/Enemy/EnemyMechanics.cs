using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanics : MonoBehaviour
{
    public float speed = 0.01f;
    public List<EnemyData> EnemyRandomizer = new List<EnemyData>();
    EnemyData Enemy;
    public float HP;
    public float shield;
    public Color color;
    private SpriteRenderer rend;
    private int score;
    public GameObject loot;

    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        rend = GetComponent<SpriteRenderer>();
        Enemy = GetStats();
        HP = HP + Enemy.HP;
        shield = Enemy.shield;
        color = Enemy.Color;
        rend.color = color;

    }

    // Update is called once per frame
    void Update()
    {
        HP = Mathf.Clamp(HP, 0, 100);
        if (Time.time > 5)
        GetComponent<Collider2D>().enabled = true;
        transform.Translate(Vector2.down * (speed/5) * Time.deltaTime);
        if (transform.position.y < Boundary.LBound.y)
        {
            Destroy(this.gameObject);
        }
    }

    public EnemyData GetStats()
    {   
        float TotalChance = 0;
        foreach (var Enemy in EnemyRandomizer)
        {
            TotalChance += Enemy.SpawnChance;
        }
        float randomValue = Random.Range(0, TotalChance);
        float currentSpawnRate = 0;
        foreach (var Enemy in EnemyRandomizer)
        {
            currentSpawnRate += Enemy.SpawnChance;
            if (randomValue < currentSpawnRate)
            {
                return Enemy;
            }
        }
        return null;
    }
    public int TakeDamage(float damage)
    {
        int score;
        HP -= damage;
        Debug.Log("Enemy Hit! HP = "+HP);

        if (HP <= 0)
            return Die();

        else return 0;

    }

    public GameObject player;
    public int Die()
    {
        Debug.Log("Enemy Killed");
        float DistanceTraveled = Boundary.UPboundary.y - this.transform.position.y;
        score += System.Convert.ToInt32 (Mathf.Lerp(1,10,DistanceTraveled/Boundary.UPboundary.y));

        Destroy(this.gameObject);
        if (Enemy.name == "Buff Enemy")
        {
            Debug.Log("Drop Loot.");
            Instantiate(loot, this.transform.position, Quaternion.identity);
        }
        return score;
    }
}
