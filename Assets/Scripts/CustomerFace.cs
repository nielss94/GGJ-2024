using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerFace : MonoBehaviour
{
    private Customer customer;
    private CustomerManager customerManager;
    
    [SerializeField] private ParticleSystem particlePrefab;
    
    [SerializeField] private AudioClip[] hitClips;

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

            if (particlePrefab)
            {
                var particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                Destroy(particle, 2f);
            }
            
            if (hitClips.Length > 0) AudioManager.Instance.PlayClip(hitClips[Random.Range(0, hitClips.Length)], AudioType.SFX, transform.position, 1f, true, 0.2f);

            
            Destroy(projectile.gameObject);
        }
    }
}
