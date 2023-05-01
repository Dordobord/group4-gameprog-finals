using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer rd;
    public Animator animator;

    public static Vector2 BulletDir;

    public static float walkSpeed;
    [SerializeField] public float frameRate;
    
    public Vector2 direction;
    bool isRight = false;

    void Start()
    {
        walkSpeed = 5;
        BulletDir = Vector2.down;
    }

    void Update()
    {

        //Get the Direction of Input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        animator.SetBool("isMoving", true);

        //Set Walk Based on Direction
        body.velocity = direction * walkSpeed;

        //Flipping Character Animation
        if (direction.x < 0 && Mathf.Floor(transform.rotation.y) != 0)
        {
            Flip();
            isRight = false;
        }
        else if (direction.x > 0 && Mathf.Floor(transform.localRotation.y) == 0)
        {
            Flip();
            isRight = true;
        }

        BulletDir = GetBulletDirection(BulletDir).normalized;
    }


    void Flip()
    {
        transform.Rotate(0, 180, 0);
    }


    Vector2 GetBulletDirection(Vector2 direct)
    {
        Vector2 Dir = direct;
        if (direction.y > 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                Dir = new Vector2(1, 1);
            else
                Dir = new Vector2(0, 1);
        }
        if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                Dir = new Vector2(1, -1);
            else
                Dir = new Vector2(0, -1);
        }
        else if (direction.y == 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                Dir = new Vector2(1, 0);
        }
        if (isRight == false && direction.x != 0)
            Dir.x *= -1;

        return Dir;
    }

}
