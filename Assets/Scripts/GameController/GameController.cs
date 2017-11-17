﻿using System.Collections;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;

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
    [HideInInspector]
    public Fragment NextFragment;
	[HideInInspector]
	public Puzzle NextPuzzle;
	public InputHandlerSettings InputSettings;
    public AnimationCurve FragmentLerpCurve;
	public AnimationCurve PuzzleLerpCurve;

    private GameState currentState;

    // Use this for initialization
    void Start()
    {
        SetState(new LoadState(this));
    }

    private void Update()
    {
        if (currentState != null)
        {
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
    public void SetDanish() {
        localization.SetDanish();
        AkSoundEngine.SetState("LANGUAGE", "DANISH");
    }

    [ContextMenu("ENG")]
    public void SetEnglish() {
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
        if(currentState is FragmentState)
        {
            SetState(new RunState(this));
        }
    }

	public void QuitPuzzle()
	{
		if (currentState is PuzzleState)
		{
			SetState(new RunState(this));
		}
	}
}