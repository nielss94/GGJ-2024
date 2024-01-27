using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playButton;
    [SerializeField] private SettingsMenu optionsMenu;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
    }
    
    public void OnPlay()
    {
        SceneManager.LoadScene("Niels");
    }

    public void OnOptions()
    {
        optionsMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

}
