using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableState : GameState
{
    public InteractableState(GameController gm) : base(gm) { }
    private CameraController cameraController;
    private Interactable interactable; 

    public override void Tick()
    {
    }

    public override void OnStateEnter() 
    {
        gm.NextInteractable.Play(EndOfFragmentCallback);
        cameraController = gm.cameraController;
        interactable = gm.NextInteractable; 
        interactable.Play(EndOfFragmentCallback);
    }

    private void EndOfFragmentCallback()
    {
        PrepStateExit(); 
    }

    public override void OnStateExit()
    {
        //base.OnStateExit();
        gm.NextInteractable = null; 
    }

    private void PrepStateExit()
    {
        gm.StartCoroutine(Leaving());
    }

    private IEnumerator Leaving()
    {
        yield return null;
        gm.SetState(new RunState(gm)); 
    }
}
