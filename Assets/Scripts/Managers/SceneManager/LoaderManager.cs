using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{
    static string nextScene;
    static int nextSceneIndex;
    static bool isByIndex;

    public const string nameOfScene = "Loader";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    public static void LoadScene(string sceneName)
    {
        isByIndex = false;
        nextScene = sceneName;
        SceneManager.LoadScene(nameOfScene);
    }

    public static void LoadScene(int sceneBuildIndex)
    {
        isByIndex = true;
        nextSceneIndex = sceneBuildIndex;
        SceneManager.LoadScene(nameOfScene);
    }

    IEnumerator LoadNextScene()
    {
        AsyncOperation asyncLoader;
        if (isByIndex)
        {
            asyncLoader = SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else
        {
            asyncLoader = SceneManager.LoadSceneAsync(nextScene);
        }
        asyncLoader.allowSceneActivation = false;

        while (!asyncLoader.isDone)
        {

            if (asyncLoader.progress >= 0.9f)
            {
                asyncLoader.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
