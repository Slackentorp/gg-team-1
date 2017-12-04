using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseSwitch : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseSwitchGroupID;
    public string WwiseSwitchID;
    public bool PostOnlyAfterPointOfNoReturn;
    public GameObject ReferenceGameObject;

    void Awake()
    {
        if (ReferenceGameObject == null)
        {
            ReferenceGameObject = gameObject;
        }

        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.Awake)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void Start()
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.Start)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            AkSoundEngine.SetSwitch(WwiseSwitchGroupID, WwiseSwitchID, ReferenceGameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
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