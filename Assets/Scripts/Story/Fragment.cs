using UnityEngine;

/// <summary>
/// This basic class defines a story fragment which is placed on objects in the scene.
/// Defines the position and orientation of the camera when interacting with it.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class Fragment : MonoBehaviour
{
    [SerializeField, Tooltip("The name of the story fragment.")]
    private string storyFragment;

    [SerializeField, Tooltip("Defines the position of the fragment camera positon.")]
    private Vector3 camPosition;

    [SerializeField, Tooltip("Defines the position the fragment camera should look.")]
    private Vector3 camOrientation;

    public delegate void EasyWwiseCallback();
    public string StoryFragment
    {
        get { return storyFragment; }
    }
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

    
    public delegate void FragmentAction();
    public static event FragmentAction FragmentCall;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object");
    }

    private bool hasPlayed;
    public bool HasPlayed { get { return hasPlayed; } private set { hasPlayed = value; } }

    public void Play(EasyWwiseCallback Callback)
    {
        HasPlayed = true;
        //   FragmentCall(); // THIS NEEDS TO BE NOT COMMENTED IN THE REAL VERSION!!!!
        Debug.Log("Story fragment - " + storyFragment + " - ACTIVATE!");
        //uint markerId = AkSoundEngine.PostEvent(storyFragment, gameObject,  (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_EndOfEvent | (uint)AkCallbackType.AK_Duration, EndOfEventCallback , Callback);
        uint markerId = AkSoundEngine.PostEvent(storyFragment, gameObject,
                        (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                      | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        SubToolXML.Instance.InitSubs(markerId, storyFragment);
        EndFragment.Instance.EndFragments(markerId, storyFragment);
    }
    public int counter = 0;
    private float[] fragmentDurations = new float[2];
    public bool fragmentIsOn = false;
    [SerializeField]
     void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;
        //  AkDurationCallbackInfo durationInfo = callbackType;

        if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;
           // Debug.Log("That's the real duration:" + i.fEstimatedDuration);
            //Debug.Log("Duration brah: " + i.fDuration +" Playing ID: " + i.playingID + " Media ID: " +i.mediaID);
            
            fragmentDurations[counter] = i.fDuration;
             Debug.Log(fragmentDurations[counter]);

            if (counter == 1)
            {

                fragmentIsOn = true;
                EndFragment.Instance.Durations(fragmentDurations[1], fragmentIsOn);
                
            }

            counter++;
            if(counter==2)
            {
                counter = 0;
            }//LOGICAL PROBLEMS HERE maybe
        }
        else if (callbackType == AkCallbackType.AK_EndOfEvent)
        {
           // Debug.Log("End of event: " + callbackType);
            if (t != null)
            {
                t.Invoke();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + camPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + camPosition, transform.position + camOrientation);
    }
}
