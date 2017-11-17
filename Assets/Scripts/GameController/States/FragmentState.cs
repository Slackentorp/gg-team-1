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

    private float distanceToFragment;

    public FragmentState(GameController gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        AkSoundEngine.PostEvent("CAMERA_MOVE", gm.GameCamera);
        gm.NextFragment.Play(EndOfFragmentCallback);
        cameraController = new CameraController(gm.GameCamera.transform, 2,1,1, gm.Moth.transform, true);
        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;
        originRotation = gm.GameCamera.transform.rotation;

        distanceToFragment = Vector3.SqrMagnitude(originPos - (gm.NextFragment.transform.position + gm.NextFragment.CamPosition));
    }

    private void EndOfFragmentCallback()
    {
        Debug.Log("End of " +gm.NextFragment.StoryFragment);
        gm.SetState(new RunState(gm));
    }

    public override void OnStateExit()
    {
        gm.NextFragment = null;
        gm.GameCamera.transform.position = originPos;
        gm.GameCamera.transform.forward = originForward;
    }

    public override void Tick()
    {
        
  //      float f = Vector3.SqrMagnitude(gm.GameCamera.transform.position - (gm.NextFragment.transform.position + gm.NextFragment.CamPosition));
        float t = gm.FragmentLerpCurve.Evaluate(time * speed);
        
        Vector3 position;
        position = Vector3.Lerp(originPos, gm.NextFragment.transform.position + gm.NextFragment.CamPosition, t);

        Vector3 forward;
        forward = Vector3.Lerp(originForward, gm.NextFragment.CamForward, t);

        Quaternion rotation;
        rotation = Quaternion.Lerp(originRotation, Quaternion.Euler(gm.NextFragment.CamOrientaion), t);

    if(t < 1){
        gm.GameCamera.transform.position = position;
        gm.GameCamera.transform.rotation = rotation;
        gm.GameCamera.transform.forward = forward;
        time += Time.deltaTime;
    } else {
        cameraController.Update();
    }
        
        
    }
}