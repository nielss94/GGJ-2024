using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public SnackType snackType;
    [SerializeField] private Canvas canvas;
    
    public event Action OnOrderCorrect = delegate {  };
    public event Action OnOrderIncorrect = delegate {  };
    
    Camera mainCamera;
    private void Awake()
    {
        Array values = Enum.GetValues(typeof(SnackType));
        snackType = (SnackType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
    }
    
    public void OnSnackHit(SnackType snackType)
    {
        if (snackType == this.snackType)
        {
            OnOrderCorrect();
        }
        else
        {
            OnOrderIncorrect();
        }
    }

    private void Update()
    {
        canvas.transform.LookAt(mainCamera.transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.isFrozen) return;
            
            // Snack hit body, bad!
            OnOrderIncorrect();
            
            Destroy(projectile.gameObject);    
        }
    }
}
