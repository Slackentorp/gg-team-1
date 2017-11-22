using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : Singleton<BootstrapManager>
{
    [SerializeField]
    private Camera gameCamera;

    public GameObject mothObject;

    [SerializeField]
    private Scene levelScene;

    /*
    for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = SceneManager.GetSceneByBuildIndex(i).name;
            if(sceneName.Equals("SplashScreen"))
            {
                return;
            }
        }
    */

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = SceneManager.GetSceneByBuildIndex(i).name;
            Debug.Log("SceneName: " +sceneName);
            if(!string.IsNullOrEmpty(sceneName) && sceneName.Equals("SplashScreen"))
            {
             //   return;
            }
        }
        print("Bootstrap Start");
        levelScene = new Scene();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!SceneManager.GetSceneAt(i).name.ToUpper().Equals("BOOTSTRAP") &&
                !SceneManager.GetSceneAt(i).name.ToUpper().Equals("SOUNDSCAPE") &&
                !SceneManager.GetSceneAt(i).name.ToUpper().Equals("STORYEVENTS"))
            {
                levelScene = SceneManager.GetSceneAt(i);
                SceneManager.SetActiveScene(levelScene);
                print("Level scene is: " + levelScene.name);
                break;
            }
        }
        if (!string.IsNullOrEmpty(levelScene.name))
        {
            GameObject[] rootGameObjects = levelScene.GetRootGameObjects();
            GameObject levelCamera =
                rootGameObjects.FirstOrDefault(o => o.CompareTag("Level Camera"));
            if (levelCamera != null)
            {
                gameCamera.transform.position = levelCamera.transform.position;
                gameCamera.transform.rotation = levelCamera.transform.rotation;
                Destroy(levelCamera);
            }
            else
            {
                Debug.LogWarning("No camera with tag 'Level Camera' " +
                    "was found in level. Using default settings.");
            }
        }
        else
        {
            StartCoroutine(DelayReload());
        }
    }

    IEnumerator DelayReload()
    {

        AsyncOperation soundScapeLoad =
            SceneManager.LoadSceneAsync("SoundScape", LoadSceneMode.Additive);
        AsyncOperation apartmentLoad =
            SceneManager.LoadSceneAsync("Apartment", LoadSceneMode.Additive);

        while (!soundScapeLoad.isDone || !apartmentLoad.isDone)
        {
            yield return null;
        }
        Start();
    }

    public void ChangeLevelScene(string level)
    {
        StartCoroutine(AsyncChangeLevelScene(level));
    }

    private IEnumerator AsyncChangeLevelScene(string level)
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(levelScene);
        while (!unload.isDone)
        {
            yield return null;
        }
        //     Start();
    }

}