using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class RunState : GameState
{
	private MothBehaviour mothBehaviour;
    private MothSounds mothSounds;
    private CameraController cameraController;

    public RunState(GameController gm) : base(gm)
    {
    }


    public override void OnStateEnter()
    {
        cameraController = new CameraController(gm.GameCamera.transform, 2,1,1, gm.Moth.transform, false);
		mothBehaviour = new MothBehaviour(gm.Moth, Camera.main, .4f);
        mothSounds = new MothSounds(gm.GameCamera.transform, mothBehaviour, gm.Moth.transform);
    }

    void CheckInput()
    {
            InputEvent inputEvent = gm.InputManager.CheckInput();
            if (inputEvent.GameObject != null) {
                // Check if wall
                if(inputEvent.GameObject.CompareTag("Wall") && inputEvent.InputType == InputType.TAP)
                {
                    Debug.Log("Ey I'm flying'ere");
                    mothBehaviour.SetMothPos(inputEvent.RaycastHit);
                    return;
                }

                // Check if fragment
                Fragment fragment = inputEvent.GameObject.GetComponent<Fragment>();
                if (fragment != null && inputEvent.InputType == InputType.TAP)
                {
                    gm.NextFragment = fragment;
                    gm.SetState(new FragmentState(gm));
                    return;
                }
                else {
        //            InputManager.isTouchingObject = true;
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
    }

    public override void Tick()
    {
        cameraController.Update();
        mothBehaviour.Update();
        mothSounds.UpdateMothSounds();
        CheckInput();
    }

    public override void InternalOnGUI()
    {
        gm.InputManager.OnGUI();
    }
}