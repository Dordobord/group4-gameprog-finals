using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    // Enemy Movement variables
    public Transform player;
    private Vector2 movement;
    public float moveSpeed = 3f;
    
    //Collider enemy to player variables
    public PlayerHealth playerHealth; 
    public int damage = 10;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); //Reference
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;

        
        // Faces the player when following them
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        //Movement
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate(){
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) //When player collides it will deal damage to the player
    {
        if(collision.gameObject.tag == "Player")
            {
                playerHealth.TakeDamage(damage);
            }
    }
}
