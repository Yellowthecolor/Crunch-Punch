using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationStateChanger : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] string state = "Idle";

    void Start()
    {
        ChangeAnimationState(state);   
    }

    public void ChangeAnimationState(string newState){
        animator.speed = 1;
        if (state == newState){
            return;
        }
        state = newState;
        animator.Play(state);
    }

    public bool CheckCurrentAnimationState(String stateToCheck){

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateToCheck)){
            return true;
        }
        return false;
    }

    public bool GetAnimationStatus(){
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f){
            return true;
        }
        return false;
    }

    public void TriggerAnimation(String trigger){
        animator.SetTrigger(trigger);
    }

    public void BooleanAnimation(String target, bool boolean){
        animator.SetBool(target, boolean);
    }

}
