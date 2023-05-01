using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PowerUp : MonoBehaviour
{
    [SerializeField] public PowerUpScript buffEffect;
    public List<PowerUpScript> PowerUPs = new List<PowerUpScript>();
    private SpriteRenderer rend;

    private void Start()
    {
        float dropRoll = UnityEngine.Random.Range(0f, 1f);
        float cumulativeDropChance = 0f;
        Shuffle(PowerUPs);
        foreach (PowerUpScript buff in PowerUPs)
        {
            cumulativeDropChance += buff.DropChance;

            if (dropRoll <= cumulativeDropChance)
            {
                buffEffect = buff;
            }
        }

        rend = GetComponent<SpriteRenderer>();
        rend.sprite = buffEffect.Sprite;
    }

    private void Update()
    {
        if (transform.position.y < Boundary.UPboundary.y * -1)
        {
            Destroy(this.gameObject);
        }
    }

    void Shuffle(List<PowerUpScript> PuPs)
    {
        PowerUpScript buff;
        int n = PuPs.Count;

        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            buff = PuPs[n];
            PuPs[n] = PuPs[k];
            PuPs[k] = buff;
        }
    }
}
