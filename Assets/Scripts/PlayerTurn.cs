using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public enum Orientation
{
    Snackbar,
    Kitchen
}

public class PlayerTurn : MonoBehaviour
{
    public event Action OnDoTurn = delegate {  };
    [SerializeField] private float turnSpeedInSeconds = .5f;

    [SerializeField]
    private Orientation orientation = Orientation.Snackbar;

    [SerializeField] private bool canRotateDuringTurn = false;
    
    public bool rotating = false;
    
    public Orientation Orientation => orientation;
    
    public SnackbarInput inputActionAsset;

    private void Awake()
    {
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Turn.Enable();
        inputActionAsset.Ingame.Turn.performed += OnTurn;
    }

    private void Start()
    {
        SetOrientation(orientation);
    }

    private void OnTurn(InputAction.CallbackContext context)
    {
        if (rotating && !canRotateDuringTurn) return;
        
        SetOrientation(orientation == Orientation.Snackbar ? Orientation.Kitchen : Orientation.Snackbar);
    }

    private void SetOrientation(Orientation orientation)
    {
        this.orientation = orientation;

        switch (orientation)
        {
            case Orientation.Snackbar:
                transform.DORotate(new Vector3(0, 0, 0), turnSpeedInSeconds).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    rotating = false;
                });
                break;
            case Orientation.Kitchen:
                transform.DORotate(new Vector3(0, 180, 0), turnSpeedInSeconds).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    rotating = false;
                });
                break;
        }

        rotating = true;
        OnDoTurn();
    }
}
