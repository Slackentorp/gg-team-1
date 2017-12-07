using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using UnityEngine;

public class DoorWallController : MonoBehaviour
{
    [SerializeField]
    private LightSourceInput[] roomLamps;
    [SerializeField, Tooltip("The name of the StoryEvent to call once all \"Room Lamps\" for this door is lit")]
    private string StoryEventName;

    private ParticleSystem doorParticleSystem;
    [SerializeField]
    private int roomIndex;
    [SerializeField, Tooltip("Optional: Identifier for this fogwall")]
    public string fogIdentifier;

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

    [Button]
    public void Refresh()
    {
        LampChecker(true);
    }

    void LampChecker(bool beingLoaded)
    {
        
        int numActiveLamps = roomLamps.Count(l => l.LampActivated);
        int numFullOnLamps = roomLamps.Count(l => l.LampFullOn);

      //  print(gameObject.name +" recevied bool Lamps active: " +numActiveLamps +" fullon: " +numFullOnLamps);

        AkSoundEngine.SetState("LAMPS_ON_" + roomIndex, "LAMP_" + numFullOnLamps);
        if (roomLamps.Length == 1 && numFullOnLamps == 1)
        {
            if(beingLoaded)
            {
                EndOfEvent();
                return;
            } else {    
                CallStoryEvent();
            }
        }

        else if (numFullOnLamps >= roomLamps.Length && numActiveLamps == roomLamps.Length)
        {
            if(beingLoaded)
            {
                EndOfEvent();
                return;
            } else {    
                CallStoryEvent();
            }
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
//        print("Door EOE called");
        gameObject.SetActive(false);
        AkSoundEngine.PostEvent("FOGWALL_DISABLE", gameObject);
    }
}
