using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
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
		mothBehaviour = new MothBehaviour(gm.Moth, Camera.main, .4f);
    }

    public override void Tick()
    {
        cameraController.Update();
		mothBehaviour.Update();

		//InputEvent inputEvent = gm.InputManager.CheckInput();
		InputEvent inputEvent = new InputEvent();
		if (inputEvent.GameObject != null)
        {
            ITouchInput itt = inputEvent.GameObject.GetComponent<ITouchInput>();
            if (itt != null)
            {
                switch (inputEvent.InputType)
                {
                    case InputType.TOUCH_DOWN:
                        itt.OnTouchDown(inputEvent.TouchPosition);
                        break;
                    case InputType.TOUCH_HOLD:
                        itt.OnToucHold(inputEvent.TouchPosition);
                        break;
                    case InputType.TOUCH_UP:
                        itt.OnTouchUp();
                        break;
                    case InputType.TOUCH_EXIT:
                        itt.OnTouchExit();
                        break;
                    case InputType.SWIPE:
                        itt.OnSwipe(TouchDirection.Down);
                        break;
                    case InputType.TAP:
                        itt.OnTap();
                        break;
                }
            }
        }

        // Should return what GameObject is being touched, and the type of touch

    }
}