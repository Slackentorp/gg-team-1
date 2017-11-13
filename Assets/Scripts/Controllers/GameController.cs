using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using Gamelogic.Extensions;
using UnityEngine.UI; 

public class GameController : Singleton<GameController>
{
 /*   [SerializeField]
    private PauseState GameState;

    [SerializeField]
    private GameObject startLamp;

    [SerializeField]
    private Transform cameraStartPosition; 

    [SerializeField]
    private List<LightSourceInput> puzzleOneLights; 

    private List<bool> puzzleSolved; 

	private LocalizationManager localization;
    public GameObject mothObject; 
    private CameraController gameCamera;
    private LightController lightController; 

    // Use this for initialization
    void Awake()
    {
        lightController = GetComponent<LightController>(); 
		localization = new LocalizationManager();
        mothObject = BootstrapManager.Instance.mothObject;  
    }

    [ContextMenu("DAN")]
    void SetDanish()
    {
        localization.SetDanish();
    }

    [ContextMenu("ENG")]
    void SetEnglish()
    {
        localization.SetEnglish();
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

    public void SolveTutorial()
    {
        lightController.TurnOnMainLights(); 
    }

    public void StartGame()
    {
		gameCamera.transform.position = startLamp.GetComponent<LightSourceInput>().CameraPosition;
		gameCamera.transform.forward = (startLamp.transform.position - gameCamera.transform.position).normalized; 
		gameCamera.SetTarget(startLamp.GetComponent<LightSourceInput>().CameraPosition);
        gameCamera.SetStoryCam(false);
    }

    public void SetupScene(Camera newGameCamera)
    {
        EventBus.Instance.SetMothPosition(startLamp.transform.TransformPoint(startLamp.GetComponent<LightSourceInput>().GetLandingPos()));
        SetGameCamera(newGameCamera.gameObject);
        /*mothObject = gameCamera.TargetPos.gameObject;
		//gameCamera.transform.position = (gameCamera.transform.position - mothObject.transform.position).normalized;
		gameCamera.TargetPos = cameraStartPosition;
        lightController.LoadLights();
		StartGame();
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

    public void SetMothObject(GameObject moth)
    {
        mothObject = moth; 
    }

    public enum PauseState
    {
        PAUSED,
        PLAYING
    }*/
}
