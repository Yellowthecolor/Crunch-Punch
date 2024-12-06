using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Leaf")]

public class Leaf : PowerUP
{
    [SerializeField] float healthRegenBonus;

    public override void PowerUp(GameObject target)
    {
        target.GetComponent<Guy>().SetHealthRegenBonus(healthRegenBonus);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetHealthRegenBonus(-healthRegenBonus);
    }
}
