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
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(sensitivitySlider.gameObject);
        
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 10);
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
