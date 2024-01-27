using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourcePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayClip(AudioClip clip, Vector3 position, float pitch = 1f, bool randomPitch = false)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip);
        Destroy(audioSource.gameObject, clip.length);
    }
    
    public IEnumerator PlayMultipleClips(int amount, AudioClip[] clips, Vector3 position, float pitch = 1f, bool randomPitch = false)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(.175f);
            int randomIndex = Random.Range(0, clips.Length);
            PlayClip(clips[randomIndex], position, pitch, randomPitch);
        }
    }
}
