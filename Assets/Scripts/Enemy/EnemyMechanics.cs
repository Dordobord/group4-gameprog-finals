using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMechanics : MonoBehaviour
{
    public string EnemyName; 
    public float HP;
    public GameObject loot;
    private int score;
    public List<EnemyScript> EnemyList;
    EnemyScript Enemy;
    private bool chooseDir = false;
    private Vector3 randomDir;
    public int n;
    public SpriteRenderer rend;
    Collider2D swordRange;
   

    // Start is called before the first frame update
    public enum EnemyState
    {
        Wander,
        Follow,
        Attack
    };
    public EnemyState currState;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        swordRange = GetComponentInChildren<Collider2D>();
        swordRange.enabled = false;
        n = Random.Range(0,EnemyList.Count);
        Enemy = EnemyList[n];
        EnemyName = Enemy.name;
        Enemy.TargetPlayer();
        HP = Enemy.Health;
        rend.sprite = Enemy.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }
        if (IsPlayerInRange(Enemy.Range))
            currState = EnemyState.Follow;
        else if (!IsPlayerInRange(Enemy.Range))
            currState = EnemyState.Wander;
        else if (IsPlayerInRange(Enemy.AtkRange))
            currState = EnemyState.Attack;
    }
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, Enemy.target.transform.position) <= range;
    }
    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0, 0, Random.Range(0,360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }
    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * Enemy.speed * Time.deltaTime;
        if (IsPlayerInRange(Enemy.Range))
    
    {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, Enemy.target.transform.position, Enemy.speed * Time.deltaTime);
    }

    void Attack()
    {
        if (Enemy.name == "KnightLvl1")
        {
            Debug.Log("Attack");
        }
        if (Enemy.name == "SpearmenLvl1")
        {
            Debug.Log("Shoot");
        }
    }
    public int TakeDamage(float damage)
    {
        HP -= damage;
        Debug.Log("Enemy Hit! HP = " + HP);

        if (HP <= 0)
            return Die();

        else return 0;
    }
    public int Die()
    {
        Debug.Log("Enemy Killed");

        Destroy(this.gameObject);
        float rand = Random.value;
        if (rand <= 0.40)
        {
            Debug.Log("Drop Loot.");
            Instantiate(loot, this.transform.position, Quaternion.identity);
        }
        return score;
    }
}
