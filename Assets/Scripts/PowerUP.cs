using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUP : ScriptableObject
{
    public abstract void PowerUp(GameObject target);
    
    public abstract void PowerDown(GameObject target);
}
