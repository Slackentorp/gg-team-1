using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class PuzzleState : GameState
{

    private Vector3 originPos;
    private Vector3 originForward;
    private Quaternion originRotation;
    private CameraController cameraController;

    private float time;
    private float speed = 0.35f;

    private float distanceToPuzzle;

    public Puzzle currentPuzzle = null;

    public PuzzleState(GameController gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        //AkSoundEngine.PostEvent("CAMERA_MOVE", gm.GameCamera);
        //gm.NextPuzzle.Play(EndOfPuzzleCallback);
        cameraController = new CameraController(gm.GameCamera.transform, 2, gm.cameraHeading, 1, 1, gm.Moth.transform, true, gm.cameraDamping);
        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;

        distanceToPuzzle = Vector3.SqrMagnitude(originPos - (gm.NextPuzzle.transform.position + gm.NextPuzzle.CamPosition));

        /*    gm.GameCamera.transform.position = gm.NextFragment.transform.position + gm.NextFragment.CamPosition;
			gm.GameCamera.transform.rotation = Quaternion.Euler(gm.NextFragment.CamOrientaion);
			gm.GameCamera.transform.forward = gm.NextFragment.CamForward;*/
    }

    private void EndOfPuzzleCallback()
    {
        Debug.Log("End of " + gm.NextPuzzle.PuzzleId); //
        gm.SetState(new RunState(gm));
    }

    public override void OnStateExit()
    {
        gm.NextPuzzle = null;
        gm.GameCamera.transform.position = originPos;
        gm.GameCamera.transform.forward = originForward;
    }

    public override void Tick()
    {
        CheckInput();
        //Debug.Log("Origin " + originPos); 
        //      float f = Vector3.SqrMagnitude(gm.GameCamera.transform.position - (gm.NextFragment.transform.position + gm.NextFragment.CamPosition));
        float t = gm.PuzzleLerpCurve.Evaluate(time * speed);

        Vector3 position;
        position = Vector3.Lerp(originPos, gm.NextPuzzle.transform.position + gm.NextPuzzle.CamPosition, t);

        Vector3 forward;
        forward = Vector3.Lerp(originForward, gm.NextPuzzle.CamForward, t);

        //Quaternion rotation;
        //rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(gm.NextPuzzle.CamOrientaion), t); 

        if (t < 1)
        {
            gm.GameCamera.transform.position = position;
            //gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            time += Time.deltaTime;
        }
        else
        {
            gm.NextPuzzle.TurnOffCollider();
        }

        if (currentPuzzle != null)
        {
            currentPuzzle.UpdatePuzzle();
        }

        if (currentPuzzle.IsSolved)
        {
            gm.SetState(new RunState(gm));
            //Debug.Log("I'm solved"); 
        }
        //else
        //{
        //	cameraController.Update();
        //}

    }

    void CheckInput()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
        if (inputEvent.GameObject != null)
        {
            //            InputManager.isTouchingObject = true;
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
    }
}
