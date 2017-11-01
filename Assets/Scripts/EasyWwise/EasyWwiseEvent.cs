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
            print(ChosenType);
        }
    }

    void Start()
    {
        if (ChosenType == EasyWWiseEventType.Start)
        {
            print(WwiseEventID);
            print(ChosenType);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerEnter)
        {
            print(ChosenType);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerStay)
        {
            print(ChosenType);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (ChosenType == EasyWWiseEventType.OnTriggerExit)
        {
            print(ChosenType);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionEnter)
        {
            print(ChosenType);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionStay)
        {
            print(ChosenType);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (ChosenType == EasyWWiseEventType.OnCollisionExit)
        {
            print(ChosenType);
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