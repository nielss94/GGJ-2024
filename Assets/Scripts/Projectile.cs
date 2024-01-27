using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hitSomething = false;
    
    [SerializeField] private Rigidbody rigidBody;

    public bool isFrozen;
    
    public SnackType snackType;
    public void Init(Vector3 velocity, SnackType snackType, bool isFrozen)
    {
        hitSomething = false;
        rigidBody.velocity = Vector3.zero;
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
    
    public void Init(Vector3 pos, Vector3 velocity, SnackType snackType, bool isFrozen)
    {
        transform.position = pos;
        Init(velocity, snackType, isFrozen);
    }

    public void SetGhost(bool isGhost)
    {
        if (isGhost)
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
        else
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.TryGetComponent(out Projectile projectile))
        {
            hitSomething = true;
        }
    }
    
}
