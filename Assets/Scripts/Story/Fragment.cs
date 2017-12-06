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

    uint marker;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        if(string.IsNullOrEmpty(StoryFragment))
        {
            Debug.LogWarning("StoryFragment: \"" +StoryFragment +"\" does not exist");
            Callback();
            return;
        }

        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
                        (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                      | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        marker = markerId;
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        //EndFragments(markerId, StoryFragment);
        OnFragmentCall();
    }


    private void OnFragmentCall()
    {
        if (FragmentCall != null)
        {
            FragmentCall();
        }
    }
}
