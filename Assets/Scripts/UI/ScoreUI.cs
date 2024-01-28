using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private LevelProgressionManager levelProgressionManager;

    [SerializeField] private TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        levelProgressionManager.OnScoreChanged += OnScoreChanged;
        
    }
    
    private void OnScoreChanged(int score)
    {
        scoreText.text = $"Score: {score.ToString()}";
    }
}
