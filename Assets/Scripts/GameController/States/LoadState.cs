using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadState : GameState
{
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

        if(PlayerPrefs.GetInt("SAVELOAD", 0) == 1)
        {
            loadedGame = SaveLoad.Load(gm);
        }

        gm.InputManager = new InputManager(gm.InputSettings, gm.GameCamera.GetComponent<Camera>());      
        gm.mothBehaviour = new MothBehaviour(gm.Moth, gm.GameCamera.GetComponent<Camera>(), gm.mothDistanceToObject, gm.mothFlightSpeed, gm.MothFidgitingCurve, 
											gm.FidgetingDistanceReducerMax, gm.FidgetingDistanceReducerMin, gm.mothSpeedModifier, gm.mothFlightSpeedCurve,
											gm.VerticalMothScreenPosition, gm.LimitMothForwardFidgit, gm.FidgitInFlightReducer, gm.fidgitTimeScalar, gm.mothDistanceToCeiling);
        gm.mothSounds = new MothSounds(gm.GameCamera.transform, gm.mothBehaviour, gm.Moth.transform);
        gm.cameraHeading = gm.GameCamera.transform.position - gm.Moth.transform.position;

        if(loadedGame)
        {
            gm.SetState(new RunState(gm));
        }
#if UNITY_EDITOR
        gm.SetState(new RunState(gm));
#endif
    }

    public override void OnStateExit()
    {
        gm.LoadingPanel.SetActive(false); 
        base.OnStateExit();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Apartment")
        {
            if (GameObject.FindWithTag("Respawn") != null && !loadedGame)
            {
                gm.SetState(new RunState(gm));
                //gm.tutorialPuzzle = GameObject.FindWithTag("Respawn").GetComponent<Puzzle>();
                //gm.SetState(new InteractableState(gm, gm.tutorialPuzzle));
            }

        }
    }

    public override void Tick()
    {
    }
}