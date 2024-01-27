using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hitSomething = false;
    
    [SerializeField] private Rigidbody rigidBody;
    
    public void Init(Vector3 velocity)
    {
        hitSomething = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.AddForce(velocity, ForceMode.Impulse);
    }
    
    public void Init(Vector3 pos, Vector3 velocity)
    {
        transform.position = pos;
        Init(velocity);
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
            Debug.Log($"Hit {other.gameObject.name}");
            hitSomething = true;
        }
    }
    
}
