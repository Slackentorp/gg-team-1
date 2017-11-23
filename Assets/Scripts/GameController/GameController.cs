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
    [HideInInspector]
    public Fragment NextFragment;
    [HideInInspector]
    public Puzzle NextPuzzle;

    public CameraController cameraController; 

    private Interactable nextInteractable;
    public Interactable NextInteractable { get { return nextInteractable; } set { nextInteractable = value; } }

    public AnimationCurve PuzzleLerpCurve;

    [HideInInspector]
    public Puzzle tutorialPuzzle;


    private GameState currentState;

    // Use this for initialization
    void Start()
    {
        cameraController = new CameraController(GameCamera.transform, 2, cameraHeading, 1, 1, Moth.transform, true, cameraDamping);
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
        if (currentState is FragmentState)
        {
            AkSoundEngine.StopAll(NextFragment.gameObject);
            SetState(new RunState(this));
        }
    }

    private void CheckInput()
    {
        InputEvent inputEvent = InputManager.CheckInput();

        if (inputEvent.GameObject != null)
        {
            // Check if wall
            if (inputEvent.GameObject.CompareTag("Wall") && inputEvent.InputType == InputType.TAP)
            {
                mothBehaviour.SetMothPos(inputEvent.RaycastHit);
                return;
            }

            // Check if interactable object
            Fragment fragment = inputEvent.GameObject.GetComponent<Fragment>();
            Puzzle puzzle = inputEvent.GameObject.GetComponent<Puzzle>();

            if (fragment != null && inputEvent.InputType == InputType.TAP)
            {
                NextFragment = fragment;
                SetState(new FragmentState(this));
                return;
            }
            //Check if Puzzle 
            else if (puzzle != null && inputEvent.InputType == InputType.TAP)
            {
                NextPuzzle = puzzle;
                SetState(new PuzzleState(this));
                PuzzleState newState = (PuzzleState)currentState;
                newState.currentPuzzle = puzzle;
                return;
            }
            else
            {
                //            InputManager.isTouchingObject = true;
                ITouchInput itt = inputEvent.GameObject.GetComponent<ITouchInput>();
                if (itt != null)
                {
                    switch (inputEvent.InputType)
                    {
                        case InputType.TOUCH_DOWN:
                            itt.OnTouchDown(inputEvent.TouchPosition);
                            break;
                        case InputType.TOUCH_HOLD:
                            itt.OnToucHold(inputEvent.TouchPosition);
                            break;
                        case InputType.TOUCH_UP:
                            itt.OnTouchUp();
                            break;
                        case InputType.TOUCH_EXIT:
                            itt.OnTouchExit();
                            break;
                        case InputType.SWIPE:
                            itt.OnSwipe(TouchDirection.Down);
                            break;
                        case InputType.TAP:
                            itt.OnTap();
                            break;
                    }
                }
            }


        }
    }
}