using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndScreen : MonoBehaviour
{
    [SerializeField] private LevelProgressionManager levelProgressionManager;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image[] stars;
    [SerializeField] private string mainMenuSceneName = "Menu";

    private void Start()
    {
        levelProgressionManager.OnLevelEnded += OnLevelEnded;
    }

    private void OnLevelEnded()
    {
        mainPanel.SetActive(true);
        
        // TODO: stop game/input
        Cursor.lockState = CursorLockMode.None;
        
        var score = levelProgressionManager.CurrentScore;
        scoreText.text = $"Score: {score.ToString()}";

        SetStars(score);
    }

    private void SetStars(int score)
    {
        foreach (var star in stars)
        {
            star.enabled = false;
        }
        
        stars[0].enabled = true;
        if (score >= levelProgressionManager.Tier2ScoreThreshold)
        {
            stars[1].enabled = true;
        }

        if (score >= levelProgressionManager.Tier3ScoreThreshold)
        {
            stars[2].enabled = true;
        }
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnNextLevelClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
