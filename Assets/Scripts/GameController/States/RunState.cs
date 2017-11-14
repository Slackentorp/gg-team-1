using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GameState
{
	private MothBehaviour mothBehaviour; 
    private CameraController cameraController;

    public RunState(GameControllerMain gm) : base(gm)
    {
    }


    public override void OnStateEnter()
    {
        cameraController = new CameraController(gm.GameCamera.transform, 2,1,1, gm.Moth.transform);
		mothBehaviour = new MothBehaviour(gm.Moth.transform, Camera.main, .4f);
    }

    public override void Tick()
    {
        cameraController.Update();
		mothBehaviour.Update();
    }
}