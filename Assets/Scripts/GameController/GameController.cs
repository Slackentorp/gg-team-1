using System.Collections;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0414
public class GameController : Singleton<GameController>
{
    [SerializeField, ReadOnly]
    private string currentStateLiteral;

    public GameObject Moth;
	public GameObject GameCamera;
	public Queue StoryQueue;
	public LocalizationManager localization;
	public LightController LightController;
	public InputManager InputManager;
	public Text HeadsetStateUIText;
	public MothBehaviour mothBehaviour;
	public MothSounds mothSounds;
	public InputHandlerSettings InputSettings;
    public CameraController cameraController;
	
	[Header("Camera Attributes")]
	[Tooltip("Determines the camera's turn speed on it's y-axis")]
	public float cameraTurnSpeedY;
	[Tooltip("Determines the camera's turn speed on it's x-axis")]
	public float cameraTurnSpeedX;
	[Tooltip("Controls the angle that the Camera maximum can move to underneath the Moth, " +
			"0 being directly below"), Range(0,90)]
	public float minimumVerticalAngle = 20;
	[Tooltip("Controls the angle that the Camera maximum can move to above the Moth, " +
			"180 being directly above"), Range(90, 180)]
	public float maximumVerticalAngle = 160;
	[Tooltip("Adjusts the smoothness of the Moth displacement based on the Camera movement.")]
	public float cameraDamping;
	[Space(15)]

	[Header("Moth Attributes")]
	[Tooltip("Determines the speed of the moths flight to/from points")]
	public float mothFlightSpeed; 
	[Tooltip("How close the Moth should be closed to the clicked destination")]
	public float mothDistanceToObject;
	[Tooltip("Determines the Moth's movement when figiditing")]
	public AnimationCurve MothFidgitingCurve;
	[Tooltip("Controls the speed of the Moth's movement while fidgiting")]
	public float mothSpeedModifier;
	[Tooltip("The amount of distance the between each fidgit point. " +
			"The higher it is the shorter the distance")]
	public int FidgetingDistanceReducer;
	[Space(15)]

	[Header("Fragment Attributes")]
	[Tooltip("Determines the speed ups, and slow downs when dollying to/from the fragments")]
	public AnimationCurve FragmentLerpCurve;
	[Tooltip("Decides the speed with which the camera moves to defined fragment position." +
			"Also determines rotation speed of camera when moving to fragment")]
	public float cameraToFragmentSpeed; 
	[Space(15)]
	[Header("Puzzle Attributes")]
	[Tooltip("Determines the speed ups, and slow downs when dollying to/from the puzzles")]
	public AnimationCurve PuzzleLerpCurve;
	[Tooltip("Decides the speed with which the camera moves to defined puzzle position." +
			"Also determines rotation speed of camera when moving to puzzle")]
	public float cameraToPuzzleSpeed;
	[Space(15)]

    [HideInInspector]
    public Vector3 cameraHeading;
    [HideInInspector]
    public Puzzle tutorialPuzzle;


    private GameState currentState;

    // Use this for initialization
    void Start()
    {
        cameraHeading = GameCamera.transform.position - Moth.transform.position;
    //    cameraController = new CameraController(GameCamera.transform, 2, cameraHeading, 1, 1, Moth.transform, false, cameraDamping);

        cameraController = new CameraController(GameCamera.transform, 2, cameraHeading, 1, Moth.transform, false, 
												cameraDamping, cameraTurnSpeedY, cameraTurnSpeedX, minimumVerticalAngle,
												maximumVerticalAngle);

        //tutorialPuzzle = GameObject.FindWithTag("Respawn").GetComponent<Puzzle>(); 
        //NextPuzzle = tutorialPuzzle;
        SetState(new LoadState(this));
    }

    private void Update()
    {
        if (currentState != null)
        {
            // CheckInput(); 
            currentState.Tick();
        }
    }

    private void OnGUI()
    {
        if (currentState != null)
        {
            currentState.InternalOnGUI();
        }
    }

    [ContextMenu("DAN")]
    public void SetDanish()
    {
        localization.SetDanish();
        AkSoundEngine.SetState("LANGUAGE", "DANISH");
    }

    [ContextMenu("ENG")]
    public void SetEnglish()
    {
        localization.SetEnglish();
        AkSoundEngine.SetState("LANGUAGE", "ENGLISH");
    }

    public void SetState(GameState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }
        currentState = state;
        currentStateLiteral = state.ToString();

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void QuitFragment()
    {
        if (currentState is InteractableState)
        {
            SetState(new RunState(this));
        }
    }

    
}