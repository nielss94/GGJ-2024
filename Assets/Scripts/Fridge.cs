using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public SnackType snackType;
    
    public GameObject currentSnack;
    [SerializeField] private Transform snackHolder;

    public List<FridgeOption> fridgeOptions = new List<FridgeOption>();
    
    private void Start()
    {
        SpawnSnack();
    }

    public void SpawnSnack()
    {
        var prefab = fridgeOptions.Find(x => x.snackType == snackType).prefab;
        currentSnack = Instantiate(prefab, snackHolder.position, Quaternion.identity, snackHolder);
    }

    
}

[System.Serializable]
public class FridgeOption
{
    public SnackType snackType;
    public GameObject prefab;
}