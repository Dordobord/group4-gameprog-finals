using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    Rigidbody2D Rb;
    private int damage, n;
    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = transform.right * speed;
        n = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if (transform.position.x > Boundary.UPboundary.x || transform.position.x < Boundary.LBound.x ||
            transform.position.y > Boundary.UPboundary.y || transform.position.y < Boundary.LBound.y)
        {
            Destroy(gameObject);
        }
        if (n == 1)
            damage = 10;
        else if (n == 2)
            damage = 15;
        else if (n == 3)
            damage = 30;
    }

    public void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
        {
            if (hit.gameObject.tag == "Player")
            {
                PlayerHealth player = hit.gameObject.GetComponent<PlayerHealth>();

                player.TakeDamage(damage);

            }
            Destroy(gameObject);
        }
    }
}
