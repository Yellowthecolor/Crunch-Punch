using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.U2D;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider playerSfxSlider;
    [SerializeField] Slider enemySfxSlider;
    [SerializeField] Slider pickUpSfxSlider;

    [SerializeField] TMP_Dropdown resolutionDropdown;

    List<string> resolutions = new List<string>();
   

    void Start(){
        resolutionDropdown.ClearOptions();

        foreach(Resolution res in Screen.resolutions){
            string resString = res.width + " x " + res.height;
            resolutions.Add(resString);
        }

        resolutionDropdown.AddOptions(resolutions);
        
    }

    public void SetResolution (int resolutionIndex){
        Resolution chosenResolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, Screen.fullScreen);
    }

    public void SetMasterVolume(){
        masterMixer.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume(){
        masterMixer.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume(){
        masterMixer.SetFloat("PlayerSFXVolume", playerSfxSlider.value);
    }

    public void SetEnemySFXVolume(){
        masterMixer.SetFloat("EnemySFXVolume", enemySfxSlider.value);
    }

    public void SetPickUpSFXVolume(){
        masterMixer.SetFloat("PickUpSFXVolume", pickUpSfxSlider.value);
    }

    public void ToggleFullScreen(bool fullScreenToggle){
        Screen.fullScreen = fullScreenToggle;
    }

    public void ToggleVSync(bool vSyncToggle){
        if (vSyncToggle){
            QualitySettings.vSyncCount = 1;
        } else {
            QualitySettings.vSyncCount = 0;
        }
    }
    


}
