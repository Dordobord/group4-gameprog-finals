using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class BossMechanics : MonoBehaviour
{
        public float HP = 750;
        public int MeleeDmg = 35, AxeDmg = 30;
        public float speed = 4;
        public GameObject Bullet, target;
        public Transform BulletSpawnSpot;
        private Vector3 respawnPoint;
        int n, bumpDmg = 10;
        Vector3 currPos;
        float MRange, ARange;
        bool canShoot = true;
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
            target = GameObject.FindGameObjectWithTag("Player");
            respawnPoint = transform.position;

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
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (IsPlayerInRange(MRange) && canShoot)
                StartCoroutine(Shoot());
            {
                currState = EnemyState.Attack;
            }
        }

        void Attack()
        {
                StartCoroutine(Melee());
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
                Instantiate(Bullet, BulletSpawnSpot.position, Quaternion.identity);
                yield return new WaitForSeconds(6);
                canShoot = true;
        }
        IEnumerator AoE()
        {
            GetComponentInChildren<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(1);
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(10);
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
            GetComponentInChildren<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(1);
            GetComponentInChildren<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(2);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerMech>().TakeDamage(bumpDmg);
            }
        }
}
