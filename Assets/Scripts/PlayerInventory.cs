using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public event Action OnHoldSnack = delegate {  };
    public event Action OnReleaseSnack = delegate {  };
    
    public FrozenSnack frozenSnack;
    public FriedSnack friedSnack;
    public bool HoldingFriedSnack => friedSnack != null;
    public bool HoldingFrozenSnack => frozenSnack != null;
    
    public Transform frozenSnackHolder;
    public Transform friedSnackHolder;
    
    [SerializeField] private MeshRenderer mandRenderer;
    
    public bool Available => frozenSnack == null && friedSnack == null;
    
    [SerializeField] private FrozenSnack frozenSnackPrefab;
    [SerializeField] private FriedSnack friedSnackPrefab;
    public void TakeFrozenSnack(SnackType snackType)
    {
        frozenSnack = Instantiate(frozenSnackPrefab, frozenSnackHolder.position, frozenSnackHolder.rotation, frozenSnackHolder);
        frozenSnack.Init(snackType);
        OnHoldSnack();
    }
    
    public void TakeFriedSnack(SnackType snackType)
    {
        friedSnack = Instantiate(friedSnackPrefab, friedSnackHolder.position, friedSnackHolder.rotation, friedSnackHolder);
        friedSnack.Init(snackType);
        mandRenderer.enabled = true;
        OnHoldSnack();
    }
    
    public void DropFrozenSnack()
    {
        Destroy(frozenSnack.gameObject);
        frozenSnack = null;
        OnReleaseSnack();
    }
    
    public void DropFriedSnack()
    {
        Destroy(friedSnack.gameObject);
        friedSnack = null;
        mandRenderer.enabled = false;
        OnReleaseSnack();
    }
}
