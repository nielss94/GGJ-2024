using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody rb;
    private SnackbarInput inputActionAsset;
    public Vector2 moveInput;
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Move.Enable();
    }
    
    private void Update()
    {
        moveInput = inputActionAsset.Ingame.Move.ReadValue<Vector2>();
        // Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        // transform.Translate(moveDirection * (moveSpeed * Time.deltaTime));
    }
    
    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        // move relative to this transform's rotation
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * (moveSpeed * Time.fixedDeltaTime));
    }
}
