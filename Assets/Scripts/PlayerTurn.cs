using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public enum Orientation
{
    Snackbar,
    Kitchen
}

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    [SerializeField] private float turnSpeedInSeconds = .5f;

    [SerializeField]
    private Orientation orientation = Orientation.Snackbar;

    [SerializeField] private bool canRotateDuringTurn = false;
    
    private bool rotating = false;
    private void Start()
    {
        SetOrientation(orientation);
    }

    private void OnTurn()
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
                rigidbody.DORotate(new Vector3(0, 0, 0), turnSpeedInSeconds).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    rotating = false;
                });
                break;
            case Orientation.Kitchen:
                rigidbody.DORotate(new Vector3(0, 180, 0), turnSpeedInSeconds).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    rotating = false;
                });
                break;
        }
        
        rotating = true;
    }
}
