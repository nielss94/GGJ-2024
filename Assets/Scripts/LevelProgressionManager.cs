using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressionManager : MonoBehaviour
{
    public event Action OnLevelEnded = delegate {  };
    public event Action<int> OnScoreChanged = delegate {  };
    
    public int CurrentScore => currentScore;
    public int Tier2ScoreThreshold => (int) (totalCustomers * tier2ScorePercentage) * customerHitScore;
    public int Tier3ScoreThreshold => (int) (totalCustomers * tier3ScorePercentage) * customerHitScore;
    
    [SerializeField] private int totalCustomers = 10;
    [SerializeField] private float levelDurationMinutes = 3f;
    [SerializeField] private int customerHitScore = 10;
    [SerializeField] private int customerMissScore = -5;
    [SerializeField] [Range(0,1)] private float tier2ScorePercentage = 0.5f;
    [SerializeField] [Range(0,1)] private float tier3ScorePercentage = 0.8f;
    [SerializeField] private CustomerManager customerManager;
    
    private int currentScore = 0;

    private void Start()
    {
        var customersPerSecondMin = (levelDurationMinutes * 60 * 0.8f) / totalCustomers;
        var customersPerSecondMax = (levelDurationMinutes * 60) / totalCustomers;
        
        customerManager.Init(customersPerSecondMin, customersPerSecondMax, this);
        
        StartCoroutine(WaitAndEndLevel());
    }
    
    public void OnCustomerHit()
    {
        currentScore += customerHitScore;
        OnScoreChanged(currentScore);
    }
    
    public void OnCustomerMiss()
    {
        currentScore += customerMissScore;
        OnScoreChanged(currentScore);
    }
    
    private IEnumerator WaitAndEndLevel()
    {
        Debug.Log("starting wait...");
        yield return new WaitForSeconds(levelDurationMinutes * 60);
        EndLevel();
    }
    
    private void EndLevel()
    {
        Debug.Log("Level ended with score: " + currentScore);
        OnLevelEnded();
    }
    
}
