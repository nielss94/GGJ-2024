using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private Slider sensitivitySlider;
    
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(sensitivitySlider.gameObject);
        
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 10);
        
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }
    
    public void OnMasterVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MasterVolume", value);
        AudioManager.Instance.master.audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
    }
    
    public void OnMusicVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        AudioManager.Instance.music.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }
    
    public void OnSFXVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        AudioManager.Instance.sfx.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }
    
    public void OnSensitivityChanged(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }
    
    public void OnBack()
    {
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
