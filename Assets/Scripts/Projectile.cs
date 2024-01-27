using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    
    public void Init(Vector3 velocity)
    {
        rigidBody.AddForce(velocity, ForceMode.Impulse);
    }
    
    
}
