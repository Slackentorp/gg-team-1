using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;
using Gamelogic.Extensions;

public class InteractableState : GameState
{
    private Vector3 originPos, originForward;
    private Quaternion originRotation, originMothRotation;
    private float time;
    private bool lerpOut;

    private bool keepBirdLaned;

    private CameraController cameraController;
    private Interactable currentInteractable;
    private Renderer currentInteractableRenderer;

    public InteractableState(GameController gm, Interactable interactable) : base(gm)
    {
        currentInteractable = interactable;
    }

    public override void Tick()
    {
        if (lerpOut) return;
        gm.mothBehaviour.Update();
        float t = gm.FragmentLerpCurve.Evaluate(time * gm.cameraToFragmentSpeed);

        Vector3 position;
        position = Vector3.Lerp(originPos, currentInteractable.transform.position + currentInteractable.CamPosition, t);

        Vector3 forward;
        forward = Vector3.Lerp(originForward, currentInteractable.CamForward, t);

        Quaternion rotation;
        rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(currentInteractable.CamOrientaion), t);

        Quaternion mothRotation = Quaternion.identity;
        if (currentInteractableRenderer != null)
        {
            if (currentInteractable.LandingRotation == Interactable.LandRotation.VERTICAL)
            {
                // Land vertically
                mothRotation = Quaternion.Lerp(originMothRotation, Quaternion.Euler(0, 0, 90), t + .1f);
            }
            else
            {
                // Land horizontally
                mothRotation = Quaternion.Lerp(originMothRotation, Quaternion.Euler(0, 0, 0), t + .1f);
            }
        }

        if (t < 1)
        {
            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            gm.Moth.transform.rotation = mothRotation;
            time += Time.deltaTime;
        }
        else
        {
            if (currentInteractable is Puzzle)
            {
                CheckInput();
                ((Puzzle)currentInteractable).UpdatePuzzle();
                if (((Puzzle)currentInteractable).IsSolved)
                {
                    lerpOut = true;
                    currentInteractable.Play(EndOfFragmentCallback);
                }
            }
        }
        if (keepBirdLaned)
        {
            MakeLanded();
        }
    }

    public override void OnStateEnter()
    {
        cameraController = gm.cameraController;

        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;
        originMothRotation = gm.Moth.transform.rotation;

        gm.mothBehaviour.OnReachedPosition += OnMothLands;
        gm.mothBehaviour.SetFragmentMode(true);

        Vector3 newMothPos = currentInteractable.transform.TransformPoint(currentInteractable.LandingPosition);
        gm.mothBehaviour.SetMothPos(newMothPos);

        currentInteractableRenderer = currentInteractable.GetComponentInChildren<Renderer>();

        cameraController.SetFragmentMode(true);
        if (currentInteractable is Puzzle)
        {
            currentInteractable.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            // It's a Fragment
            currentInteractable.Play(EndOfFragmentCallback);
        }
    }

    private void OnMothLands()
    {
        if (currentInteractable.firstPuzzleCheck)
        {
            gm.mothBehaviour.SetMothAnimationState("Flying");
        }
        else if (!currentInteractable.firstPuzzleCheck)
        {
            keepBirdLaned = true;
            MakeLanded();
        }
    }

    private void MakeLanded()
    {
        AkSoundEngine.PostEvent("MOTH_LANDING", gm.Moth);
        gm.mothBehaviour.SetMothAnimationState("Landing");
    }

    private void EndOfFragmentCallback()
    {
        keepBirdLaned = false;
        gm.mothBehaviour.SetMothAnimationState("Flying");
        gm.StartCoroutine(Leaving());
        lerpOut = true;
    }

    public override void OnStateExit()
    {
        AkSoundEngine.StopAll(currentInteractable.gameObject);
        gm.mothBehaviour.OnReachedPosition -= OnMothLands;
        gm.mothBehaviour.SetFragmentMode(false);
    }

    private IEnumerator Leaving()
    {
        time = 0;

        Vector3 heading = gm.GameCamera.transform.position - gm.Moth.transform.position;
        heading = heading.ResizeMagnitude(gm.cameraController.InitialHeading.magnitude);

        Vector3 desiredPosition = heading + gm.Moth.transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(-heading.normalized);

        Quaternion cameraStartRotation = gm.GameCamera.transform.rotation;
        Quaternion mothStartRotation = gm.Moth.transform.rotation;

        while (time * gm.cameraToFragmentSpeed < 1)
        {
            float t = gm.FragmentLerpCurve.Evaluate(time * gm.cameraToFragmentSpeed);

            Vector3 position = Vector3.Lerp(currentInteractable.transform.position + currentInteractable.CamPosition, desiredPosition, t);
            Quaternion camQ = Quaternion.Lerp(cameraStartRotation, desiredRotation, t);
            Quaternion mothQ = Quaternion.Lerp(mothStartRotation, desiredRotation, t);

            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = camQ;
            gm.Moth.transform.rotation = mothQ;

            time += Time.deltaTime;
            yield return null;
        }

        gm.cameraController.SetHeading(heading);
        gm.mothBehaviour.SetMothAnimationState("Flying");
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