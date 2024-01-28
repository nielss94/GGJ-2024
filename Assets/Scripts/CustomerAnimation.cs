using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private Customer customer;

    [SerializeField] private string[] walkAnimations;
    private string currentWalkAnimation;
    
    [SerializeField] private string[] idleAnimations;
    private string currentIdleAnimation;
    
    private void Start()
    {
        currentWalkAnimation = walkAnimations[UnityEngine.Random.Range(0, walkAnimations.Length)];
        currentIdleAnimation = idleAnimations[UnityEngine.Random.Range(0, idleAnimations.Length)];
    }
    
    private void Update()
    {
        animator.SetBool(currentWalkAnimation, customer.walking);
        animator.SetBool(currentIdleAnimation, !customer.walking);
    }
}
