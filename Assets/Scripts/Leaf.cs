using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Leaf")]

public class Leaf : PowerUp
{
    [SerializeField] float healthRegenBonus;
    [SerializeField] int dropChance;
    [SerializeField] Sprite sprite;
    [SerializeField] string displayText = "Maple Leaf\nBring Me A Higher Love";



    public override void PowerBuff(GameObject target)
    {
        target.GetComponent<Guy>().SetHealthRegenBonus(healthRegenBonus);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetHealthRegenBonus(-healthRegenBonus);
    }

    public override int GetDropChance()
    {
        return dropChance;
    }

    public override string GetItemString()
    {
        return displayText;
    }

    public override Sprite GetItemSprite(){
        return sprite;
    }
}
