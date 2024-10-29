using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Guy : MonoBehaviour
{
    Rigidbody2D rigidBody;


    [Header("Stats")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;
    [SerializeField] float healthRegen;

    bool isDead = false;
    [SerializeField] float speed;
    [SerializeField] float defaultSpeed = 50;
    [SerializeField] float punchDamage = 10;
    [SerializeField] float kickDamage = 20;
    [SerializeField] float punchRange = .15f;
    [SerializeField] float kickRange = .2f;
    Vector3 movement = Vector3.zero;

    [Header("Attack Points")]
    [SerializeField] Transform punchPoint;
    [SerializeField] Transform kickPoint;

    [Header("Helpers")]
    GameObject guy;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationStateChanger animationStateChanger;
    [SerializeField] LayerMask opponentLayer;

    [Header("Animation States")]
    [SerializeField] String walk = "Walk";
    [SerializeField] String idle = "Idle";
    [SerializeField] String punch = "Punch";
    [SerializeField] String kick = "Kick";
    String currentAnimationState = "Idle";


    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start(){
        CityManager.singleton.RegisterGuy(this);
        speed = defaultSpeed;
        currentHealth = maxHealth;
    }

    void FixedUpdate(){
        rigidBody.velocity = movement * speed * Time.fixedDeltaTime;

        if (rigidBody.velocity.x == 0){
            //Do nothing
        } else if (rigidBody.velocity.x > 0) {
            // spriteRenderer.flipX = false;
            transform.localScale = new Vector3(1,1,1);
        } else {
            // spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1,1,1);
        }
            

    }

    public void Move(Vector3 newMove){

        if (animationStateChanger.CheckCurrentAnimationState(punch) || animationStateChanger.CheckCurrentAnimationState(kick)){
            return;
        }
        movement = newMove;
        if (movement != Vector3.zero){
            speed = defaultSpeed;
            currentAnimationState = walk;
            animationStateChanger.TriggerAnimation(walk);
        } else {
            currentAnimationState = idle;
            animationStateChanger.TriggerAnimation(idle);
        }
    }

    public void MoveToward(Vector3 goalPos){
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized);
    }

    public void PassiveRegen(){
        healthRegen = (maxHealth - currentHealth)/10;
        currentHealth += healthRegen;
    }

    public void PunchAnimation(){
        if (currentAnimationState == punch || currentAnimationState == kick){
            return;
        }

        currentAnimationState = punch;
        animationStateChanger.TriggerAnimation(punch);
        speed = 1;
    }

    public void KickAnimation(){
        if (currentAnimationState == punch || currentAnimationState == kick){
            return;
        }

        currentAnimationState=kick;
        animationStateChanger.TriggerAnimation(kick);
        speed = 1;
    }

    public void Punch(){
        Collider2D[] hitOpponent = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, opponentLayer);
        foreach(Collider2D opponent in hitOpponent){
            opponent.GetComponent<Guy>().TakeDamage(punchDamage);
        }
    }

    public void Kick(){
        Collider2D[] hitOpponent = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, opponentLayer);
        foreach(Collider2D opponent in hitOpponent){
            opponent.GetComponent<Guy>().TakeDamage(kickDamage);
        }
    }

    public void TakeDamage(float damage){
        currentHealth -= damage;

        if (currentHealth > 0){
        } else {
            isDead = true;
            if (tag == "Player"){
                CityManager.singleton.GameOver();
            } else {
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        CityManager.singleton.RemoveGuy(this);
    }

    public bool GetDeathStatus(){
        return isDead;
    }
    public void SetDeathStatus(bool status){
        isDead = status;
    }

    public Vector3 GetPunchPosition(){
        return punchPoint.position;
    }
    public float GetPunchRange(){
        return punchRange;
    }
    public Vector3 GetKickPosition(){
        return kickPoint.position;
    }
    public float GetKickRange(){
        return kickRange;
    }




    void OnDrawGizmosSelected()
    {
        if(kickPoint == null) return;
        if(punchPoint == null) return;

        Gizmos.DrawWireSphere(kickPoint.position, kickRange);
        Gizmos.DrawWireSphere(punchPoint.position, punchRange);
    }


}
