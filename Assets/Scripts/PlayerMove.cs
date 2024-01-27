using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private SnackbarInput inputActionAsset;
    
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Move.Enable();
    }
    
    private void Update()
    {
        Vector2 moveInput = inputActionAsset.Ingame.Move.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }
}
