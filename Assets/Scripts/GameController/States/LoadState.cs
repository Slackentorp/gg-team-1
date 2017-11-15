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
        gm.InputManager = new InputManager(gm.InputSettings);
   //     gm.GameCamera.SetTarget(gm.Moth.transform.position);
        gm.SetState(new RunState(gm));
    }

    public override void Tick()
    {
    }
}