using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;

    public bool isFrozen;
    
    public SnackType snackType;
    public void Init(Vector3 velocity, SnackType snackType, bool isFrozen)
    {
        this.isFrozen = isFrozen;
        rigidBody.AddForce(velocity, ForceMode.Impulse);
        
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
