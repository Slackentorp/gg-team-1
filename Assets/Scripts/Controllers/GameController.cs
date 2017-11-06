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

    [SerializeField]
    private List<LightSourceInput> puzzleOneLights; 

    private List<bool> puzzleSolved; 

    private GameObject mothObject; 
    private Camera gameCamera;
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

    public void SetupScene(Camera gameCamera)
    {
        EventBus.Instance.SetMothPosition(startLamp.transform.TransformPoint(startLamp.GetComponent<LightSourceInput>().GetLandingPos()));
        SetGameCamera(gameCamera);
        mothObject = gameCamera.GetComponent<CameraController>().TargetPos.gameObject;
        gameCamera.transform.forward = (gameCamera.transform.position - mothObject.transform.position).normalized;
        lightController.LoadLights();
    }

    public void SetGameCamera(Camera newCam)
    {
        gameCamera = newCam; 
    }

    public void SetCameraTarget(Vector3 position)
    {
        gameCamera.GetComponent<CameraController>().TargetPos.position = position; 
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
