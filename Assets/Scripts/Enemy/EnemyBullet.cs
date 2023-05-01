using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    Rigidbody2D Rb;
    private int damage = 20;
    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.velocity = transform.right * speed;
    }
    void Update()
    {
        if (transform.position.x > Boundary.UPboundary.x || transform.position.x < Boundary.LBound.x ||
            transform.position.y > Boundary.UPboundary.y || transform.position.y < Boundary.LBound.y)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag == "Player" || hit.gameObject.tag == "PlayerBullet")
        {
            if (hit.gameObject.tag == "Player")
            {
                PlayerMech player = hit.gameObject.GetComponent<PlayerMech>();

                player.TakeDamage(damage);

            }
            Destroy(gameObject);
        }
    }
}
