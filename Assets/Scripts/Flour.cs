using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/Flour")]

public class Flour : PowerUp
{
    [SerializeField] float speedBonus;
    [SerializeField] float timeChange;
    [SerializeField] int dropChance;
    [SerializeField] Sprite sprite;


    public override void PowerBuff(GameObject target)
    {
        target.GetComponent<Guy>().SetDefaultSpeed(speedBonus);
        Time.timeScale = timeChange;
    }

    public override void PowerDown(GameObject target)
    {
        target.GetComponent<Guy>().SetDefaultSpeed(-speedBonus);
        Time.timeScale = 1f;
    }

    public override int GetDropChance()
    {
        return dropChance;
    }

    public override Sprite GetItemSprite(){
        return sprite;
    }
}
