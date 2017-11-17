using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashscreenLoadGame : MonoBehaviour
{
	uint framesActive = 0;
	void OnEnable()
	{
		framesActive = 0;
	}

	void Update(){
		if(framesActive == 2)
		{
			StartCoroutine(LoadScenes());
			print("Loading Scenes");
		}
		framesActive++;
	}

	void OnDisable()
	{
		framesActive = 0;
	}

	IEnumerator LoadScenes()
    {
        AsyncOperation bootstrapLoad =
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        AsyncOperation soundScapeLoad =
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
		AsyncOperation apartmentLoad =
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);

        while (!soundScapeLoad.isDone || !bootstrapLoad.isDone || !apartmentLoad.isDone)
        {
            yield return null;
        }
		SceneManager.UnloadSceneAsync(0);
    }
}
