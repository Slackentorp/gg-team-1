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

    IEnumerator InputCoroutine()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
        if (inputEvent.GameObject != null) {
            // Check if fragment
            Fragment fragment = inputEvent.GameObject.GetComponent<Fragment>();
            if (fragment != null)
            {
                gm.NextFragment = fragment;
                gm.SetState(new FragmentState(gm));
            }
            else {
                ITouchInput itt = inputEvent.GameObject.GetComponent<ITouchInput>();
                if (itt != null) {
                    switch (inputEvent.InputType) {
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
        }
        yield return null;
    }

    public override void Tick()
    {
        cameraController.Update();
        gm.StartCoroutine(InputCoroutine());
        mothBehaviour.Update();
        // Should return what GameObject is being touched, and the type of touch

    }

    public override void InternalOnGUI()
    {
        gm.InputManager.OnGUI();
    }
}