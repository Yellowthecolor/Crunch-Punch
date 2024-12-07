using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Syringe")]
public class Syringe : PowerUp
{

    [SerializeField] float strengthBonus;
    [SerializeField] int dropChance;
    [SerializeField] Sprite sprite;

    public override void PowerBuff(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageMultiplier(strengthBonus);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageMultiplier(-strengthBonus);
    }

    public override int GetDropChance()
    {
        return dropChance;
    }

    public override Sprite GetItemSprite(){
        return sprite;
    }
}
