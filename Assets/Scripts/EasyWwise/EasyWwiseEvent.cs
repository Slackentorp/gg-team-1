using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseEvent : MonoBehaviour
{
    public EasyWWiseEventType ChosenType;
    public string WwiseEventID;
    public bool PostOnlyAfterPointOfNoReturn;
    public bool PostOnlyOnce;

    private bool postedBefore;

    [SerializeField]
    private bool isPhoneTrigger;

    void Awake()
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.Awake)
        {
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING"); 
        }
    }

    void Start()
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.Start)
        {
            AkSoundEngine.PostEvent(WwiseEventID, gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            if (isPhoneTrigger)
                SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (!GameController.Instance.hasReachedPointOfNoReturn && PostOnlyAfterPointOfNoReturn)
        {
            return;
        }
        if (PostOnlyOnce && postedBefore)
        {
            return;
        }
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
            AkSoundEngine.PostEvent(WwiseEventID, other.gameObject);
            postedBefore = true;
            //SubToolXML.Instance.InitSubs("TRIGGEREDSOUND_ANSWERING");
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