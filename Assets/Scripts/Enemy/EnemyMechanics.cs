using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMechanics : MonoBehaviour
{
    public float HP;
    public GameObject loot;
    private int score;
    public List<EnemyScript> EnemyList;
    EnemyScript Enemy;
    private bool chooseDir = false;
    private Vector3 randomDir;
    public int n;

    // Start is called before the first frame update
    public enum EnemyState
    {
        Wander,
        Follow
    };
    public EnemyState currState;
    void Start()
    {
        n = Random.Range(0,EnemyList.Count-1);
        Enemy = EnemyList[n];
        Enemy.TargetPlayer();
        HP = Enemy.Health;
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
        }
        if (IsPlayerInRange(Enemy.Range))
            currState = EnemyState.Follow;
        else if (!IsPlayerInRange(Enemy.Range))
            currState = EnemyState.Wander;
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
    public int TakeDamage(float damage)
    {
        int score;
        HP -= damage;
        Debug.Log("Enemy Hit! HP = " + HP);

        if (HP == 0)
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
