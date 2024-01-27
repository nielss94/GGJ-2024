using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private float yRotation = 0f; 
    private float xRotation = 0f;
    
    private float rotationSpeed = 1f; 
    [SerializeField] private Transform cameraTarget;
    
    [SerializeField] private PlayerTurn playerTurn;
    public SnackbarInput inputActionAsset;
    
    [Header("Look settings")]
    [SerializeField] private float lookMinXRotation = -90f;
    [SerializeField] private float lookMaxXRotation = 90f;
    
    [Header("Kitchen look settings")]
    [SerializeField] private float kitchenLookMinYRotation = 90f;
    [SerializeField] private float kitchenLookMaxYRotation = 270f;
    
    [Header("Snackbar look settings")]
    [SerializeField] private float snackbarLookMinYRotation = -90f;
    [SerializeField] private float snackbarLookMaxYRotation = 90f;

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
        rotationSpeed = PlayerPrefs.GetFloat("Sensitivity", 10);
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
                return new Tuple<float, float>(Mathf.Clamp(yRotation, snackbarLookMinYRotation, snackbarLookMaxYRotation), Mathf.Clamp(xRotation, lookMinXRotation, lookMaxXRotation));
            case Orientation.Kitchen:
                return new Tuple<float, float>(Mathf.Clamp(yRotation, kitchenLookMinYRotation, kitchenLookMaxYRotation), Mathf.Clamp(xRotation, lookMinXRotation, lookMaxXRotation));
            default:
                return new Tuple<float, float>(yRotation, xRotation);
        }
    }
}
