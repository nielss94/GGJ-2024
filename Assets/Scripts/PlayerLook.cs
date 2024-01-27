using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private float yRotation = 0f; 
    private float xRotation = 0f;
    
    private float rotationSpeed = 1f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Thrower thrower;
    [SerializeField] private Transform cameraTarget;
    
    public SnackbarInput inputActionAsset;
    
    [Header("Camera Distance")]
    [SerializeField] private float defaultCameraDistance = 3f;
    [SerializeField] private float aimCameraDistance = 2.5f;
    [SerializeField] private float shootCameraDistance = 2.5f;
    [SerializeField] private float cameraDistanceChangeSpeed = .5f;
    
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Look.Enable();

        thrower.OnStartAim += () =>
        {
            DOTween.To(() => 
                virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance,
                x => virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = x,
                aimCameraDistance,
                cameraDistanceChangeSpeed);
            
        };
        
        thrower.OnEndAim += () =>
        {
            DOTween.To(() => 
                    virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance,
                x => virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = x,
                defaultCameraDistance,
                cameraDistanceChangeSpeed);
        };
        
        thrower.OnStartThrow += () =>
        {
            DOTween.To(() => 
                    virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance,
                x => virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = x,
                shootCameraDistance,
                cameraDistanceChangeSpeed);
        };
        
        thrower.OnEndThrow += () =>
        {
            DOTween.To(() => 
                    virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance,
                x => virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = x,
                defaultCameraDistance,
                cameraDistanceChangeSpeed);
        };
    }

    private void Start()
    {
        rotationSpeed = PlayerPrefs.GetFloat("Sensitivity", 10);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 input = inputActionAsset.Ingame.Look.ReadValue<Vector2>();
        
        // Increment the yRotation with input scaled by rotation speed
        yRotation += input.x * rotationSpeed;
        xRotation += -input.y * rotationSpeed;

        // Apply the rotation
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        cameraTarget.rotation = Quaternion.Euler(xRotation, cameraTarget.eulerAngles.y, cameraTarget.eulerAngles.z);
    }

}
