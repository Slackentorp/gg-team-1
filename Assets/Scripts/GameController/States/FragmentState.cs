using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentState : GameState
{
    private Vector3 originPos;
    private Vector3 originForward;
    private Quaternion originRotation;
    private CameraController cameraController;

    private float time;
    private float speed = 0.35f;
    private bool exiting;

    private float distanceToFragment;
    private Vector3 currentVelocity;

    public FragmentState(GameController gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        //    AkSoundEngine.PostEvent("CAMERA_MOVE", gm.GameCamera);
        gm.NextFragment.Play(EndOfFragmentCallback);
        cameraController = new CameraController(gm.GameCamera.transform, 2, gm.cameraHeading, 1, 1, gm.Moth.transform, true, gm.cameraDamping);
        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;

        distanceToFragment = Vector3.SqrMagnitude(originPos - (gm.NextFragment.transform.position + gm.NextFragment.CamPosition));
    }

    private void EndOfFragmentCallback()
    {
        Debug.Log("End of " + gm.NextFragment.StoryFragment);

        Vector3 heading = gm.GameCamera.transform.position - gm.Moth.transform.position;
        heading = heading.normalized * gm.cameraHeading.magnitude;
        gm.cameraHeading = heading;

        exiting = true;
    }

    public override void OnStateExit()
    {
        gm.NextFragment = null;
    }

    public override void Tick()
    {
        if (!exiting)
        {
            float t = gm.FragmentLerpCurve.Evaluate(time * speed);

            Vector3 position;
            position = Vector3.Lerp(originPos, gm.NextFragment.transform.position + gm.NextFragment.CamPosition, t);

            Vector3 forward;
            forward = Vector3.Lerp(originForward, gm.NextFragment.CamForward, t);

            Quaternion rotation;
            rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(gm.NextFragment.CamOrientaion), t);

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
        else
        {
            // Lerp out of Fragment State
            Vector3 nextPos = Vector3.SmoothDamp(gm.GameCamera.transform.position, gm.cameraHeading + gm.Moth.transform.position, ref currentVelocity, 1);
            
            gm.GameCamera.transform.position = nextPos;
            gm.GameCamera.transform.rotation = Quaternion.RotateTowards(gm.GameCamera.transform.rotation, Quaternion.LookRotation(-gm.cameraHeading.normalized), .2f);
            
            if (gm.GameCamera.transform.position == gm.cameraHeading + gm.Moth.transform.position &&
            gm.GameCamera.transform.rotation == Quaternion.LookRotation(-gm.cameraHeading.normalized))
            {
                gm.SetState(new RunState(gm));
            }
        }

        gm.mothBehaviour.Update();
        gm.mothSounds.UpdateMothSounds();
    }
}