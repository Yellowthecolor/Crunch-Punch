using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityManager : MonoBehaviour
{

    public static CityManager singleton;

    [SerializeField] List<Guy> guys;
    [SerializeField] float timeLimit;
    float currentTime = 0;

    [Header("Helpers")]
    [SerializeField] TextMeshProUGUI timerText;
    

    void Awake(){
        guys = new List<Guy>();
        if (singleton == null){
            singleton = this;
        } else {
            Destroy(this.gameObject);
        }
        StartCoroutine(GlobalTimerRoutine());
    }

    public void RegisterGuy(Guy guy){
        guys.Add(guy);
    }

    public void RemoveGuy(Guy guy){
        guys.Remove(guy);
    }

    public List<Guy> GetGuys(){
        return guys;
    }

    
    IEnumerator GlobalTimerRoutine(){
        while(currentTime < timeLimit){
            yield return new WaitForSeconds(1);
            currentTime++;

            timerText.text = currentTime.ToString();
        }
        StopAllCoroutines();
        yield return null;

    }

    void RestartGame(){
        SceneManager.LoadScene("CityScene");
    }
}
