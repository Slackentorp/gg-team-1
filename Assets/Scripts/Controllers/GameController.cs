using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PauseState GameState;



    // Use this for initialization
    void Start()
    {
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
