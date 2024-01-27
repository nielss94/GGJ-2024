using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interactionLayers;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, interactionLayers))
        {
            if (hit.transform.TryGetComponent(out Snack snack))
            {
                Debug.Log("Snack: " + snack.SnackType);
            }
            
        }
    }
}
