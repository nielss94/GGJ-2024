using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private float yRotation = 0f; 
    private float xRotation = 0f;
    
    
    [SerializeField] private float rotationSpeed = 1f; 
    [SerializeField] private Transform cameraTarget;
    
    public SnackbarInput inputActionAsset;
    

    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Look.Enable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 input = inputActionAsset.Ingame.Look.ReadValue<Vector2>();
        
        Debug.Log(input);
        // Increment the yRotation with input scaled by rotation speed
        yRotation += input.x * rotationSpeed;

        // Clamp the rotation
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        // Apply the rotation
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        
        xRotation += -input.y * rotationSpeed;
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cameraTarget.rotation = Quaternion.Euler(xRotation, cameraTarget.eulerAngles.y, cameraTarget.eulerAngles.z);
    }
}
