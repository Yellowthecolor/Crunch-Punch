using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] Guy playerGuy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
         
    }

    void FixedUpdate(){

        if (Input.GetButton("Punch")){
            playerGuy.PunchAnimation();
        } 
        if (Input.GetButton("Kick")){
            playerGuy.KickAnimation();
        }

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        move = move.normalized;
        playerGuy.Move(move);            
    }
}
