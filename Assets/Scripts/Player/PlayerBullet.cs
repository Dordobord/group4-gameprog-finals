using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] public float speed = 10f;
    public float damage = 20;
    public Rigidbody2D rb;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dir = TopDownController.BulletDir;
        score = PlayerPrefs.GetInt("PlayerScore");
        rb.velocity = dir * speed;

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

                score += enemy.TakeDamage(damage);
                PlayerPrefs.SetInt("PlayerScore", score);
                PlayerPrefs.Save();

            }
            Destroy(gameObject);
        }
    }
}
