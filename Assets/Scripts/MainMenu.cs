using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject settingsPanel;

    [SerializeField] bool settingPanelToggle = false;

    [SerializeField] GameObject musicPlayer;
    public void StartGame(){
        DontDestroyOnLoad(musicPlayer);
        SceneManager.LoadScene("CityScene");
    }

    public void QuitGame(){
        Application.Quit();
    }


    void Update()
    {
        if (settingPanelToggle){
            settingsPanel.SetActive(true);
        } else{
            settingsPanel.SetActive(false);
        }
    }

    public void ToggleSettingsPanel(){
        settingPanelToggle = !settingPanelToggle;
    }
}
