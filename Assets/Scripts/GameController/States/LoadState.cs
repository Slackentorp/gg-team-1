using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class LoadState : GameState
{
    public LoadState(GameController gm) : base(gm)
    {
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
		gm.mothBehaviour = new MothBehaviour(gm.Moth, gm.GameCamera.GetComponent<Camera>(), .4f, gm.MothChildCurve, gm.noiseReducer, gm.mothSpeedModifier);
        gm.mothSounds = new MothSounds(gm.GameCamera.transform, gm.mothBehaviour, gm.Moth.transform);
        gm.cameraHeading = gm.GameCamera.transform.position - gm.Moth.transform.position;
   //     gm.GameCamera.SetTarget(gm.Moth.transform.position);
        gm.SetState(new RunState(gm));
    }

    public override void Tick()
    {
    }
}