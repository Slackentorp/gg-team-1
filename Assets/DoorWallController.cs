using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorWallController : MonoBehaviour
{
    [SerializeField]
    private LightSourceInput[] roomLamps;
    [SerializeField, Tooltip("The name of the StoryEvent to call once all \"Room Lamps\" for this door is lit")]
    private string StoryEventName;

    private ParticleSystem doorParticleSystem;
    private int LampsON = 0;

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

        AkSoundEngine.SetState("LAMPS_ON", "LAMP_" + numActiveLamps);
        if(roomLamps.Length == 1 && numFullOnLamps == 1)
        {
            gameObject.SetActive(false);
            CallStoryEvent();
        }

        else if (numFullOnLamps >= 1 && numActiveLamps == roomLamps.Length)
        {
            gameObject.SetActive(false);
            CallStoryEvent();
        }
    }

    private void CallStoryEvent()
    {
        if(StoryEventName != null && StoryEventController.Instance != null)
        {
            StoryEventController.Instance.PostStoryEvent(StoryEventName);
        }
    }
}

