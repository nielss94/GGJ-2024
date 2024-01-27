using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<GameObject> OnLookAtInteraction = delegate {  };
    [SerializeField] private LayerMask interactionLayers;
    
    [SerializeField] private PlayerInventory playerInventory;
    private SnackbarInput inputActionAsset;
    
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Interact.Enable();
    }
    
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, interactionLayers))
        {
            if (hit.transform.TryGetComponent(out SnackStack snackStack))
            {
                if (playerInventory.Available)
                {
                    // Debug.Log("Snack Stack: " + snackStack.SnackType);
                    if (inputActionAsset.Ingame.Interact.triggered)
                    {
                        // Debug.Log("Interact with Snack Stack " + snackStack.SnackType);
                        playerInventory.TakeFrozenSnack(snackStack.SnackType);
                    }
                    
                    OnLookAtInteraction(snackStack.gameObject);
                }
                else
                {
                    OnLookAtInteraction(null);
                }
                
                
            }
            else if (hit.transform.TryGetComponent(out FryingPan fryingPan) && fryingPan.currentSnack != null)
            {
                if (playerInventory.Available)
                {
                    Debug.Log("Fying pan: " + fryingPan.currentSnackType);
                    if (inputActionAsset.Ingame.Interact.triggered && fryingPan.snackReady)
                    {
                        Debug.Log("Take " + fryingPan.currentSnackType);
                        playerInventory.TakeFriedSnack(fryingPan.currentSnackType);
                        fryingPan.TakeSnack();
                    }
                    
                    OnLookAtInteraction(fryingPan.gameObject);
                }
                else
                {
                    OnLookAtInteraction(null);
                }
            }
            else
            {
                OnLookAtInteraction(null);
            }
        }
        else
        {
            OnLookAtInteraction(null);
        }
    }
}
