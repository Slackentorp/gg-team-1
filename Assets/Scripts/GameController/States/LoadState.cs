using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadState : GameState
{
    public LoadState(GameController gm) : base(gm)
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered load state");
        gm.localization = new LocalizationManager();
        if (gm.LightController != null)
        {
            gm.LightController.LoadLights();
        }
        gm.InputManager = new InputManager(gm.InputSettings, gm.GameCamera.GetComponent<Camera>());
	//	gm.mothBehaviour = new MothBehaviour(gm.Moth, gm.GameCamera.GetComponent<Camera>(), .4f, gm.MothChildCurve, gm.noiseReducer, gm.mothSpeedModifier);
        
        gm.mothBehaviour = new MothBehaviour(gm.Moth, gm.GameCamera.GetComponent<Camera>(), gm.mothDistanceToObject, gm.mothFlightSpeed, gm.MothFidgitingCurve, gm.FidgetingDistanceReducer, gm.mothSpeedModifier);
        gm.mothSounds = new MothSounds(gm.GameCamera.transform, gm.mothBehaviour, gm.Moth.transform);
        gm.cameraHeading = gm.GameCamera.transform.position - gm.Moth.transform.position;
		//     gm.GameCamera.SetTarget(gm.Moth.transform.position);
		gm.SetState(new RunState(gm));

    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Apartment")
        {
            if (GameObject.FindWithTag("Respawn") != null)
            {
                gm.tutorialPuzzle = GameObject.FindWithTag("Respawn").GetComponent<Puzzle>();
                gm.SetState(new InteractableState(gm, gm.tutorialPuzzle));
            }
           
        }
    }

    public override void Tick()
    {
    }
}