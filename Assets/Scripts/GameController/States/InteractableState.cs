using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Gamelogic.Extensions;
using UnityEngine;

public class InteractableState : GameState
{
    private Vector3 originPos, originForward;
    private Quaternion originRotation, originMothRotation;
    private float time;
    private bool lerpOut;

    private bool keepMothLandingState;

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

        // We should be able to look around if the interactable has been viewed
        if ((currentInteractable.HasPlayed || gm.hasReachedPointOfNoReturn) && currentInteractable is Fragment)
        {
            //   gm.cameraController.Update();
            CheckInput();
        }

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
         //       ((Puzzle) currentInteractable).UpdatePuzzle();
                if (((Puzzle) currentInteractable).IsSolved && !gm.hasReachedPointOfNoReturn)
                {
                    lerpOut = true;
                    currentInteractable.Play(EndOfFragmentCallback);
                }
            }
        }
        if (keepMothLandingState)
        {
            ForceLanding();
        }
    }

    public override void OnStateEnter()
    {
        cameraController = gm.cameraController;

        // Guards against a zero heading vector, which breaks the camera look
        if(currentInteractable.CamPosition == Vector3.zero)
        {
            currentInteractable.CamPosition = new Vector3(0,.5f,0);
        }

        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;
        originMothRotation = gm.Moth.transform.rotation;

        gm.mothBehaviour.OnReachedPosition += OnMothLands;

        // We should be able to move to a new location if the interactable has played
        // Black bars should not show if the interactable has been seen
        if (!currentInteractable.HasPlayed && !gm.hasReachedPointOfNoReturn)
        {
            gm.mothBehaviour.SetFragmentMode(true);
            gm.CinemaBars.gameObject.SetActive(true);
        }

        Vector3 newMothPos = currentInteractable.transform.TransformPoint(currentInteractable.LandingPosition);
        gm.mothBehaviour.SetMothPos(newMothPos);

        currentInteractableRenderer = currentInteractable.GetComponentInChildren<Renderer>();

        cameraController.SetFragmentMode(true);
        if (currentInteractable is Puzzle)
        {
            currentInteractable.GetComponent<BoxCollider>().enabled = false;
			AkSoundEngine.PostEvent("CAMERA_MOVE", gm.GameCamera);
        }
        else if(!gm.hasReachedPointOfNoReturn)
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
            keepMothLandingState = true;
            ForceLanding();
        }
    }

    private void ForceLanding()
    {
        AkSoundEngine.PostEvent("MOTH_LANDING", gm.Moth);
        gm.mothBehaviour.SetMothAnimationState("Landing");
    }

    private void EndOfFragmentCallback()
    {
        if(gm.GetGameState() != this)
        {
            return;
        }
        currentInteractable.InvokeInteractableCall();
        keepMothLandingState = false;
        gm.mothBehaviour.SetMothAnimationState("Flying");
        gm.StartCoroutine(Leaving(1f));
        lerpOut = true;
    }

    public override void OnStateExit()
    {
        gm.mothBehaviour.OnReachedPosition -= OnMothLands;
        gm.mothBehaviour.SetFragmentMode(false);
        if (currentInteractable is Puzzle)
        {
            currentInteractable.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private IEnumerator Leaving(float multiplier)
    {
        time = 0;
        Vector3 heading = Vector3.zero;

        Quaternion cameraStartRotation = gm.GameCamera.transform.rotation;

        while (time * gm.cameraToFragmentSpeed * multiplier < 1)
        {
            heading = gm.GameCamera.transform.position - gm.Moth.transform.position;
            heading = heading.ResizeMagnitude(gm.cameraController.InitialHeading.magnitude);

            Vector3 desiredPosition = heading + gm.Moth.transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(-heading.normalized);

            Quaternion mothStartRotation = gm.Moth.transform.rotation;

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
        if (gm.CinemaBars.gameObject.activeInHierarchy)
        {
            gm.CinemaBars.SetTrigger("Up");
        }

        if(gm.hasReachedPointOfNoReturn)
        {
            gm.SetState(new PointOfNoReturnState(gm)); 
        } 
        else 
        {
            gm.SetState(new RunState(gm));
        }
    }

    void CheckInput()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
        if (inputEvent.GameObject != null)
        {
            // Check if wall
            if (currentInteractable is Fragment && (inputEvent.GameObject.CompareTag("Wall") || inputEvent.GameObject.CompareTag("Ceiling")) 
                && inputEvent.InputType == InputType.TAP)
            {
                EndOfFragmentCallback();
                cameraController.SetFragmentMode(false);
                if(inputEvent.GameObject.CompareTag("Wall")){
                    gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit, true);
                } else {
                    gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit, false);
                }
                return;
            }
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