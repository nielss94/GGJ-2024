using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1f;
    private NavMeshAgent navMeshAgent;
    public bool walking = true;
    public bool angry = false;
    private Thrower player;
    
    private List<Transform> travelPoints = new List<Transform>();
    [SerializeField] private Material[] materials;
    [SerializeField] private List<GameObject> hats = new List<GameObject>();
    [SerializeField] private GameObject sunGlasses;
    [SerializeField] private float sunGlassChance = 0.05f;
    Coroutine travelCoroutine;
    
    private float angryLeaveInterval = 1f;
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Thrower>();
        player.OnStartThrow += LookAtPlayer;
        player.OnEndThrow += ContinueWalking;
        navMeshAgent.speed = walkSpeed;
        
        var meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.material = materials[Random.Range(0, materials.Length)];
        
        sunGlasses.SetActive(Random.value < sunGlassChance);
        walking = true;
        var randomHat = Random.Range(0, hats.Count);
        hats[randomHat].SetActive(true);
    }
    
    private void ContinueWalking()
    {
        if (angry)
        {
            return;
        }
        StartCoroutine(WaitAndContinueWalking());
    }

    private IEnumerator WaitAndContinueWalking()
    {
        yield return new WaitForSeconds(0.5f);
        var skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        DOTween.To(() => skinnedMeshRenderer.GetBlendShapeWeight(0), x => skinnedMeshRenderer.SetBlendShapeWeight(0, x), 0f, 0.5f).SetEase(Ease.InOutSine);
        walking = true;
        travelCoroutine = StartCoroutine(Travel(travelPoints));
    }

    private void LookAtPlayer()
    {
        if (angry)
        {
            return;
        }
        navMeshAgent.SetDestination(transform.position);
        StopCoroutine(travelCoroutine);
        walking = false;
        transform.DOLookAt(player.transform.position, 0.5f);
        var skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        DOTween.To(() => skinnedMeshRenderer.GetBlendShapeWeight(0), x => skinnedMeshRenderer.SetBlendShapeWeight(0, x), 100f, 0.5f).SetEase(Ease.InOutSine);
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

    private void Update()
    {
        if (angry && Time.time % angryLeaveInterval < 0.1f)
        {
            navMeshAgent.SetDestination(FindFirstObjectByType<Exit>().transform.position);
        }
    }

    public void Angry()
    {
        angry = true;
        navMeshAgent.SetDestination(FindFirstObjectByType<Exit>().transform.position);
    }
}
