using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


    public void SetMasterVolume(){
        masterMixer.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume(){
        masterMixer.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume(){
        masterMixer.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void ToggleFullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }

}
