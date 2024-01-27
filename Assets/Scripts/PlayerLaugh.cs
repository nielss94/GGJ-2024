using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaugh : MonoBehaviour
{
    [SerializeField] private AudioClip[] laughClips;
    
    private SnackbarInput inputActionAsset;
    [SerializeField] private float playerPitch = 1;
    
    private void Awake()
    {
        playerPitch = Random.Range(.6f, 1.6f);
        inputActionAsset = new SnackbarInput();
        inputActionAsset.Ingame.Laugh.Enable();
        inputActionAsset.Ingame.Laugh.performed += ctx =>
        {
            StartCoroutine(AudioManager.Instance.PlayMultipleClips(Random.Range(2,4), laughClips, transform.position, playerPitch));
        };
    }
}
