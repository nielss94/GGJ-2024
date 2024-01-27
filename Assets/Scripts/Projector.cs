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
            var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
        }
    }

    public void SimulateTrajectory(Projectile projectile, Vector3 pos, Vector3 velocity)
    {
        var ghostObj = Instantiate(projectile, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj.gameObject, simulationScene);
        
        ghostObj.Init(velocity);
        
        line.positionCount = maxSimulationIteration;

        for (int i = 0; i < maxSimulationIteration; i++)
        {
            phyicsScene.Simulate(Time.fixedDeltaTime);
            line.SetPosition(i, ghostObj.transform.position);
        }
        
        Destroy(ghostObj.gameObject);
    }

}
