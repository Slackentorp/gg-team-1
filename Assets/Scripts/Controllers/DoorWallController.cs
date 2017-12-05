using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorWallController : MonoBehaviour
{
    [SerializeField]
    private LightSourceInput[] roomLamps;
    [SerializeField, Tooltip("The name of the StoryEvent to call once all \"Room Lamps\" for this door is lit")]
    private string StoryEventName;

    private ParticleSystem doorParticleSystem;
    private int LampsON = 0;
    [SerializeField]
    private int roomIndex;

    public int GetRoomIndex()
    {
        return roomIndex;
    }
    private void Start()
    {
        AkSoundEngine.PostEvent("FOGWALL_ENABLE", gameObject);
    }

    void OnEnable()
    {
        LightSourceInput.LightSourceCall += LampChecker;
    }

    void OnDisable()
    {
        LightSourceInput.LightSourceCall -= LampChecker;
    }

    void LampChecker()
    {
        int numActiveLamps = roomLamps.Count(l => l.LampActivated);
        int numFullOnLamps = roomLamps.Count(l => l.LampFullOn);

        AkSoundEngine.SetState("LAMPS_ON_" + roomIndex, "LAMP_" + numFullOnLamps);
        if (roomLamps.Length == 1 && numFullOnLamps == 1)
        {
            CallStoryEvent();
        }

        else if (numFullOnLamps >= roomLamps.Length && numActiveLamps == roomLamps.Length)
        {
            CallStoryEvent();
        }
    }

    private void CallStoryEvent()
    {
        if (string.IsNullOrEmpty(StoryEventName))
        {
            EndOfEvent();
            return;
        }
        if (StoryEventName != null && StoryEventController.Instance != null)
        {
            StoryEventController.Instance.PostStoryEvent(StoryEventName, EndOfEvent);
        }
    }

    private void EndOfEvent()
    {
        gameObject.SetActive(false);
        AkSoundEngine.PostEvent("FOGWALL_DISABLE", gameObject);
    }
}
