using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityManager : MonoBehaviour
{

    public static CityManager singleton;

    [SerializeField] List<Guy> guys;
    [SerializeField] Guy playerGuy;
    [SerializeField] float timeLimit;
    [SerializeField] bool isGameOver = false;
    float currentTime = 0;

    [Header("Helpers")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPanel;

    void Awake(){
        isGameOver = false;
        gameOverPanel.SetActive(false);
        guys = new List<Guy>();
        if (singleton == null){
            singleton = this;
        } else {
            Destroy(this.gameObject);
        }
        RegisterGuy(playerGuy);
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

    public Guy GetPlayerGuy(){
        return playerGuy;
    }

    public void DestroyAllGuys(){
        foreach(Guy guy in guys){
            Destroy(guy.gameObject);
        }
    }

    
    IEnumerator GlobalTimerRoutine(){
        while(currentTime < timeLimit || playerGuy.GetDeathStatus()){
            yield return new WaitForSeconds(1);
            currentTime++;
            playerGuy.PassiveRegen();
            timerText.text = currentTime.ToString();
        }
        playerGuy.SetDeathStatus(true);
        StopAllCoroutines();
        GameOver();
        yield return null;
    }

    public void GameOver(){
        if (currentTime >= timeLimit){
            //this doessnt work for sosme reason
            // gameOverPanel.GetComponent<TextMeshProUGUI>().text = "You Survived!";
        } 
        isGameOver=true;
        gameOverPanel.SetActive(true);

    }

    public void MainMenu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame(){
        DestroyAllGuys();
        SceneManager.LoadScene("CityScene");
    }

    public bool GetIsGameOver(){
        return isGameOver;
    }

}
