using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class InteractableState : GameState
{
    private Vector3 originPos, originForward;
    private Quaternion originRotation;
    private float time;

    bool lerpOut;

    private CameraController cameraController;
    private Interactable currentInteractable;
    public InteractableState(GameController gm, Interactable interactable) : base(gm)
    {
        currentInteractable = interactable;
    }

    public override void Tick()
    {
        if (lerpOut) return;
        float t = gm.FragmentLerpCurve.Evaluate(time * gm.cameraToFragmentSpeed);

        Vector3 position;
        position = Vector3.Lerp(originPos, currentInteractable.transform.position + currentInteractable.CamPosition, t);

        Vector3 forward;
        forward = Vector3.Lerp(originForward, currentInteractable.CamForward, t);

        Quaternion rotation;
        rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(currentInteractable.CamOrientaion), t);

        if (t < 1)
        {
            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            time += Time.deltaTime;
        }
        else
        {    
            if (currentInteractable is Puzzle)
            {
                CheckInput();
                ((Puzzle)currentInteractable).UpdatePuzzle();
                if(((Puzzle)currentInteractable).IsSolved)
                {
                    lerpOut = true;
                    currentInteractable.Play(EndOfFragmentCallback);
                }
            }
        }

    }

    public override void OnStateEnter()
    {
        cameraController = gm.cameraController;

        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;
        cameraController.SetFragmentMode(true);
        if (currentInteractable is Puzzle)
        {
            currentInteractable.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            currentInteractable.Play(EndOfFragmentCallback);
        }
    }

    private void EndOfFragmentCallback()
    {
        Debug.Log("End of interactable: " + currentInteractable.gameObject.name);
        gm.StartCoroutine(Leaving());
        lerpOut = true;
    }

    public override void OnStateExit()
    {
        AkSoundEngine.StopAll(currentInteractable.gameObject);
    }

    private IEnumerator Leaving()
    {
        time = 0;
        while (time * gm.cameraToFragmentSpeed < 1)
        {
            float t = gm.FragmentLerpCurve.Evaluate(time * gm.cameraToFragmentSpeed);
            Vector3 position;
            position = Vector3.Lerp(currentInteractable.transform.position + currentInteractable.CamPosition, originPos, t);

            Vector3 forward;
            forward = Vector3.Lerp(currentInteractable.CamForward, originForward, t);

            Quaternion rotation;
            rotation = Quaternion.Lerp(Quaternion.Euler(currentInteractable.CamOrientaion), originRotation, t);

            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            time += Time.deltaTime;
            yield return null;
        }

        gm.SetState(new RunState(gm));
    }

    void CheckInput()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
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
            else
            {
                cameraController.Update();
            }
        }
    }
}