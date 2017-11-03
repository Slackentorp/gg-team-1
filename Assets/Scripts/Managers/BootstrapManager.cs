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
                gameCamera.GetComponent<CameraTranslation>().SetMoth(levelCamera.GetComponent<CameraTranslation>().GetMoth());
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
            // Sound scape
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            // Apartment
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            StartCoroutine(DelayReload());
        }
    }

    IEnumerator DelayReload()
    {
        yield return new WaitForSeconds(1);
        Start();
    }
}