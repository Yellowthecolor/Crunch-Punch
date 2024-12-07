using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Leaf")]

public class Leaf : PowerUp
{
    [SerializeField] float healthRegenBonus;
    [SerializeField] int dropChance;
    [SerializeField] Sprite sprite;



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

    public override Sprite GetItemSprite(){
        return sprite;
    }
}
