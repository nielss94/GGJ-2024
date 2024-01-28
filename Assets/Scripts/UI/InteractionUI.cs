using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private Image interactionImage;
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
            interactionImage.enabled = false;
            interactionText.text = "";
            return;
        }
        
        if (gameObject.TryGetComponent(out SnackStack snackStack))
        {
            interactionImage.enabled = true;
            interactionText.text = $"{snackStack.SnackType}";
        }
        else if (gameObject.TryGetComponent(out FryingPan fryingPan))
        {
            if (fryingPan.snackReady)
            {
                interactionImage.enabled = true;
                interactionText.text = $"{fryingPan.currentSnackType}";
            }
            else
            {
                interactionImage.enabled = false;
                interactionText.text = "";
            }
        }
        else if (gameObject.TryGetComponent(out Fridge fridge))
        {
            interactionImage.enabled = true;
            interactionText.text = $"{fridge.snackType}";
        }
        else
        {
            interactionImage.enabled = false;
            interactionText.text = "";
        }
    }
}
