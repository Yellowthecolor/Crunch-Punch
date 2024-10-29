using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] string currentStateString;

    [SerializeField] Guy guy;
    [SerializeField] Guy opponent;
    [SerializeField] LayerMask opponentLayer;


    delegate void AIState();
    AIState currentState;

    float stateTime = 0;
    bool changeState = false;


    void Start(){
        ChangeState(IdleState);
    }

    void ChangeState(AIState newState){
        currentState = newState;
        changeState = true;
    }

    void IdleState(){
        if(stateTime == 0){
            currentStateString = "IdleState";
        }
        if (FindTarget()){
            ChangeState(AttackState);
            return;
        }

    }

    bool FindTarget(){
        List<Guy> targets = CityManager.singleton.GetGuys();
        Debug.Log(targets);
        foreach(Guy currentGuy in targets){
            if(currentGuy.tag != "Player"){
                continue;
            }
            if(opponent == null){
                opponent = currentGuy;
                return true;
            }
        }
        return false;
    }

    void AttackState(){
        if (Vector3.Distance(guy.GetPunchPosition(), opponent.transform.position) <= guy.GetPunchRange()){
            guy.Move(Vector3.zero);
            guy.PunchAnimation();
        } else {
            guy.MoveToward(opponent.transform.position);
        }
    }

    void AITick(){
        if(changeState){
            stateTime = 0;
            changeState = false;
        }
        currentState();
        stateTime += Time.deltaTime;
    }

    void Update(){
        AITick();
    }

}