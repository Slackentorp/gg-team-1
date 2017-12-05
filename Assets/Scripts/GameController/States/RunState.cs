using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class RunState : GameState
{
    private CameraController cameraController;
	private FragmentParticleController fragParticleController;
	private Fragment fragment;

    public delegate void ParticleTapCall(Vector3 position);
    public static event ParticleTapCall particleTapCall;

    private ParticleSystem tapParticle;
    private string particlePath;
    private Vector3 particlePos;

    public RunState(GameController gm) : base(gm)
    {
    }

	public override void OnStateEnter()
	{
		cameraController = gm.cameraController;
		cameraController.SetFragmentMode(false);
    }

    void CheckInput()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
        if (inputEvent.GameObject != null)
        {
            if ((inputEvent.GameObject.CompareTag("Wall") || inputEvent.GameObject.CompareTag("Ceiling")) && inputEvent.InputType == InputType.TAP)
            {
                particlePos = inputEvent.RaycastHit.point + inputEvent.RaycastHit.normal * 0.07f;
                particleTapCall(particlePos);

                if(inputEvent.GameObject.CompareTag("Wall")){
                    gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit, true);
                } else {
                    gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit, false);
                }

                return;
            }

            Interactable interactable = inputEvent.GameObject.GetComponent<Interactable>();
            if (interactable != null && inputEvent.InputType == InputType.TAP)
            {
                particlePos = inputEvent.RaycastHit.point + inputEvent.RaycastHit.normal * 0.07f;
                particleTapCall(particlePos);

                float dist = Vector3.SqrMagnitude(gm.Moth.transform.position - interactable.transform.position);
                if (Mathf.Abs(dist) < interactable.InternalInteractionDistance)
                {
                    gm.SetState(new InteractableState(gm, interactable));
                } else {
                    gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit, true);
                }
            }
            else
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
        }
    }

    public override void Tick()
    {
        CheckInput();
        cameraController.Update();
        gm.mothBehaviour.Update();
        gm.mothSounds.UpdateMothSounds();
        gm.HeadsetStateUIText.text = gm.InputManager.GetHeadsetState() ? "Headset plugged in" : "Headset not plugged in";
		gm.fragParticleController.Update();
	}

    public override void InternalOnGUI()
    {
        gm.InputManager.OnGUI();
    }
}