using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrder : MonoBehaviour
{
    public event Action OnOrderCorrect = delegate {  };
    public event Action OnOrderIncorrect = delegate {  };
    
    public SnackType snackType;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image timerFill;
    [SerializeField] private Image snackImage;
    [SerializeField] private List<SnackOption> snackSprites;
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    
    private Camera mainCamera;
    
    private void Awake()
    {
        Array values = Enum.GetValues(typeof(SnackType));
        snackType = (SnackType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        snackImage.sprite = snackSprites.First(x => x.snackType == snackType).sprite;
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
    }
    
    public void OnSnackHit(SnackType snackType)
    {
        if (snackType == this.snackType)
        {
            Debug.Log("Order correct");
            OnOrderCorrect();
        }
        else
        {
            Debug.Log("Order incorrect");
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
            Debug.Log("Order incorrect");
            OnOrderIncorrect();
            
            Destroy(projectile.gameObject);    
        }
    }
}
