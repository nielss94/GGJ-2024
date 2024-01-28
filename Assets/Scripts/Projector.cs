using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Projector : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform obstaclesParent;
    [SerializeField] private int maxSimulationIteration;
    
    
    private Scene simulationScene;
    private PhysicsScene phyicsScene;
    private Projectile ghostProjectile;

    private void Start()
    {
        CreatePhysicsScene();
    }

    public void CreatePhysicsScene()
    {
        simulationScene = SceneManager.CreateScene("SimulationScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        phyicsScene = simulationScene.GetPhysicsScene();

        foreach (Transform obj in obstaclesParent)
        {
            if (!obj.TryGetComponent(out MeshRenderer meshRenderer))
            {
                continue;
            }
            
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            if (ghostObj.TryGetComponent(out Renderer renderer))
            {
                renderer.enabled = false;
                var renderers = renderer.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    r.enabled = false;
                }
            }
            
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
        }
    }

    public void SimulateTrajectory(Projectile projectile, Vector3 pos, Vector3 velocity)
    {
        if (ghostProjectile == null)
        {
            ghostProjectile = Instantiate(projectile, pos, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(ghostProjectile.gameObject, simulationScene);
            ghostProjectile.SetGhost(true);
        }

        ghostProjectile.Init(pos, velocity, projectile.snackType, projectile.isFrozen);

        line.positionCount = maxSimulationIteration;

        for (int i = 0; i < maxSimulationIteration; i++)
        {
            
            phyicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostProjectile.transform.position);
            
            if (ghostProjectile.hitSomething)
            {
                line.positionCount = i + 1;
                break;
            }
        }
        
        // Destroy(ghostObj.gameObject);
    }
    
    public void ClearTrajectory()
    {
        line.positionCount = 0;
    }

}
