using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMechanics : MonoBehaviour
{
    public string EnemyName; 
    public float HP;
    public int damage;
    public GameObject loot;
    private int score;
    public List<EnemyScript> EnemyList;
    EnemyScript Enemy;
    private bool chooseDir = false;
    private Vector3 randomDir;
    int n;
    public SpriteRenderer rend;
    Vector3 currPos;
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
        n = Random.Range(0,EnemyList.Count);
        Enemy = EnemyList[n];
        EnemyName = Enemy.name;
        Enemy.TargetPlayer();
        HP = Enemy.Health;
        rend.sprite = Enemy.sprite;
        damage = Enemy.AtkDmg;
    }

    // Update is called once per frame
    void Update()
    {
        currPos = transform.position;
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
        else if (IsPlayerInRange(Enemy.Range) && IsPlayerInRange(Enemy.AtkRange))
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
            GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        if (Enemy.name == "SpearmenLvl1")
        {
            Debug.Log("Shoot");
        }
    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
        Debug.Log("Enemy Hit! HP = " + HP);

        if (HP <= 0)
            Die();

    }
    public float rand;
    public void Die()
    {
        Debug.Log("Enemy Killed");

        rand = Random.value; Debug.Log("Drop Loot? " +rand);
        if (rand < 0.40)
        {

            Instantiate(loot, currPos, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}
