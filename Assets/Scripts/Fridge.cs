using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public SnackType snackType;
    
    public GameObject currentSnack;
    [SerializeField] private Transform snackHolder;

    public GameObject drinkPrefab;
    
    private void Start()
    {
        SpawnSnack();
    }

    public void SpawnSnack()
    {
        currentSnack = Instantiate(drinkPrefab, snackHolder.position, Quaternion.identity, snackHolder);
    }
}
