using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SplashManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        var audio = GetComponent<AudioSource>();
        audio.Play();
        while (audio.isPlaying)
        {
            yield return null;
        }
        LoaderManager.LoadScene(MenuManager.sceneName);
    }
}
