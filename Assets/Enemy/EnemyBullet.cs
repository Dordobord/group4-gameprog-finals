using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    private float damage = 20;
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < Boundary.UPboundary.y)
        {
            Destroy(this.gameObject);
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
