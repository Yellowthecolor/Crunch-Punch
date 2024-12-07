using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : ScriptableObject
{
    public abstract void PowerBuff(GameObject target);
    
    public abstract void PowerDown(GameObject target);

    public abstract int GetDropChance();

    public abstract Sprite GetItemSprite();
}
