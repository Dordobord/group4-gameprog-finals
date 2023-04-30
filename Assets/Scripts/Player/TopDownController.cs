using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer rd;

    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;
    public static Vector2 BulletDir;

    [SerializeField] public float walkSpeed;
    [SerializeField] public float frameRate;
    
    float idleTime;
    Vector2 direction;

    void Start()
    {
        walkSpeed = 5;
        BulletDir = Vector2.down;
    }

    void Update()
    {
        //Get the Direction of Input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //Set Walk Based on Direction
        body.velocity = direction * walkSpeed;

        //Flipping Character Animation
        Flip();
        BulletDir = GetBulletDirection(BulletDir).normalized;
        SetSprite();
    }

    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();
        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int frame = (int)((playTime * frameRate)) % directionSprites.Count;
            rd.sprite = directionSprites[frame];
        }
        else
            idleTime = Time.time;
    }

    void Flip()
    {
        if (!rd.flipX && direction.x < 0)
        {
            rd.flipX = true;
        }
        else if (rd.flipX && direction.x > 0)
        {
            rd.flipX = false;
        }
    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;
        if (direction.y > 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                selectedSprites = neSprites;
            else
                selectedSprites = nSprites;
        }
        if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                selectedSprites = seSprites;
            else
                selectedSprites = sSprites;
        }
        else if (direction.y == 0)
        {
            if (Mathf.Abs(direction.x) > 0)
                selectedSprites = eSprites;
        }

        return selectedSprites;
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
        if (rd.flipX == true && direction.x != 0)
            Dir.x *= -1;

        return Dir;
    }

}
