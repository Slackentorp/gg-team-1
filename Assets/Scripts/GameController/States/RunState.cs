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

	private Fragment[] fragmentPositions;

    public RunState(GameController gm) : base(gm)
    {
    }

	public override void OnStateEnter()
	{
		cameraController = gm.cameraController;
		cameraController.SetFragmentMode(false);
		fragmentPositions = GameObject.FindObjectsOfType<Fragment>(); 
		gm.fragParticleController = new FragmentParticleController(fragmentPositions, gm.fragmentParticles, gm.Moth.transform);
		gm.fragParticleController.OnRunStart();
    }

    void CheckInput()
    {
        InputEvent inputEvent = gm.InputManager.CheckInput();
        if (inputEvent.GameObject != null)
        {
            if (inputEvent.GameObject.CompareTag("Wall") && inputEvent.InputType == InputType.TAP)
            {
                particlePos = inputEvent.RaycastHit.point + inputEvent.RaycastHit.normal * 0.07f;
                particleTapCall(particlePos);

                gm.mothBehaviour.SetMothPos(inputEvent.RaycastHit);

                //Debug.Log("Ey I'm flying'ere");
                return;
            }

            Interactable interactable = inputEvent.GameObject.GetComponent<Interactable>();
            if (interactable != null && inputEvent.InputType == InputType.TAP)
            {
                particlePos = inputEvent.RaycastHit.point + inputEvent.RaycastHit.normal * 0.07f;
                particleTapCall(particlePos);

                float dist = Vector3.SqrMagnitude(gm.Moth.transform.position - interactable.transform.position);
                if (Mathf.Abs(dist) < interactable.InternalInteractionDistion)
                {
                    gm.SetState(new InteractableState(gm, interactable));
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