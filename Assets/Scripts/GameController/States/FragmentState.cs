using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentState : GameState
{
    private Vector3 originPos;
    private Vector3 originForward;

    public FragmentState(GameController gm) : base(gm)
    {
    }

    public override void OnStateEnter()
    {
        gm.NextFragment.Play(EndOfFragmentCallback);

        originPos = gm.GameCamera.transform.position;
        originForward = gm.GameCamera.transform.forward;

        gm.GameCamera.transform.position = gm.NextFragment.transform.position + gm.NextFragment.CamPosition;
        gm.GameCamera.transform.rotation = Quaternion.Euler(gm.NextFragment.CamOrientaion);
        gm.GameCamera.transform.forward = gm.NextFragment.CamForward;
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
    }
}