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
        rend.color = buffEffect.color;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < Boundary.UPboundary.y * -1)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            if (buffEffect.name == "HealthBuff")
            {
                buffEffect.Apply(collision.gameObject);
                Destroy(gameObject);
            }

            else if (buffEffect.name == "BulletBuff")
            {
                speed = 0;
                StartCoroutine (UpgradeBuffs(collision.gameObject));
            }
        }
    }
    IEnumerator UpgradeBuffs (GameObject target)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        buffEffect.Apply(target);
        Debug.Log("Coroutine Start. " + Time.time);
        float a = Time.time;
        yield return new WaitForSeconds(5);
        Debug.Log("Coroutine Stopped.");
        buffEffect.Remove(target);

        Destroy(gameObject);
    }
}
