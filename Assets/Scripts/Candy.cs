using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Candy")]

public class Candy : PowerUP
{
    [SerializeField] float damageReduction;

    public override void PowerUp(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageReduction(damageReduction);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageReduction(-damageReduction);
    }
}
