﻿using System.Collections;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;

#pragma warning disable 0414
public class GameControllerMain : Singleton<GameControllerMain>
{
    [SerializeField, ReadOnly]
    private string currentStateLiteral;

    public GameObject Moth;
    public GameObject GameCamera;
    public Queue StoryQueue;
    public LocalizationManager localization;
    public LightController LightController;
    public InputManager InputManager;
    public InputHandlerSettings InputSettings;

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

    [ContextMenu("DAN")]
    void SetDanish() {
        localization.SetDanish();
    }

    [ContextMenu("ENG")]
    void SetEnglish() {
        localization.SetEnglish();
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
}