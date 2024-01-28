using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private LevelProgressionManager levelProgressionManager;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button nextLevelButton;    
    [SerializeField] private Button mainMenuButton;    
    [SerializeField] private Image[] stars;
    [SerializeField] private string mainMenuSceneName = "Menu";
    [SerializeField] private bool hasNextLevel = true;

    [SerializeField] private Sprite starFilled;
    [SerializeField] private Sprite starEmpty;
    
    private SnackbarInput inputActionAsset;

    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
    }

    private void Start()
    {
        levelProgressionManager.OnLevelEnded += OnLevelEnded;
        nextLevelButton.gameObject.SetActive(hasNextLevel);
    }

    private void OnLevelEnded()
    {
        mainPanel.SetActive(true);

        if (hasNextLevel)
        {
            EventSystem.current.SetSelectedGameObject(nextLevelButton.gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(mainMenuButton.gameObject);
        }
        
        inputActionAsset.Ingame.Disable();
        inputActionAsset.Ingame.Look.Disable();
        inputActionAsset.Ingame.Shoot.Disable();
        inputActionAsset.Ingame.Interact.Disable();
        
        Time.timeScale = 0;
        
        Cursor.lockState = CursorLockMode.None;
        
        var score = levelProgressionManager.CurrentScore;
        scoreText.text = $"Score: {score.ToString()}";

        SetStars(score);
    }

    private void SetStars(int score)
    {
        stars[0].sprite = starFilled;
        if (score >= levelProgressionManager.Tier2ScoreThreshold)
        {
            stars[1].sprite = starFilled;
        }
        else
        {
            stars[1].sprite = starEmpty;
        }

        if (score >= levelProgressionManager.Tier3ScoreThreshold)
        {
            stars[2].sprite = starFilled;
        }
        else
        {
            stars[2].sprite = starEmpty;
        }
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnNextLevelClicked()
    {
        if (!hasNextLevel)
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
