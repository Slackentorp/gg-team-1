using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Extensions;
using UnityEngine.UI; 

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private PauseState GameState;

    [SerializeField]
    private GameObject startLamp;

    [SerializeField]
    private Transform cameraStartPosition; 

    [SerializeField]
    private List<LightSourceInput> puzzleOneLights; 

    private List<bool> puzzleSolved; 

    private GameObject mothObject; 
    private CameraController gameCamera;
    private LightController lightController; 

    // Use this for initialization
    void Start()
    {
        lightController = GetComponent<LightController>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ActivateLightSource();
            lightController.LoadLights();
        }
    }

    private void ActivateLightSource()
    {
        foreach (var item in puzzleOneLights)
        {
            item.Lit = !item.Lit; 
        }
    }

    public void StartGame()
    {
        gameCamera.SetTarget(startLamp.GetComponent<LightSourceInput>().CameraPosition);
        gameCamera.SetStoryCam(false);
    }

    public void SetupScene(Camera newGameCamera)
    {
        EventBus.Instance.SetMothPosition(startLamp.transform.TransformPoint(startLamp.GetComponent<LightSourceInput>().GetLandingPos()));
        SetGameCamera(newGameCamera.gameObject);
        mothObject = gameCamera.TargetPos.gameObject;
        gameCamera.transform.forward = (gameCamera.transform.position - mothObject.transform.position).normalized;
        gameCamera.SetStoryTarget(cameraStartPosition);
        lightController.LoadLights();
    }

    public void SetGameCamera(GameObject newCam)
    {
        gameCamera = newCam.GetComponent<CameraController>(); 
    }

    public void SetCameraTarget(Vector3 position)
    {
        gameCamera.TargetPos.position = position;
        gameCamera.SetStoryCam(false); 
    }

    public void GoToCameraTarget()
    {

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
