using UnityEngine;

/// <summary>
/// This basic class defines a story fragment which is placed on objects in the scene.
/// Defines the position and orientation of the camera when interacting with it.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Fragment : MonoBehaviour, ITouchInput
{
    [SerializeField, Tooltip("The name of the story fragment.")]
    private string storyFragment;

    [SerializeField, Tooltip("Defines the position of the fragment camera positon.")]
    private Vector3 camPosition;

    [SerializeField, Tooltip("Defines the position the fragment camera should look.")]
    private Vector3 camOrientation;

    public Vector3 CamPosition { get { return camPosition; } set { camPosition = value; } }
    public Vector3 CamOrientaion { get { return camOrientation; } set { camOrientation = value; } }
    public Vector3 CamForward
    {
        get
        {
            return
                (transform.position + camOrientation) -
                (transform.position + camPosition);
        }
    }

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object");
    }

    private bool hasPlayed = false;
    public bool HasPlayed { get { return hasPlayed; } private set { hasPlayed = value; } }

    public void Play()
    {
        HasPlayed = true;
        Debug.Log("Story fragment - " + storyFragment + " - ACTIVATE!");
        AkSoundEngine.PostEvent(storyFragment, this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position + camPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + camPosition, transform.position + camOrientation);
    }


    public void OnTap()
    {
        Play(); 
    }

    public void OnTouchDown(Vector3 worldPos)
    {
    }

    public void OnTouchUp()
    {
    }

    public void OnToucHold(Vector3 worldPos)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(TouchDirection direction)
    {
    }
}
