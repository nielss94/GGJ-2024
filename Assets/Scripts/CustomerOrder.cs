using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public SnackType snackType;
    [SerializeField] private Canvas canvas;
    
    Camera mainCamera;
    private void Awake()
    {
        Array values = Enum.GetValues(typeof(SnackType));
        snackType = (SnackType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
    }

    private void Update()
    {
        canvas.transform.LookAt(mainCamera.transform);
    }


    private void OnCollisionEnter(Collision other)
    {
        
    }
}
