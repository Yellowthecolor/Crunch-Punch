using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Candy")]

public class Candy : PowerUp
{
    [SerializeField] float damageReduction;
    [SerializeField] int dropChance;
    [SerializeField] Sprite sprite;
    [SerializeField] string displayText = "You Can't Feel Jack";



    public override void PowerBuff(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageReduction(damageReduction);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageReduction(-damageReduction);
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
