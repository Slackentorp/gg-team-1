using System.Collections;
using UnityEngine;

/// <summary>
/// This basic class defines a story fragment which is placed on objects in the scene.
/// Defines the position and orientation of the camera when interacting with it.
/// </summary>
//[RequireComponent(typeof(BoxCollider))]
[System.Serializable]
public class Fragment : Interactable
{
    public delegate void FragmentAction();
    public static event FragmentAction FragmentCall;

    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        base.Play(Callback);
        OnFragmentCall();
    }

    private void OnFragmentCall()
    {
        if (FragmentCall != null)
        {
            FragmentCall();
        }
    }

    public override void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        base.EndOfEventCallback(sender, callbackType, info);
        if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;

            if (interactableWwiseFrameCounter == 1)
            {
                eventDuration = i.fDuration;
            }
            interactableWwiseFrameCounter++;
        }
    }
}