using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GameState
{
    private CameraController cameraController;

    public RunState(GameControllerMain gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        cameraController = new CameraController(gm.GameCamera.transform, 2,1,1, gm.Moth.transform);
    }

    public override void Tick()
    {
        cameraController.Update();
    }
}