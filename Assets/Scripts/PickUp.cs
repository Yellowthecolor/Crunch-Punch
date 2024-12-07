using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PowerUp powerUp;

    [SerializeField] float duration;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(PowerDuration(other.gameObject));
        }
    }


    IEnumerator PowerDuration(GameObject guy){
        powerUp.PowerBuff(guy.gameObject);
        yield return new WaitForSeconds(duration);
        powerUp.PowerDown(guy.gameObject);
        Destroy(gameObject);
    }

    public void SetPowerUp(PowerUp newPowerUp){
        powerUp = newPowerUp;
    }
}
