using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseState : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseStateGroupID;
    public string WwiseStateID;

    void Awake()
    {
        if (ChosenType == EasyWWiseEventType.Awake)
        {
            print("Awake: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void Start()
    {
        if (ChosenType == EasyWWiseEventType.Start)
        {
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            print("On Trigger Enter: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            print("On Trigger Stay: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            print("On Trigger Exit: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            print("On Collision Enter: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            print("On Collision Stay: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
            print("On Trigger Exit: " + WwiseStateID);
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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