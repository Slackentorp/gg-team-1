using System.Collections;
using UnityEngine;

/// <summary>
/// This basic class defines a story fragment which is placed on objects in the scene.
/// Defines the position and orientation of the camera when interacting with it.
/// </summary>
//[RequireComponent(typeof(BoxCollider))]
public class Fragment : Interactable
{
    public delegate void FragmentAction();
    public static event FragmentAction FragmentCall;

    private int callBackCounter = 0;
    private bool firstEventFrame = false;

    uint marker;
    int clipLength;
    int uPosition;

    public override void Awake()
    {
        base.Awake();
    }

    public override void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;

        if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;
            //Debug.Log("Hello " + marker);

            if (!firstEventFrame)
            {
                firstEventFrame = true;
            }
            else
            {
                clipLength = (int)(i.fDuration / 10) - 20;
                StartCoroutine(DurationCallBack(marker));
            }
        }
        else if (t != null)
        {
            base.EndOfEventCallback(sender, callbackType, info);
        }
    }

    IEnumerator DurationCallBack(uint type)
    {
        bool playing = true;
        while (playing)
        {
            //Debug.Log("HEJSA " + type);
            if (TwoSecondsBeforeEnd(type) || type == 0)
            {
                playing = true;
                //AkSoundEngine.PostEvent("FRAGMENT_END", gameObject);
            }
            yield return null;
        }

        firstEventFrame = false;
        AkSoundEngine.PostEvent("FRAGMENT_END", gameObject);
    }
   
    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        if(string.IsNullOrEmpty(StoryFragment))
        {
            Debug.LogWarning("StoryFragment: \"" +StoryFragment +"\" does not exist");
            Callback();
            return;
        }

        HasPlayed = true;
        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
                        (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                      | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        marker = markerId;
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        OnFragmentCall();
    }

    private void OnFragmentCall()
    {
        if (FragmentCall != null)
        {
            FragmentCall();
        }
    }

    private void EndFragment(uint marker, string storyFragment)
    {
        if (TwoSecondsBeforeEnd(marker))
        {
            AkSoundEngine.PostEvent("FRAGMENT_END", gameObject);
        }
    }

    private bool TwoSecondsBeforeEnd(uint marker)
    {
        AkSoundEngine.GetSourcePlayPosition(marker, out uPosition);
        uPosition = uPosition / 10;

        //Debug.Log("Time " + uPosition);
        if (uPosition > clipLength)
        {
            return true;
        }

        return false;
    }

    private int ClipDuration(int time)
    {
        return (int)(time) - 20;
    }

    private bool WwiseEventDoesntExist(string eventName)
    {
        return AkSoundEngine.PrepareEvent(PreparationType.Preparation_Load, new string[]{eventName}, 1) == AKRESULT.AK_IDNotFound;

    }
}
