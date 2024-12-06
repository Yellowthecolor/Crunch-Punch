using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Flour")]

public class Flour : PowerUP
{
    [SerializeField] float speedBonus;
    [SerializeField] float timeChange;

    public override void PowerUp(GameObject target)
    {
        target.GetComponent<Guy>().SetDefaultSpeed(speedBonus);
        Time.timeScale = timeChange;
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDefaultSpeed(-speedBonus);
        Time.timeScale = 1f;
    }
}
