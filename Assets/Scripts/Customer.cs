using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private CustomerFace customerFace;
    
    [SerializeField] private float walkSpeed = 1f;
    private NavMeshAgent navMeshAgent;
    public bool walking = true;
    private Thrower player;
    
    private List<Transform> travelPoints = new List<Transform>();
    [SerializeField] private Material[] materials;
    
    Coroutine travelCoroutine;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Thrower>();
        player.OnStartThrow += LookAtPlayer;
        player.OnEndThrow += ContinueWalking;
        navMeshAgent.speed = walkSpeed;
        
        var meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.material = materials[Random.Range(0, materials.Length)];
    }
    
    private void ContinueWalking()
    {
        StartCoroutine(WaitAndContinueWalking());
    }

    private IEnumerator WaitAndContinueWalking()
    {
        yield return new WaitForSeconds(0.5f);
        walking = true;
        travelCoroutine = StartCoroutine(Travel(travelPoints));
    }

    private void LookAtPlayer()
    {
        navMeshAgent.SetDestination(transform.position);
        StopCoroutine(travelCoroutine);
        walking = false;
        transform.DOLookAt(player.transform.position, 0.5f);
    }

    public void Init(List<Transform> travelPoints)
    {
        this.travelPoints = travelPoints;
        travelCoroutine = StartCoroutine(Travel(travelPoints));
    }

    private IEnumerator Travel(List<Transform> travelPoints)
    {
        var randomIndex = Random.Range(0, travelPoints.Count);
        int currentTravelPoint = randomIndex;
        navMeshAgent.SetDestination(travelPoints[currentTravelPoint].position);
        while (walking)
        {
            if (Vector3.Distance(transform.position, travelPoints[currentTravelPoint].position) < 0.5f)
            {
                currentTravelPoint = Random.Range(0, travelPoints.Count);
                navMeshAgent.SetDestination(travelPoints[currentTravelPoint].position);
            }
            yield return null;
        }
    }
}
