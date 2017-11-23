using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableState : GameState
{
    private Vector3 originPos, originForward;
    private Quaternion originRotation;
    private float time;
    private float speed = 0.35f;

    bool lerpOut;

    private CameraController cameraController;
    private Interactable nextInteractable;
    public InteractableState(GameController gm, Interactable interactable) : base(gm)
    {
        nextInteractable = interactable;
    }

    public override void Tick()
    {
        if(lerpOut) return;
        float t = gm.FragmentLerpCurve.Evaluate(time * speed);

        Vector3 position;
        position = Vector3.Lerp(originPos, nextInteractable.transform.position + nextInteractable.CamPosition, t);

        Vector3 forward;
        forward = Vector3.Lerp(originForward, nextInteractable.CamForward, t);

        Quaternion rotation;
        rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(nextInteractable.CamOrientaion), t);

        if (t < 1)
        {
            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            time += Time.deltaTime;
        }
        else
        {
            cameraController.Update();
        }
    }

    public override void OnStateEnter()
    {
        nextInteractable.Play(EndOfFragmentCallback);
        cameraController = gm.cameraController;

        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;
        cameraController.SetFragmentMode(true);
    }

    private void EndOfFragmentCallback()
    {
        Debug.Log("End of interactable: " + nextInteractable.gameObject.name);
        gm.StartCoroutine(Leaving());
        lerpOut = true;
    }

    public override void OnStateExit()
    {
        AkSoundEngine.StopAll(nextInteractable.gameObject);
    }

    private IEnumerator Leaving()
    {
        time = 0;
        while (time * speed < 1)
        {
            float t = gm.FragmentLerpCurve.Evaluate(time * speed);
            Vector3 position;
            position = Vector3.Lerp(nextInteractable.transform.position + nextInteractable.CamPosition, originPos, t);

            Vector3 forward;
            forward = Vector3.Lerp(nextInteractable.CamForward, originForward, t);

            Quaternion rotation;
            rotation = Quaternion.Lerp(Quaternion.Euler(nextInteractable.CamOrientaion), originRotation, t);

            gm.GameCamera.transform.position = position;
            gm.GameCamera.transform.rotation = rotation;
            gm.GameCamera.transform.forward = forward;
            time += Time.deltaTime;
            yield return null;
        }

        gm.SetState(new RunState(gm));
    }
}