using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour
{
    public float MANA_MAX = 100;
    public float manaAmount = 100;
    private float manaRegenAmount = 5;
    Image barImage;
    public GameObject mbar;

    private void Start()
    {
        barImage = mbar.GetComponent<Image>();

    }
    private void Update()
    {

        barImage.fillAmount = GetManaNormalized();
        manaAmount = Mathf.Clamp(manaAmount, 0, 100);
        if (Input.GetKeyDown(KeyCode.Minus)) // HealthBar Tester
        {
            SpendMana(20);
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
            RecoverMana(20);

    }

    public void FixedUpdate()
    {
        manaAmount += manaRegenAmount * Time.fixedDeltaTime;
    }

    public void SpendMana(float amount)
    {
        manaAmount -= amount;
    }
    public void RecoverMana(float amount)
    {
        manaAmount += amount;
    }
    public float GetManaNormalized()
    {
        return manaAmount / MANA_MAX;
    }
}
