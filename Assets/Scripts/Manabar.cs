using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manabar : MonoBehaviour{
    private Mana mana;
    private Image barImage;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();

        mana = new Mana();
    }

    private void Update()
    {
        mana.Update();

        barImage.fillAmount = mana.GetManaNormalized();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mana.SpendMana(15);
        }
    }
 
}
public class Mana{   
    public const int MANA_MAX = 100;
    private float manaAmount;
    private float manaRegenAmount;

    public Mana(){
        manaAmount = 0;
        manaRegenAmount = 30f;
    }

    public void Update()
    {
        manaAmount += manaRegenAmount * Time.deltaTime;
    }
    public void SpendMana(int amount)
    {
        if (manaAmount >= amount){
            manaAmount -= amount;
        }
    }
    public float GetManaNormalized()
    {
        return manaAmount / MANA_MAX;
    }

}
