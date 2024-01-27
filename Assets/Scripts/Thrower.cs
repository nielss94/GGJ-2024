using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
