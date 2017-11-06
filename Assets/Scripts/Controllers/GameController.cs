using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Extensions; 

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private PauseState GameState;

    [SerializeField]
    private GameObject startLamp;

    private GameObject mothObject; 
    private Camera gameCamera; 

    // Use this for initialization
    void Start()
    {
    }

    private void Update()
    {
        
    }

    public void SetupScene(Camera gameCamera)
    {
        EventBus.Instance.SetMothPosition(startLamp.transform.TransformPoint(startLamp.GetComponent<LightSourceInput>().GetLandingPos()));
        SetGameCamera(gameCamera);
        mothObject = gameCamera.GetComponent<CameraController>().TargetPos.gameObject;
        gameCamera.transform.forward = (gameCamera.transform.position - mothObject.transform.position).normalized;
    }

    public void SetGameCamera(Camera newCam)
    {
        gameCamera = newCam; 
    }

    public void SetMothObject(GameObject moth)
    {
        mothObject = moth; 
    }

    public enum PauseState
    {
        PAUSED,
        PLAYING
    }
}
