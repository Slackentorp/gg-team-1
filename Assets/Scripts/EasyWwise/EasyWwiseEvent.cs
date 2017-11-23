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
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
        }
    }

    void Start()
    {
        if (ChosenType == EasyWWiseEventType.Start)
        {
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
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