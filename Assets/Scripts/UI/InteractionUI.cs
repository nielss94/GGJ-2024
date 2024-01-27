using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private TextMeshProUGUI interactionText;
    private SnackbarInput inputActionAsset;
    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        playerInteraction.OnLookAtInteraction += OnLookAtInteraction;
    }
    
    private void OnLookAtInteraction(GameObject gameObject)
    {
        if (gameObject == null)
        {
            interactionText.text = "";
            return;
        }
        
        if (gameObject.TryGetComponent(out SnackStack snackStack))
        {
            interactionText.text = $"Press {inputActionAsset.Ingame.Interact.name} to take frozen {snackStack.SnackType}";
        }
        else if (gameObject.TryGetComponent(out FryingPan fryingPan))
        {
            interactionText.text = $"Press {inputActionAsset.Ingame.Interact.name} to take fried {fryingPan.currentSnackType}";
        }
        else
        {
            interactionText.text = "";
        }
    }
}
