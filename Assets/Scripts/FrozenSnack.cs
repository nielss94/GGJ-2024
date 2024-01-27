using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenSnack : MonoBehaviour
{
    public SnackType snackType;
    
    public void Init(SnackType snackType)
    {
        this.snackType = snackType;
        string snackName = snackType.ToString();
        // disable each child that isn't the snack we want
        foreach (Transform child in transform)
        {
            if (child.name != snackName)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
