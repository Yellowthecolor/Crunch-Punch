using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PowerUps/Syringe")]
public class Syringe : PowerUP
{

    [SerializeField] float strengthBonus;

    public override void PowerUp(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageMultiplier(strengthBonus);
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDamageMultiplier(-strengthBonus);
    }
}
