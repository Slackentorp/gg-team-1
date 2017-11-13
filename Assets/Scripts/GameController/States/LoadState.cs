using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : GameState
{
    public LoadState(GameControllerMain gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        Debug.Log("Entered load state");
        gm.localization = new LocalizationManager();
        gm.LightController.LoadLights();
   //     gm.GameCamera.SetTarget(gm.Moth.transform.position);
        gm.SetState(new RunState(gm));
    }

    public override void Tick()
    {
    }
}