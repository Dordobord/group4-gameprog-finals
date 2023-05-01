using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BossMechanics : MonoBehaviour
{
        public float HP = 750;
        public int MeleeDmg = 35, AxeDmg = 30;
        public float speed = 2;
        public GameObject Bullet, target, sRange, aRange;
        public Transform BulletSpawnSpot;
        private Vector3 respawnPoint;
        int n, bumpDmg = 10;
        Vector3 currPos;
        float MRange = 1, ARange = 10, angle;
        public bool canShoot, canAtk, canAoe;
        // Start is called before the first frame update
        public enum EnemyState
        {
            Return,
            Follow,
            Attack
        };
        public EnemyState currState;
        void Start()
        {
            canShoot = true; canAtk = true; canAoe = true;
            target = GameObject.FindGameObjectWithTag("Player");
            respawnPoint = transform.position;
            aRange.SetActive(false);
            sRange.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {
            currPos = transform.position;
            switch (currState)
            {
                case (EnemyState.Return):
                    Return();
                    break;
                case (EnemyState.Follow):
                    Follow();
                    break;
                case (EnemyState.Attack):
                    Attack();
                    break;
            }
            if (IsPlayerInRange(ARange) && !IsPlayerInRange(MRange))
                currState = EnemyState.Follow;
            else if (!IsPlayerInRange(ARange))
                currState = EnemyState.Return;
            else if (IsPlayerInRange(ARange) && IsPlayerInRange(MRange))
                currState = EnemyState.Attack;

            if (HP <= 50)
                StartCoroutine(AoE());
        //Get angle to player
        Vector2 direction = Vector3.right - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
        private bool IsPlayerInRange(float range)
        {
            return Vector3.Distance(transform.position, target.transform.position) <= range;
        }
        void Return()
        {
            transform.position = Vector2.MoveTowards(transform.position, respawnPoint, speed * Time.deltaTime);
            if (IsPlayerInRange(ARange))

            {
                currState = EnemyState.Follow;
            }
        }

        void Follow()
        {   if(canShoot)
            StartCoroutine(Shoot());
            else if (IsPlayerInRange(ARange) && !canShoot)
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            else if (IsPlayerInRange(MRange) && IsPlayerInRange(ARange))
            {
                currState = EnemyState.Attack;
            }
        }

        void Attack()
        {
            StartCoroutine(Melee());
            if (!IsPlayerInRange(MRange) && IsPlayerInRange(ARange))
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(MRange) && !IsPlayerInRange(ARange))
            {
                currState = EnemyState.Return;
            }
        }
        public void TakeDamage(float damage)
        {
            HP -= damage;
            Debug.Log("Enemy Hit! HP = " + HP);

            if (HP <= 0)
                Die();

        }
        IEnumerator Shoot()
        {
                canShoot = false;
                Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                Bullet.transform.position = Vector2.MoveTowards(BulletSpawnSpot.position, target.transform.position, speed * Time.deltaTime);
                yield return new WaitForSeconds(6);
                canShoot = true;
        }
        IEnumerator AoE()
        {
            if (canAoe)
            {
                canAoe= false;
                aRange.SetActive(true);
                yield return new WaitForSeconds(1);
                aRange.SetActive(false);
                yield return new WaitForSeconds(10);
                canAoe= true;
            }
        }
        public void Die()
        {
            StartCoroutine(BossDead());
        }
        IEnumerator BossDead()
        {
            Debug.Log("Boss Killed. You Won");
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(0);
        }
        IEnumerator Melee()
        {
        if (IsPlayerInRange(MRange) && canShoot)
            StartCoroutine(Shoot());
        {
            currState = EnemyState.Attack;
        }
        if (canAtk)
        {
            canAtk = false;
            sRange.SetActive(true);
            yield return new WaitForSeconds(1);
            sRange.SetActive(false);
            yield return new WaitForSeconds(2);
            canAtk = true;
        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bumpDmg);
            }
        }
}
