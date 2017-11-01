using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseEvent : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseEventID;

    void Awake()
    {
        if (ChosenType == EasyWWiseEventType.Awake)
        {
            print("Awake: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
        }
    }

    void Start()
    {
        if (ChosenType == EasyWWiseEventType.Start)
        {
            print("Start: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            print("On Trigger Enter: " +WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            print("On Trigger Stay: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            print("On Trigger Exit: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            print("On Collision Enter: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            print("On Collision Stay: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
            print("On Trigger Exit: " + WwiseEventID);
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    public enum EasyWWiseEventType
    {
        None,
        Start,
        Awake,
        OnCollisionEnter,
        OnCollisionStay,
        OnCollisionExit,
        OnTriggerEnter,
        OnTriggerStay,
        OnTriggerExit
    }
}