using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerOrder : MonoBehaviour
{
    public event Action OnOrderCorrect = delegate {  };
    public event Action OnOrderIncorrect = delegate {  };
    
    public SnackType snackType;
    [SerializeField] private float patienceTimeSeconds = 30f;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image timerFill;
    [SerializeField] private Image snackImage;
    [SerializeField] private List<SnackOption> snackSprites;
    [SerializeField] private Color startColor = Color.green;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private AudioClip[] orderCorrectSounds;
    [SerializeField] private AudioClip[] orderIncorrectSounds;
    
    private Camera mainCamera;
    private float startTime;
    private AudioManager audioManager;
    
    private void Start()
    {
        Array values = Enum.GetValues(typeof(SnackType));
        snackType = (SnackType)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        snackImage.sprite = snackSprites.First(x => x.snackType == snackType).sprite;
        mainCamera = Camera.main;
        canvas.worldCamera = mainCamera;
        startTime = Time.time;
        audioManager = AudioManager.Instance;
    }
    
    public void OnSnackHit(SnackType snackType)
    {
        if (snackType == this.snackType)
        {
            Debug.Log("Order correct");
            OnOrderCorrect();
            if (orderCorrectSounds.Length > 0) audioManager.PlayClip(orderCorrectSounds[Random.Range(0, orderCorrectSounds.Length)], transform.position, 1f, true, 0.2f);
        }
        else
        {
            Debug.Log("Order incorrect");
            OnOrderIncorrect();
            if (orderIncorrectSounds.Length > 0) audioManager.PlayClip(orderIncorrectSounds[Random.Range(0, orderIncorrectSounds.Length)], transform.position, 1f, true, 0.2f);
        }
    }

    private void Update()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        canvas.transform.LookAt(mainCamera.transform);
        timerFill.fillAmount = (Time.time - startTime) / patienceTimeSeconds;
        timerFill.color = Color.Lerp(startColor, endColor, timerFill.fillAmount);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Projectile projectile))
        {
            if (projectile.isFrozen) return;
            
            // Snack hit body, bad!
            Debug.Log("Order incorrect");
            OnOrderIncorrect();
            if (orderIncorrectSounds.Length > 0) audioManager.PlayClip(orderIncorrectSounds[Random.Range(0, orderIncorrectSounds.Length)], transform.position, 1f, true, 0.2f);
            Destroy(projectile.gameObject);    
        }
    }
}
