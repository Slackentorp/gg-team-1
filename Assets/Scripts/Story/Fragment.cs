using UnityEngine;

/// <summary>
/// This basic class defines a story fragment which is placed on objects in the scene.
/// Defines the position and orientation of the camera when interacting with it.
/// </summary>
public class Fragment : MonoBehaviour
{
    [SerializeField]
    private string storyFragment;

    [SerializeField]
    private Vector3 camPosition;

    [SerializeField]
    private Vector3 camOrientation; 

    private bool hasPlayed = false;
    public bool HasPlayed { get { return hasPlayed; } private set { hasPlayed = value; } }

    public void Play()
    {
        HasPlayed = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position + camPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + camPosition, transform.position + camOrientation);
    }
}
