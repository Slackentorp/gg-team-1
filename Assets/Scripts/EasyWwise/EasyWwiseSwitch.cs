using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseSwitch : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseSwitchGroupID;
    public string WwiseSwitchID;
    public GameObject ReferenceGameObject;

    void Awake()
    {
        if (ReferenceGameObject == null)
        {
            ReferenceGameObject = gameObject;
        }

        if (ChosenType == EasyWWiseEventType.Awake)
        {
            print("Awake: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void Start()
    {
        if (ChosenType == EasyWWiseEventType.Start)
        {
            print("Start: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            print("On Trigger Enter: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            print("On Trigger Stay: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            print("On Trigger Exit: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            print("On Collision Enter: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            print("On Collision Stay: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
            print("On Trigger Exit: " + WwiseSwitchID);
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
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