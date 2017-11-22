using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Controller script that plays the splash screen in proper order, and shows the #Gandhi quote 
/// </summary>
public class SplashscreenController : MonoBehaviour 
{
    [SerializeField]
    private PlayableAsset Splash;
    [SerializeField]
    private PlayableAsset Gandhi;

    private PlayableDirector Director;
    
    void Awake()
    {
        Director = GetComponent<PlayableDirector>();
        Director.playableAsset = Splash;
        Director.Play();
    }
    public void TapToStart()
    {
        Director.playableAsset = Gandhi;
        Director.time = 0;
        Director.Play();
    }
}