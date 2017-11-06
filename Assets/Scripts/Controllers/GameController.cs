using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PauseState GameState;


    private LocalizationManager localization;

    // Use this for initialization
    void Start()
    {
        localization = new LocalizationManager();
    }

    [Button]
    void SetDanish()
    {
        localization.SetDanish();
    }

    [Button]
    void SetEnglish()
    {
        localization.SetEnglish();
    }


    private void Update()
    {
        
    }

    public enum PauseState
    {
        PAUSED,
        PLAYING
    }
}
