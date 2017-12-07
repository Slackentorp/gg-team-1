using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    [ReadOnly]
    public string uniqueGUID;

    public delegate void InteractableAction(Interactable sender, bool beingLoaded);
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
	private bool HashasPlayed;
	public bool HasHasPlayed
	{
		get
		{
			return HashasPlayed;
		}
		set
		{
			HashasPlayed = value;
		}
	}
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
        if (firstPuzzleCheck)
        {
            TUTInteractableCall(this);
        }
        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
                            (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                          | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        EndFragments(markerId, StoryFragment);

    }

// #Kiril
    public int counter = 0;
    private float[] fragmentDurations = new float[3];
    public bool fragmentIsOn = false;
    public uint markerr;
    public string storyFragmentt;
    public int realDuration;
    public float durationn;
    public bool fragmentIsOnn = false;
    public GameObject thePlayedFragment;
    int uPosition;

    public virtual void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;

        if (t != null && callbackType == AkCallbackType.AK_EndOfEvent)
        {
            HasPlayed = true;
            t.Invoke();
        } 
        else if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;
            fragmentDurations[counter] = i.fDuration;
            
                if (counter == 2)
                {
                    fragmentIsOn = true;
                    Durations(fragmentDurations[2], fragmentIsOn, gameObject);
                }

                counter++;

                if (counter == 3)
                {
                    fragmentIsOn = false;
                    counter = 0;
                }
            }

    }

     private int ClipDuration(int time)
    {
        return (int)(time) - 20;
    }

    private bool WwiseEventDoesntExist(string eventName)
    {
        return AkSoundEngine.PrepareEvent(PreparationType.Preparation_Load, new string[] { eventName }, 1) == AKRESULT.AK_IDNotFound;

    }
    public void EndFragments(uint marker, string storyFragment)
    {
        markerr = marker;
        storyFragmentt = storyFragment;

    }

    public void TwoSecondsBeforeEnd()
    {
        AkSoundEngine.GetSourcePlayPosition(markerr, out uPosition);
        uPosition = uPosition / 10;
       // Debug.Log(uPosition);
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");

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
    

    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object");
    }
    

    [ContextMenu("Generate GUID")]
    private void GenerateGUID()
    {
        if (string.IsNullOrEmpty(uniqueGUID))
        {
            Debug.Log("uniqueGUID is apparently null");
            uniqueGUID = System.Guid.NewGuid().ToString();
        }
    }
    
    public void InvokeInteractableCall(bool beingLoaded)
    {
        if (InteractableCall != null)
        {
            InteractableCall(this, beingLoaded);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + cameraPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + cameraPosition, transform.position + cameraOrientation);
        Gizmos.DrawIcon(transform.position + landingPosition, "MothIcon.tif", true);

        Gizmos.DrawSphere(transform.position + resetPosition, .05f);
        Gizmos.color = Color.blue;
        Vector3 rotatedVector = Quaternion.Euler(landingRotation) * Vector3.up;
        Gizmos.DrawLine(transform.position + landingPosition, transform.position + landingPosition + rotatedVector.ResizeMagnitude(.2f));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + landingPosition, transform.position + landingPosition - rotatedVector.ResizeMagnitude(.075f));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + landingPosition, transform.position + landingPosition + (Quaternion.Euler(landingRotation) * -Vector3.forward.ResizeMagnitude(.075f)));
    }
}