using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFace : MonoBehaviour
{
    private Customer customer;
    private CustomerManager customerManager;

    private void Start()
    {
        customer = GetComponentInParent<Customer>();
        customerManager = FindFirstObjectByType<CustomerManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (customer.angry) return;
        
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.isFrozen) return;
            
            customer.gameObject.GetComponent<CustomerOrder>().OnSnackHit(projectile.snackType);
            customerManager.Haha();
            
            Destroy(projectile.gameObject);
        }
    }
}
