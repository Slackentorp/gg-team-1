using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[System.Serializable]
public abstract class Interactable : MonoBehaviour
{
    public delegate void InteractableAction(Interactable sender);
    public static event InteractableAction InteractableCall;
    public delegate void TUTInteractableAction(Interactable sender);
    public static event TUTInteractableAction TUTInteractableCall;
    public delegate void EasyWwiseCallback();
    public float InteractionDistance { get { return interactionDistance; } }
    public Vector3 CamPosition { get { return cameraPosition; } set { cameraPosition = value; } }
    public Vector3 CamOrientaion { get { return cameraOrientation; } set { cameraOrientation = value; } }
    public Vector3 CamForward
    {
        get
        {
            return (transform.position + cameraOrientation) -
                (transform.position + cameraPosition);
        }
    }
    public float InternalInteractionDistance { get { return Mathf.Sqrt(interactionDistance); } }
    public bool HasPlayed { get { return hasPlayed; } set { hasPlayed = value; } }
    public string StoryFragment { get { return storyFragment; } }
    public Vector3 LandingPosition { get { return landingPosition; } }
    public Vector3 MothResetPosition { get { return resetPosition; } }

    [SerializeField]
    public bool firstPuzzleCheck;

    [SerializeField, Tooltip("The maximum distance of interaction")]
    private float interactionDistance = 2f;

    [SerializeField, Tooltip("Defines the fixed position for the camera.")]
    private Vector3 cameraPosition;
    [SerializeField, Tooltip("Defines the fixed orientation for the camera.")]
    private Vector3 cameraOrientation;
    [SerializeField, Tooltip("Where the moth should land")]
    private Vector3 landingPosition;
    [SerializeField, Tooltip("Should the moth land vertically or horizontally")]
    private Vector3 landingRotation;
    [SerializeField, Tooltip("Where the moth should go to after the interactable")]
    private Vector3 resetPosition;
    public Vector3 LandingRotation { get { return landingRotation; } }

    [SerializeField, Tooltip("The name of the story fragment")]
    private string storyFragment;

    [SerializeField]
    private bool hasPlayed;

    public virtual void Play(Interactable.EasyWwiseCallback Callback)
    {
        if (string.IsNullOrEmpty(StoryFragment))
        {
            Debug.LogWarning("StoryFragment: \"" + StoryFragment + "\" does not exist");
            Callback();
            return;
        }
        TUTInteractableCall(this);
        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject, (uint) AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        EndFragments(markerId, StoryFragment);
        OnFragmentCall();
    }

    public virtual void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
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
        EndOfEventCallback(sender, callbackType, info);

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
    int uPosition;

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

        if (fragmentIsOnn)
        {
            if (uPosition > realDuration)
            {

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

        //Debug.Log(realDuration);
    }

    void Update()
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



    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object");
    }

    public void InvokeInteractableCall()
    {
        if (InteractableCall != null)
        {
            InteractableCall(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + cameraPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + cameraPosition, transform.position + cameraOrientation);
        Gizmos.DrawIcon(transform.TransformPoint(landingPosition), "MothIcon.tif", true);

        Gizmos.DrawSphere(transform.TransformPoint(resetPosition), .05f);
        Gizmos.color = Color.blue;
        Vector3 rotatedVector = Quaternion.Euler(landingRotation) * transform.up;
        Gizmos.DrawLine(transform.TransformPoint(landingPosition), transform.TransformPoint(landingPosition) + rotatedVector.ResizeMagnitude(.2f));
    }
}