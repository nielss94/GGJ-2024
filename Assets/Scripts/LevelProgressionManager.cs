using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgressionManager : MonoBehaviour
{
    [SerializeField] private int totalCustomers = 10;
    [SerializeField] private float levelDurationMinutes = 3f;
    [SerializeField] private int customerHitScore = 10;
    [SerializeField] private int customerMissScore = -5;

    [SerializeField] private CustomerManager customerManager;
    
    private int currentScore = 0;

    private void Start()
    {
        var customersPerSecondMin = totalCustomers / (levelDurationMinutes * 60 * 0.8f);
        var customersPerSecondMax = totalCustomers / (levelDurationMinutes * 60);
        
        customerManager.Init(customersPerSecondMin, customersPerSecondMax, this);
        
        StartCoroutine(WaitAndEndLevel());
    }
    
    public void OnCustomerHit()
    {
        currentScore += customerHitScore;
    }
    
    public void OnCustomerMiss()
    {
        currentScore += customerMissScore;
    }
    
    private IEnumerator WaitAndEndLevel()
    {
        yield return new WaitForSeconds(levelDurationMinutes * 60);
        // EndLevel();
    }
    
}
