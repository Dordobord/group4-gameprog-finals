using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUPScriptable buffEffect;
    public List<PowerUPScriptable> PowerUPs = new List<PowerUPScriptable>();
    private SpriteRenderer rend;
    private int speed = 2;

    private void Start()
    {
        int randomvalue = System.Convert.ToInt32(Random.value * 10);
        buffEffect = PowerUPs[randomvalue % PowerUPs.Count];
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = buffEffect.Sprite;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < Boundary.UPboundary.y * -1)
        {
            Destroy(this.gameObject);
        }
    }
 

}
