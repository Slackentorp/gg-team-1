﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadState : GameState
{
	private Fragment[] fragmentPositions;
	private bool loadedGame;
    public LoadState(GameController gm) : base(gm)
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    public override void OnStateEnter()
    {
        gm.LoadingPanel.SetActive(true);
        
        gm.localization = new LocalizationManager();
        if (gm.LightController != null)
        {
            gm.LightController.LoadLights();
        }

        if(PlayerPrefs.GetInt("saveload", -1) == 1)
        {
            if(StoryEventController.Instance != null)
            {
                StoryEventController.Instance.isMuted = true;
            }
            AkSoundEngine.PostEvent("SFX_MUTE", gm.gameObject);
            loadedGame = SaveLoad.Load(gm);
            int storyeventReached = PlayerPrefs.GetInt("SE_REACHED", 0);
            if(storyeventReached != 0)
            {
                AkSoundEngine.PostEvent("STORYEVENT_" + storyeventReached + "_LOAD", gm.gameObject);
            }
            Debug.Log("Loading game: " +loadedGame);
        }

        gm.InputManager = new InputManager(gm.InputSettings, gm.GameCamera.GetComponent<Camera>());      
        gm.mothBehaviour = new MothBehaviour(gm.Moth, gm.GameCamera.GetComponent<Camera>(), gm.mothDistanceToObject, gm.mothFlightSpeed, gm.MothFidgitingCurve, 
											gm.FidgetingDistanceReducerMax, gm.FidgetingDistanceReducerMin, gm.mothSpeedModifier, gm.mothFlightSpeedCurve,
											gm.VerticalMothScreenPosition, gm.LimitMothForwardFidgit, gm.FidgitInFlightReducer, gm.fidgitTimeScalar, gm.mothDistanceToCeiling);
        gm.mothSounds = new MothSounds(gm.GameCamera.transform, gm.mothBehaviour, gm.Moth.transform);
        gm.cameraHeading = gm.GameCamera.transform.position - gm.Moth.transform.position;

		fragmentPositions = GameObject.FindObjectsOfType<Fragment>();
		gm.fragParticleController = new FragmentParticleController(fragmentPositions, gm.fragmentParticles, gm.Moth.transform);

	   if(SceneManager.GetSceneByName("Apartment").isLoaded)
        {
            gm.SetState(new RunState(gm));
        }
    }

    public override void OnStateExit()
    {
        gm.LoadingPanel.SetActive(false); 
        SceneManager.sceneLoaded -= OnSceneLoaded;
        AkSoundEngine.PostEvent("SFX_UNMUTE", gm.gameObject);

        #if UNITY_EDITOR
        if(!UnityEditor.EditorPrefs.GetBool("ShowIntro", true))
        {
            return;
        }
        #endif
        if(StoryEventController.Instance != null)
        {
            StoryEventController.Instance.isMuted = false;
            if(!loadedGame)
            {
                StoryEventController.Instance.PostStoryEvent("STORYEVENT_INTRO", null);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Apartment")
        {
            if (GameObject.FindWithTag("Respawn") != null)
            {
                 gm.SetState(new RunState(gm));
            }

        }
    }

    public override void Tick()
    {
    }
}