using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thrower : MonoBehaviour
{
    public event Action<float> OnUpdateThrowForce = delegate {}; // throwforce from 0 to 1 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float maxThrowForce = 100;
    [SerializeField] private float minThrowForce = 0;
    [SerializeField] private float throwForce = 100;
    [SerializeField] private float throwForceRampSpeed = 10;
    [SerializeField] private Projector projector;
    [SerializeField] private PlayerInventory playerInventory;
    
    private SnackbarInput inputActionAsset;

    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Shoot.Enable();
        inputActionAsset.Ingame.Shoot.canceled += OnShoot;
    }
    
    private void Start()
    {
        throwForce = minThrowForce;
        OnUpdateThrowForce(throwForce / maxThrowForce);
    }

    private void Update()
    {
        var shootInput = inputActionAsset.Ingame.Shoot.ReadValue<float>();
        if (shootInput > 0 && (playerInventory.HoldingFriedSnack || playerInventory.HoldingFrozenSnack))
        {
            throwForce += throwForceRampSpeed * Time.deltaTime;
            throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
            projector.SimulateTrajectory(projectilePrefab, projectileSpawn.position, projectileSpawn.forward * throwForce);
            OnUpdateThrowForce(throwForce / maxThrowForce);
        }
        else
        {
            projector.ClearTrajectory();
        }
    }
    
    private void OnShoot(InputAction.CallbackContext context)
    {
        if (!playerInventory.HoldingFriedSnack && !playerInventory.HoldingFrozenSnack)
        {
            return;
        }
        
        var projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        var snackType = playerInventory.HoldingFriedSnack ? playerInventory.friedSnack.snackType : playerInventory.frozenSnack.snackType;
        projectile.Init(projectileSpawn.forward * throwForce, snackType, playerInventory.HoldingFrozenSnack);
        
        throwForce = minThrowForce;
        OnUpdateThrowForce(throwForce / maxThrowForce);
        
        if (playerInventory.HoldingFriedSnack)
        {
            playerInventory.DropFriedSnack();
        }
        else if (playerInventory.HoldingFrozenSnack)
        {
            playerInventory.DropFrozenSnack();
        }
    }
    
}
