using UnityEngine;
using System.Collections;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller script that plays the splash screen in proper order, and shows the #Gandhi quote 
/// </summary>
public class SplashscreenController : MonoBehaviour {
    [SerializeField]
    private PlayableAsset Splash;

    [SerializeField]
    private double timeToPause = 1200;
    [SerializeField]
    private double timeToLoadLevel = 1200;

    private bool isPaused;
    private IEnumerator LoadLevelsCoroutine;
    private PlayableDirector Director;

    void Awake ()
    {
        Director = GetComponent<PlayableDirector> ();
        Director.playableAsset = Splash;
        Director.Play ();
    }
    public void TapToStart ()
    {
        Director.time = timeToPause + .1;
        Director.Resume ();
    }

    void Update () {
        if (Director.time >= timeToPause && 
            Director.time <= timeToPause + .1 && !isPaused)
        {
            Pause ();
            isPaused = true;
        }

        if(Director.time >= timeToLoadLevel && LoadLevelsCoroutine == null)
        {
            LoadLevelsCoroutine = LoadScenes();
            StartCoroutine(LoadLevelsCoroutine);
        }
    }

    public void Pause () {
        Director.Pause ();
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