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
    
    [SerializeField] private PlayerTurn playerTurn;
    public SnackbarInput inputActionAsset;
    

    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Look.Enable();

        playerTurn.OnDoTurn += () =>
        {
            if (playerTurn.Orientation == Orientation.Snackbar)
            {
                yRotation = 0f;
            }
            else
            {
                yRotation = 180f;
            }
        };
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (playerTurn.rotating) return;
        
        Vector2 input = inputActionAsset.Ingame.Look.ReadValue<Vector2>();
        
        // Increment the yRotation with input scaled by rotation speed
        yRotation += input.x * rotationSpeed;
        xRotation += -input.y * rotationSpeed;

        // Clamp the rotation
        var clampedRotations = ClampRotationsByOrientation(playerTurn.Orientation, yRotation, xRotation);
        yRotation = clampedRotations.Item1;
        xRotation = clampedRotations.Item2;

        // Apply the rotation
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        cameraTarget.rotation = Quaternion.Euler(xRotation, cameraTarget.eulerAngles.y, cameraTarget.eulerAngles.z);
    }

    Tuple<float, float> ClampRotationsByOrientation(Orientation orientation, float yRotation, float xRotation)
    {
        switch (orientation)
        {
            case Orientation.Snackbar:
                return new Tuple<float, float>(Mathf.Clamp(yRotation, -90f, 90f), Mathf.Clamp(xRotation, -90f, 90f));
            case Orientation.Kitchen:
                return new Tuple<float, float>(Mathf.Clamp(yRotation, 90f, 270f), Mathf.Clamp(xRotation, -90f, 90f));
            default:
                return new Tuple<float, float>(yRotation, xRotation);
        }
    }
}
