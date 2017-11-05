using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;

    // Use this for initialization
    void Start()
    {
        Scene levelScene = new Scene();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!SceneManager.GetSceneAt(i).name.Equals("Bootstrap") &&
                !SceneManager.GetSceneAt(i).name.Equals("SoundScape"))
            {
                levelScene = SceneManager.GetSceneAt(i);
                break;
            }
        }
        if (!string.IsNullOrEmpty(levelScene.name))
        {
            GameObject[] rootGameObjects = levelScene.GetRootGameObjects();
            GameObject levelCamera =
                rootGameObjects.FirstOrDefault(o => o.tag == "Level Camera");
            if (levelCamera != null)
            {
                gameCamera.transform.position = levelCamera.transform.position;
                gameCamera.transform.rotation = levelCamera.transform.rotation;
                Destroy(levelCamera);
            }
            else
            {
                Debug.LogWarning( "No camera with tag 'Level Camera' " +
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
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        AsyncOperation apartmentLoad =
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        while (!soundScapeLoad.isDone || !apartmentLoad.isDone)
        {
            yield return null;
        }
        Start();
    }
}