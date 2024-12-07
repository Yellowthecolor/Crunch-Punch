using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Guy player;

    [SerializeField] Slider healthBar;

    void Start(){
        healthBar.maxValue = player.GetMaxHealth();
        healthBar.value = player.GetCurrentHealth();
    }
    public void SetHealthBar(){
        healthBar.maxValue = player.GetMaxHealth();
        healthBar.value = player.GetCurrentHealth();
    }

}
