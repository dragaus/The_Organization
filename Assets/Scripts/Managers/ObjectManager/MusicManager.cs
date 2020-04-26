using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    public static float volumeValue = 1f;
    public static int instancesOfMusicManager = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instancesOfMusicManager < 1)
        {
            instancesOfMusicManager++;
            DontDestroyOnLoad(this.gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volumeValue;
    }

    public void UpdateVolume()
    {
        audioSource.volume = volumeValue;
    }
}
