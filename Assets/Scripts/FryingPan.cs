using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPan : MonoBehaviour
{
    public GameObject currentSnack;
    [SerializeField] private Transform snackHolder;
    
    public SnackType currentSnackType;
    private void OnCollisionEnter(Collision other)
    {
        if (currentSnack == null && other.gameObject.TryGetComponent(out Projectile projectile) && projectile.isFrozen)
        {
            
            if (projectile.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;
            }
            if (projectile.TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }
            
            currentSnackType = projectile.snackType;
            projectile.transform.SetParent(snackHolder);
            projectile.transform.position = snackHolder.position;
            currentSnack = projectile.gameObject;
            
        }
    }

    private void Update()
    {
        // moved currentSnack with sin
        if (currentSnack != null)
        {
            currentSnack.transform.position = snackHolder.position + Vector3.up * Mathf.Sin(Time.time * 5f) * 0.1f;
        }
    }

    public void TakeSnack()
    {
        Destroy(currentSnack);
        currentSnack = null;;
    }
}
