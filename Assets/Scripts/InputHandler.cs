using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] Guy playerGuy;

    void FixedUpdate(){
        if(playerGuy.GetDeathStatus()){
            return;
        }

        if (Input.GetButton("Punch")){
            playerGuy.PunchAnimation();
        } 
        if (Input.GetButton("Kick")){
            playerGuy.KickAnimation();
        }
        if (Input.GetButton("Block")){
            playerGuy.SetBlockValue(true);
        } else {
            playerGuy.SetBlockValue(false);
        }
        playerGuy.BlockAnimation();

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        move = move.normalized;
        playerGuy.Move(move);            
    }
}
