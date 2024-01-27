using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public event Action<GameObject> OnLookAtInteraction = delegate {  };
    [SerializeField] private LayerMask interactionLayers;
    
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
                Debug.Log("Snack Stack: " + snackStack.SnackType);
                if (inputActionAsset.Ingame.Interact.triggered)
                {
                    Debug.Log("Interact with Snack Stack " + snackStack.SnackType);
                }
            }
            OnLookAtInteraction(hit.transform.gameObject);
        }
        else
        {
            OnLookAtInteraction(null);
        }
    }
}
