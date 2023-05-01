using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] public float speed = 60f;
    public float damage = 15;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dir = TopDownController.BulletDir;
        rb.velocity = dir * speed;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > Boundary.UPboundary.x || transform.position.x < Boundary.LBound.x ||
            transform.position.y > Boundary.UPboundary.y || transform.position.y < Boundary.LBound.y)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D (Collider2D hit)
    {

        if (hit.gameObject.tag == "Enemy" || hit.gameObject.tag == "EnemyBullet")
        {
            if (hit.gameObject.tag == "Enemy")
            {
                EnemyMechanics enemy = hit.gameObject.GetComponent<EnemyMechanics>();

                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
