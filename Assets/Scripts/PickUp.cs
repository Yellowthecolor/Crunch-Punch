using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] PowerUP powerUP;
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
        powerUP.PowerUp(guy.gameObject);
        yield return new WaitForSeconds(duration);
        powerUP.PowerDown(guy.gameObject);
        Destroy(gameObject);
    }
}
