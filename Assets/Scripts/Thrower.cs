using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thrower : MonoBehaviour
{
    public event Action OnStartThrow = delegate {};
    public event Action OnEndThrow = delegate {};    
    public event Action OnStartAim = delegate {};
    public event Action OnEndAim = delegate {};

    public event Action<float> OnUpdateThrowForce = delegate {}; // throwforce from 0 to 1 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float maxThrowForce = 100;
    [SerializeField] private float minThrowForce = 0;
    [SerializeField] private float throwForce = 100;
    [SerializeField] private float throwForceRampSpeed = 10;
    [SerializeField] private Projector projector;
    [SerializeField] private PlayerInventory playerInventory;
    
    [SerializeField] private AudioClip[] throwSounds;
    
    private SnackbarInput inputActionAsset;

    
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Shoot.Enable();
        inputActionAsset.Ingame.Aim.Enable();
        
        inputActionAsset.Ingame.Aim.started += ctx =>
        {
            if (playerInventory.HoldingFriedSnack || playerInventory.HoldingFrozenSnack)
            {
                OnStartAim();
            }
        };
        
        inputActionAsset.Ingame.Aim.canceled += ctx =>
        {
            OnEndAim();
        };
        
        inputActionAsset.Ingame.Shoot.canceled += OnShoot;
        inputActionAsset.Ingame.Shoot.started += ctx =>
        {
            if (playerInventory.HoldingFriedSnack || playerInventory.HoldingFrozenSnack)
            {
                OnStartThrow();
            }
        };
    }
    
    private void Start()
    {
        throwForce = minThrowForce;
        OnUpdateThrowForce(throwForce / maxThrowForce);
    }

    private void Update()
    {
        projectileSpawn.LookAt((Camera.main.transform.position + Camera.main.transform.forward.normalized * 5));
        
        var shootInput = inputActionAsset.Ingame.Shoot.ReadValue<float>();
        var aimInput = inputActionAsset.Ingame.Aim.ReadValue<float>();
        if (aimInput > 0  && (playerInventory.HoldingFriedSnack || playerInventory.HoldingFrozenSnack))
        {
            if (shootInput > 0)
            {
                throwForce += throwForceRampSpeed * Time.deltaTime;
                throwForce = Mathf.Clamp(throwForce, minThrowForce, maxThrowForce);
               
                OnUpdateThrowForce(throwForce / maxThrowForce);
            }
            
            if (true)
            {
                projector.SimulateTrajectory(projectilePrefab, projectileSpawn.position, projectileSpawn.forward * throwForce);
            }
        }
        else
        {
            projector.ClearTrajectory();
        }
    }
    
    private void OnShoot(InputAction.CallbackContext context)
    {
        // Debug.Log("asdad");
        if (!playerInventory.HoldingFriedSnack && !playerInventory.HoldingFrozenSnack)
        {
            return;
        }
        OnEndThrow();
        
        if (throwSounds.Length > 0) AudioManager.Instance.PlayClip(throwSounds[UnityEngine.Random.Range(0, throwSounds.Length)], AudioType.SFX, transform.position, 1f, true, 0.2f);
        
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
