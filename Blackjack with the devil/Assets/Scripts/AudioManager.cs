using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]public static AudioManager Instance;
    [SerializeField] private string audioClipNameOnLoop;
    private AudioSource audioSource;
    private AudioClip[] audioClips;



    private void Awake() 
    {
        if(Instance == null)
            Instance = this;

        audioClips = Resources.LoadAll<AudioClip>("Audio/Sounds");
    }

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    

        if(audioClipNameOnLoop == string.Empty)
            return;

        GameObject audioSourceObject = new GameObject("AUDIO CLIP LOOP");
        AudioSource newAudioSource = audioSourceObject.AddComponent<AudioSource>();
        newAudioSource.loop = true;
        AudioClip audioClip = audioClips.ToList().Find(a => a.name == audioClipNameOnLoop);

        newAudioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(string audioClipName)
    {
        AudioClip audioClip = audioClips.ToList().Find(a => a.name == audioClipName);

        if(audioClip == null)
        {
            Debug.LogError("Cant find audio clip");
            return;
        }

        audioSource.PlayOneShot(audioClip);
    }

    public void PlaySound(string audioClipName, float volume)
    {
        AudioClip audioClip = audioClips.ToList().Find(a => a.name == audioClipName);

        if(audioClip == null)
        {
            Debug.LogError("Cant find audio clip");
            return;
        }

        audioSource.PlayOneShot(audioClip, volume);
    }


    public void PlayRandomSoundByIndex(string baseAudioClipName, int indexStart, int indexEnd)
    {
        int randomIndex = UnityEngine.Random.Range(indexStart, indexEnd + 1);
        string randomAudioClipName = baseAudioClipName + randomIndex.ToString();
        Debug.Log(randomAudioClipName);
        PlaySound(randomAudioClipName);
    }

    public void PlayRandomSoundByIndex(string baseAudioClipName, int indexStart, int indexEnd, float minVolume, float maxVolume)
    {
        int randomIndex = UnityEngine.Random.Range(indexStart, indexEnd + 1);
        string randomAudioClipName = baseAudioClipName + randomIndex.ToString();
        float randomVolume = UnityEngine.Random.Range(minVolume, maxVolume);
        
        PlaySound(randomAudioClipName, randomVolume);
    }

    public void PlayRandomSoundByName(string[] randomAudioClipNames)
    {
        string randomAudioClipName = randomAudioClipNames[UnityEngine.Random.Range(0, randomAudioClipNames.Length - 1)];

        PlaySound(randomAudioClipName);
    }

    public void PlaySoundWithRandomPitch(string audioClipName, float minPitch, float maxPitch)
    {
        GameObject audioSourceObject = new GameObject("AUDIO SOURCE");
        AudioSource newAudioSource = audioSourceObject.AddComponent<AudioSource>();
        audioSourceObject.AddComponent<AudioSourceDestroyer>();
        float randomPitch = UnityEngine.Random.Range(minPitch, maxPitch);
        newAudioSource.pitch = randomPitch;
        
        AudioClip audioClip = audioClips.ToList().Find(a => a.name == audioClipName);

        newAudioSource.PlayOneShot(audioClip);
    }
}
