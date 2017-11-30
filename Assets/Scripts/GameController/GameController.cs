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
    public Animator CinemaBars;

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
            "0 being directly below"), Range(0.0f, 90.0f)]
    public float minimumVerticalAngle = 20;
    [Tooltip("Controls the angle that the Camera maximum can move to above the Moth, " +
            "180 being directly above"), Range(90.0f, 180.0f)]
    public float maximumVerticalAngle = 160;
    [Tooltip("Adjusts the smoothness of the Moth displacement based on the Camera movement.")]
    public float cameraDamping;
    [Space(15)]

    [Header("Moth Attributes")]
    [Tooltip("Determines the speed of the moths flight to/from points")]
    public float mothFlightSpeed;
    [Tooltip("The speed up and slow down curve of the Moth's flight speed")]
    public AnimationCurve mothFlightSpeedCurve;
    [Tooltip("How close the Moth should be placed to the clicked destination"), Range(0.0f, 1.0f)]
    public float mothDistanceToObject;
    [Tooltip("How close the Moth should be placed to the clicked destination"), Range(0.0f, 1.0f)]
    public float mothDistanceToCeiling;
	[Space(7)]
    [Tooltip("The speed up and slow down curve of the Moth's fidgiting speed")]
    public AnimationCurve MothFidgitingCurve;
    [Tooltip("Controls the speed of the Moth's movement while fidgiting")]
    public float mothSpeedModifier;
    [Tooltip("The max distance of the randomized value between each fidgit point. " +
            "The higher it is the shorter the distance"), Range(1, 100)]
    public int FidgetingDistanceReducerMax;
    [Tooltip("The minimum distance of the randomized value between each fidgit point. " +
            "The higher it is the shorter the distance"), Range(0, 99)]
    public int FidgetingDistanceReducerMin;
	[Tooltip("Limits the forward/backwards figiding of the Moth. " +
			"The larger the more the movement is limited")]
	public float LimitMothForwardFidgit = 1.5f;
	[Tooltip("Determines the placement of the moth vertically, " +
			"in relation to the middle of the screen. The higher the value is" +
			"the lower it is placed in the screen")]
	public float VerticalMothScreenPosition = 0.08f;
	[Tooltip("Controls the amount of fidgit the moth should have in flight between points.")]
	public float FidgitInFlightReducer = 2;
	[Tooltip("The scalar that controls how quickly the moth should switch between fidgit points")]
	public float fidgitTimeScalar = 1.5f;
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
    public GameObject FragmentToLightSourceParticles;
    public AnimationCurve FragmentToLightSourceCurve;
    [Space(15)]

    [HideInInspector]
    public Vector3 cameraHeading;
    [HideInInspector]
    public Puzzle tutorialPuzzle;


    [SerializeField, Tooltip("Loading Screen")]
    private GameObject loadingPanel;
    public GameObject LoadingPanel { get { return loadingPanel; } }

    private GameState currentState;

    // Use this for initialization
    void Start()
    {
        cameraHeading = GameCamera.transform.position - Moth.transform.position;
        cameraController = new CameraController(GameCamera.transform, 2, cameraHeading, 1, Moth.transform, false,
                                                cameraDamping, cameraTurnSpeedY, cameraTurnSpeedX, minimumVerticalAngle,
                                                maximumVerticalAngle);


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