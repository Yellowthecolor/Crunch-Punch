using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Guy : MonoBehaviour
{
    Rigidbody2D rigidBody;


    [Header("Stats")]
    [SerializeField] float maxHealth = 100;
    [SerializeField] float currentHealth = 100;
    [SerializeField] float healthRegen;
    [SerializeField] float healthRegenBonus = 0;
    [SerializeField] float damageReduction = 1;

    bool isDead = false;
    [SerializeField] float speed;
    [SerializeField] float defaultSpeed = 50;
    [SerializeField] float punchDamage = 10;
    [SerializeField] float kickDamage = 20;
    [SerializeField] float damageMultiplier = 1;
    [SerializeField] float punchRange = .15f;
    [SerializeField] float kickRange = .2f;
    [SerializeField] float knockbackMultiplier = 10f;
    [SerializeField] bool canMove = true;
    Vector3 movement = Vector3.zero;

    [Header("Attack Points")]
    [SerializeField] Transform punchPoint;
    [SerializeField] Transform kickPoint;

    [Header("Helpers")]
    GameObject guy;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AnimationStateChanger animationStateChanger;
    [SerializeField] LayerMask opponentLayer;
    [SerializeField] bool blockButtonValue;
    [SerializeField] bool inDamageState;

    [Header("Animation States")]
    [SerializeField] String walk = "Walk";
    [SerializeField] String idle = "Idle";
    [SerializeField] String punch = "Punch";
    [SerializeField] String kick = "Kick";
    [SerializeField] String block = "Block";
    [SerializeField] String death = "Death";
    [SerializeField] String damaged = "Damage";
    [SerializeField] String dead = "Dead";
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
        if (canMove){
            if (tag == "Player"){
                rigidBody.velocity = movement * speed * Time.unscaledDeltaTime;

            } else {
                rigidBody.velocity = movement * speed * Time.fixedDeltaTime;
            }
        }

        if (rigidBody.velocity.x == 0){
            //Do nothing
        } else if (rigidBody.velocity.x > 0) {
            transform.localScale = new Vector3(1,1,1);
        } else {
            transform.localScale = new Vector3(-1,1,1);
        }
            

    }

    public void Move(Vector3 newMove){

        if (!canMove 
        || animationStateChanger.CheckCurrentAnimationState(punch) 
        || animationStateChanger.CheckCurrentAnimationState(kick) 
        || animationStateChanger.CheckCurrentAnimationState(damaged)
        || animationStateChanger.CheckCurrentAnimationState(block)){
            return;
        }

        movement = newMove;
        if (movement != Vector3.zero){
                ResetSpeed();
 
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
        healthRegen = (maxHealth - currentHealth)/10 + healthRegenBonus;
        currentHealth += healthRegen;
    }

    public void PunchAnimation(){
        if (currentAnimationState == punch || currentAnimationState == kick || currentAnimationState == damaged){
            return;
        }

        currentAnimationState = punch;
        animationStateChanger.TriggerAnimation(punch);
        speed = 1;
    }

    public void KickAnimation(){
        if (currentAnimationState == punch || currentAnimationState == kick || currentAnimationState == damaged){
            return;
        }

        currentAnimationState=kick;
        animationStateChanger.TriggerAnimation(kick);
        speed = 1;
    }

    public void BlockAnimation(){
        if (currentAnimationState == punch || currentAnimationState == kick || currentAnimationState == damaged){
            return;
        }
        if (blockButtonValue){
            currentAnimationState = block;
        } 
        if (!blockButtonValue && currentAnimationState == block){
            currentAnimationState = idle;
            animationStateChanger.TriggerAnimation(idle);
        }
        
        animationStateChanger.BooleanAnimation(block, blockButtonValue);
    }

    public void Punch(){
        Collider2D[] hitOpponent = Physics2D.OverlapCircleAll(punchPoint.position, punchRange, opponentLayer);
        foreach(Collider2D opponent in hitOpponent){
            opponent.GetComponent<Guy>().TakeDamage(punchDamage * damageMultiplier, this.transform);
        }
    }

    public void Kick(){
        Collider2D[] hitOpponent = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, opponentLayer);
        foreach(Collider2D opponent in hitOpponent){
            opponent.GetComponent<Guy>().TakeDamage(kickDamage * damageMultiplier, this.transform);
        }
    }

    public void DamageAnimation(){
        currentAnimationState=damaged;
        animationStateChanger.TriggerAnimation(damaged);
    }

    public void DeathAnimation(){
        currentAnimationState = death;
        animationStateChanger.TriggerAnimation(death);
    }

    public void DeadAnimation(){
        currentAnimationState = dead;
        animationStateChanger.TriggerAnimation(dead);
    }

    public void TakeDamage(float damage, Transform opponent){
        if (tag == "Player" && 
        (currentAnimationState == block 
        || currentAnimationState == damaged)){
            return;
        }
 
        DamageAnimation();
        currentHealth -= damage * damageReduction;

        StartCoroutine(Knockback(knockbackMultiplier, opponent));

        if (currentHealth > 0){
        } else {
            isDead = true;
            if (tag == "Player"){
                DeathAnimation();
                CityManager.singleton.GameOver();
                DeadAnimation();
            } else {
                DeathAnimation();
                DeadAnimation();
            }
        }

        movement = Vector3.zero;
    }

    void DestroyGuy(){
        Destroy(gameObject);
    }
    
    void OnDestroy()
    {
        CityManager.singleton.RemoveGuy(this);
    }

    public void ResetSpeed(){
        speed = defaultSpeed;
    }

    public bool GetDeathStatus(){
        return isDead;
    }
    public void SetDeathStatus(bool status){
        isDead = status;
    }

    public void SetBlockValue(bool value){
        blockButtonValue = value;
    }

    #region PowerUp Bonuses
    public void SetDamageMultiplier(float value){
        damageMultiplier += value;
    }

    public void SetDamageReduction(float value){
        damageReduction += value;
    }

    public void SetDefaultSpeed(float value){
        defaultSpeed += value;
    }

    public void SetHealthRegenBonus(float value){
        healthRegenBonus += value;
    }

    #endregion PowerUp Bonusess

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

    public float GetCurrentHealth(){
        return currentHealth;
    }

    public void SetInDamageStateBool(){
        if (GetCurrentHealth() > 0){
            inDamageState = !inDamageState;
        } else {
            inDamageState = true;
        }
    }


    public bool GetInDamageStateBool(){
        return inDamageState;
    }

    public IEnumerator Knockback(float pushBack, Transform opponent) { 
        while(inDamageState) {
            canMove = false;
            Vector3 direction = (opponent.transform.position - this.transform.position).normalized;
            rigidBody.AddForce(-direction.normalized * pushBack, ForceMode2D.Impulse);
            yield return new WaitForFixedUpdate();
        } 
        canMove = true;
    }
  
    void OnDrawGizmosSelected()
    {
        if(kickPoint == null) return;
        if(punchPoint == null) return;

        Gizmos.DrawWireSphere(kickPoint.position, kickRange);
        Gizmos.DrawWireSphere(punchPoint.position, punchRange);
    }
}
