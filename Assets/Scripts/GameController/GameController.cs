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
    public AnimationCurve FragmentLerpCurve;
    public AnimationCurve MothChildCurve;
    public float mothSpeedModifier;
    public float cameraDamping;
    public int noiseReducer;
    [HideInInspector]
    public Vector3 cameraHeading;

    public CameraController cameraController; 

    //private Interactable nextInteractable;
    //public Interactable NextInteractable { get { return nextInteractable; } set { nextInteractable = value; } }

    public AnimationCurve PuzzleLerpCurve;

    [HideInInspector]
    public Puzzle tutorialPuzzle;


    private GameState currentState;

    // Use this for initialization
    void Start()
    {
        cameraHeading = GameCamera.transform.position - Moth.transform.position;
        cameraController = new CameraController(GameCamera.transform, 2, cameraHeading, 1, 1, Moth.transform, false, cameraDamping);
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