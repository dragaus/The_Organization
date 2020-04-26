using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    AudioSource audioSource;
    public static float volumeValue = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeValue;
        audioSource.loop= false;
        audioSource.playOnAwake = false;
    }

    public void UpdateVolume()
    {
        audioSource.volume = volumeValue;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
