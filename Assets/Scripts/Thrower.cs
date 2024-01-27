using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thrower : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float throwForce = 100;
    [SerializeField] private Projector projector;
    

    private void Update()
    {
        projector.SimulateTrajectory(projectilePrefab, projectileSpawn.position, projectileSpawn.forward * throwForce);
    }
    
    private void OnShoot()
    {
        Debug.Log("Shoot");
        var projectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        projectile.Init(projectileSpawn.forward * throwForce);
    }
}
