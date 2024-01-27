using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Thrower thrower;
    [SerializeField] private PlayerMove playerMove;
    private void Awake()
    {
        thrower.OnStartAim += () =>
        {
            animator.SetBool("Aim", true);
        };
        
        thrower.OnEndAim += () =>
        {
            animator.SetBool("Aim", false);
        };
        
        thrower.OnStartThrow += () =>
        {
            animator.SetBool("Charge", true);
        };


        thrower.OnEndThrow += () =>
        {
            animator.SetBool("Charge", false);
            animator.SetBool("Throw", true);
            
            StartCoroutine(SetAfterSeconds(0.5f, "Throw", false));
        };
    }

    private void Update()
    {
        animator.SetBool("Walk1", playerMove.moveInput != Vector2.zero);
    }

    IEnumerator SetAfterSeconds(float time, string animation, bool value)
    {
        yield return new WaitForSeconds(time);
        
        animator.SetBool(animation, value);
    }
    
}
