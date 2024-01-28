using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryingPan : MonoBehaviour
{
    public GameObject currentSnack;
    [SerializeField] private Transform snackHolder;

    [SerializeField] private float fryDuration;
    private float fryStartTime;
    public bool snackReady = false;
    
    public SnackType currentSnackType;
    [SerializeField] private Canvas canvas;
    private Camera mainCamera;

    [SerializeField] private Image fryingImage;
    [SerializeField] private Image snackImage;
    
    public List<SnackOption> snackOptions = new List<SnackOption>();
    
    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
    {        
        mainCamera = Camera.main;

        canvas.worldCamera = mainCamera;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSnack == null && other.gameObject.TryGetComponent(out Projectile projectile) && projectile.isFrozen)
        {

            if (projectile.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;
            }

            if (projectile.TryGetComponent(out Collider collider))
            {
                collider.enabled = false;
            }

            currentSnackType = projectile.snackType;
            projectile.transform.SetParent(snackHolder);
            projectile.transform.position = snackHolder.position;
            canvas.gameObject.SetActive(true);
            currentSnack = projectile.gameObject;
            fryStartTime = Time.time;
            
            fryingImage.fillAmount = 0f;
            snackImage.sprite = snackOptions.Find(x => x.snackType == currentSnackType).sprite;
            
        }
    }

    private void Update()
    {
        // moved currentSnack with sin
        if (currentSnack != null)
        {
            currentSnack.transform.position = snackHolder.position + Vector3.up * Mathf.Sin(Time.time * 5f) * 0.1f;
            
            if (Time.time - fryStartTime > fryDuration)
            {
                snackReady = true;
            }
            
            // fill image
            fryingImage.fillAmount = Mathf.Clamp01((Time.time - fryStartTime) / fryDuration);
        }
        
    }

    public void TakeSnack()
    {
        Destroy(currentSnack);
        currentSnack = null;
        canvas.gameObject.SetActive(false);
    }
}
