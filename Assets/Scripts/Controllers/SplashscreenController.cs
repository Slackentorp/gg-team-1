using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller script that plays the splash screen in proper order, and shows the #Gandhi quote 
/// </summary>
public class SplashscreenController : MonoBehaviour {
    [SerializeField]
    private PlayableAsset Splash;

    [SerializeField]
    private double timeToPauseStart = 1200;

    [SerializeField]
    private double timeToPauseLanguage = 1200;
    [SerializeField]
    private double timeToLoadLevel = 1200;

    private bool isPaused;
    private IEnumerator LoadLevelsCoroutine;
    private PlayableDirector Director;

    AsyncOperation bootstrapLoad;
    AsyncOperation soundScapeLoad;
    AsyncOperation apartmentLoad;

    void Awake () {
        Director = GetComponent<PlayableDirector> ();
        Director.playableAsset = Splash;
        Director.Play ();
    }

    private void Start () {
        bootstrapLoad =
            SceneManager.LoadSceneAsync (1, LoadSceneMode.Additive);
        soundScapeLoad =
            SceneManager.LoadSceneAsync (2, LoadSceneMode.Additive);
        apartmentLoad =
            SceneManager.LoadSceneAsync (3, LoadSceneMode.Additive);

        bootstrapLoad.allowSceneActivation = false;
        soundScapeLoad.allowSceneActivation = false;
        apartmentLoad.allowSceneActivation = false;
    }

    public void TapToStart () {
        Director.time = timeToPauseStart + .2;
        Director.Resume ();
    }

    public void ChooseLanguage (int lang) {
        PlayerPrefs.SetInt ("LANGUAGE", lang);
        print ("Select language");
        Director.time = timeToPauseLanguage + .2;
        Director.Resume ();
    }

    void Update () {
        if (Director.time >= timeToPauseStart &&
            Director.time <= timeToPauseStart + .1 && Director.playableGraph.IsPlaying ()) {
            Director.Pause ();
        }

        if (Director.time >= timeToPauseLanguage &&
            Director.time <= timeToPauseLanguage + .1 && Director.playableGraph.IsPlaying ()) {
            Director.Pause ();
        }

        if (Director.time >= timeToLoadLevel && LoadLevelsCoroutine == null) {
            LoadLevelsCoroutine = LoadScenes ();
            StartCoroutine (LoadLevelsCoroutine);
        }
    }

    IEnumerator LoadScenes () {
        SceneManager.sceneLoaded += ApartmentLoaded;
        bootstrapLoad.allowSceneActivation = true;
        soundScapeLoad.allowSceneActivation = true;
        apartmentLoad.allowSceneActivation = true;
        print("Triggered load scenes");
        yield return null;
    }

    private void ApartmentLoaded (Scene scene, LoadSceneMode mode) {
        Scene appartment = SceneManager.GetSceneByBuildIndex (3);
        if(scene != appartment){
            return;
        }

        SceneManager.SetActiveScene (appartment);
        SceneManager.UnloadSceneAsync (0);
    }
}