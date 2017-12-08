using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseState : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseStateGroupID;
    public string WwiseStateID;
    public bool PostOnlyAfterPointOfNoReturn;
    
    void Awake()
    {
        if(!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.Awake)
        {
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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
            AkSoundEngine.SetState(WwiseStateGroupID, WwiseStateID);
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