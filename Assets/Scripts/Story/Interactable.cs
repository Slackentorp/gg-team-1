using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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
    public float InternalInteractionDistion { get { return Mathf.Sqrt(interactionDistance); } }
    public bool HasPlayed { get { return hasPlayed; } set { hasPlayed = value; } }
    public string StoryFragment { get { return storyFragment; } }
    public Vector3 LandingPosition { get { return landingPosition; } }

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
    private LandRotation landingRotation;
    public LandRotation LandingRotation { get { return landingRotation; } }

    [SerializeField, Tooltip("The name of the story fragment")]
    private string storyFragment;

    public enum LandRotation{
        HORIZONTAL,
        VERTICAL
    }

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
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
            (uint) AkCallbackType.AK_EnableGetSourcePlayPosition |
            (uint) AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
    }

    public virtual void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;

        if (t != null && callbackType == AkCallbackType.AK_EndOfEvent)
        {
            HasPlayed = true;
            t.Invoke();
            InvokeInteractableCall();
        }

    }

    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object");
    }

    private void InvokeInteractableCall()
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
    }
}