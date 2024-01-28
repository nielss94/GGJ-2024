using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioType
{
    Master,
    SFX,
    Music
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSourcePrefab;
    
    [SerializeField] private AudioClip[] ostClips;
    public AudioMixerGroup master;
    public AudioMixerGroup sfx;
    public AudioMixerGroup music;
    
    private AudioSource ostAudioSource;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ostAudioSource = GetComponent<AudioSource>();
            
            StartCoroutine(PlayOst());
            
            music.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            sfx.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            master.audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        }
    }

    private IEnumerator PlayOst()
    {
        while (true)
        {
            ostAudioSource.clip = ostClips[Random.Range(0, ostClips.Length)];
            ostAudioSource.Play();
            yield return new WaitForSeconds(ostAudioSource.clip.length);
        }
    }

    public void PlayClip(AudioClip clip, AudioType audioType, Vector3 position, float pitch = 1f, bool randomPitch = false, float randomPitchRange = 0.2f)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
        
        if (randomPitch)
        {
            pitch = Random.Range(pitch - randomPitchRange, pitch + randomPitchRange);
        }
        
        audioSource.pitch = pitch;
        audioSource.outputAudioMixerGroup = audioType switch
        {
            AudioType.Master => master,
            AudioType.SFX => sfx,
            AudioType.Music => music,
            _ => audioSource.outputAudioMixerGroup
        };
        audioSource.PlayOneShot(clip);
        Destroy(audioSource.gameObject, clip.length);
    }
    
    public IEnumerator PlayMultipleClips(int amount, AudioClip[] clips, AudioType audioType, Vector3 position, float pitch = 1f, bool randomPitch = false, float randomPitchRange = 0.2f)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(.175f);
            int randomIndex = Random.Range(0, clips.Length);
            PlayClip(clips[randomIndex], audioType, position, pitch, randomPitch, randomPitchRange);
        }
    }
}
