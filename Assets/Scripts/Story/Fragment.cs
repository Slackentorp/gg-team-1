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

    private int callBackCounter = 0;
    private bool firstEventFrame = false;

    uint marker;
    int clipLength;
    int uPosition;

    public override void Awake()
    {
        base.Awake();
    }

    private int ClipDuration(int time)
    {
        return (int)(time) - 20;
    }

    private bool WwiseEventDoesntExist(string eventName)
    {
        return AkSoundEngine.PrepareEvent(PreparationType.Preparation_Load, new string[] { eventName }, 1) == AKRESULT.AK_IDNotFound;

    }

    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        if (string.IsNullOrEmpty(StoryFragment))
        {
            Debug.LogWarning("StoryFragment: \"" + StoryFragment + "\" does not exist");
            Callback();
            return;
        }

        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
                            (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                          | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        marker = markerId;
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        EndFragments(markerId, StoryFragment);
        OnFragmentCall();
    }

    public override void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;
            fragmentDurations[counter] = i.fDuration;

            if (counter == 1)
            {
                fragmentIsOn = true;
                Durations(fragmentDurations[1], fragmentIsOn, gameObject);
            }

            counter++;

            if (counter == 2)
            {
                fragmentIsOn = false;
                counter = 0;
            }
        }
        base.EndOfEventCallback(sender, callbackType, info);

    }

    public int counter = 0;
    private float[] fragmentDurations = new float[2];
    public bool fragmentIsOn = false;
    public uint markerr;
    public string storyFragmentt;
    public int realDuration;
    public float durationn;
    public bool fragmentIsOnn = false;
    public GameObject thePlayedFragment;
    //int uPosition;

    public void EndFragments(uint marker, string storyFragment)
    {
        markerr = marker;
        storyFragmentt = storyFragment;

    }

    public void TwoSecondsBeforeEnd()
    {
        AkSoundEngine.GetSourcePlayPosition(markerr, out uPosition);
        uPosition = uPosition / 10;
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");
        //Debug.Log(uPosition);
        if (fragmentIsOnn)
        {
            if (uPosition > realDuration)
            {
                Debug.Log("we are two seconds before the shit is done");
                AkSoundEngine.PostEvent("FRAGMENT_END", thePlayedFragment);
                fragmentIsOnn = false;
                return;
            }
        }
    }

    public void Durations(float duration, bool fragmentIsOn, GameObject playedFragment)
    {
        thePlayedFragment = playedFragment;
        fragmentIsOnn = fragmentIsOn;
        durationn = duration / 10;
        realDuration = (int)durationn;
        realDuration = realDuration - 20;

       // Debug.Log(realDuration);
    }


    void Update()//maybe a problem
    {
        if (fragmentIsOnn == true)
        {
            TwoSecondsBeforeEnd();
        }
    }

    private void OnFragmentCall()
    {
        if (FragmentCall != null)
        {
            FragmentCall();
        }
    }
}