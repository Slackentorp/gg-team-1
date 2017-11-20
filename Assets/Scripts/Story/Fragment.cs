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

    [SerializeField, Tooltip("The name of the story fragment.")]
    private string storyFragment;

    private bool hasPlayed;

    public string StoryFragment { get { return storyFragment; } }
    public bool HasPlayed { get { return hasPlayed; } private set { hasPlayed = value; } }

    public override void Awake()
    {
        base.Awake();
    }

    void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;
        if (t != null)
        {
            t.Invoke();
        }
    }

    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        HasPlayed = true;
        FragmentCall();
        Debug.Log("Story fragment - " + storyFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(storyFragment, gameObject,
                        (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
        SubToolXML.Instance.InitSubs(markerId, storyFragment);
    }

}
