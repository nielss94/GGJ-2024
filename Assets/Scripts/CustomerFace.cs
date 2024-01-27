using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFace : MonoBehaviour
{
    private Customer customer;

    private void Start()
    {
        this.customer = GetComponentInParent<Customer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.isFrozen) return;
            
            customer.gameObject.GetComponent<CustomerOrder>().OnSnackHit(projectile.snackType);
            
            Destroy(projectile.gameObject);
        }
    }
}
