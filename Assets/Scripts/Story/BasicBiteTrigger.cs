﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBiteTrigger : MonoBehaviour, ITouchInput
{
    [SerializeField, Tooltip("The event which should be called.")]
    private string fragmentName;
    [SerializeField, Tooltip("If the event requires fixed camera.")]
    private bool fixedCamera = false;
    [SerializeField, Tooltip("If the event requires fixed camera, where should it be?")]
    private Vector3 fixedCamPos;

    public bool FixedCamera { get { return fixedCamera; } }
    public Vector3 FixedCamPos { get { return fixedCamPos; } }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    public void OnTap(Touch finger)
    {
        CallEvent();
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {

    }

    public void OnTouchUp(Touch finger)
    {
    }

    private void CallEvent()
    {
        print("Plays bite: " + fragmentName);
        EventBus.Instance.TriggerStoryBite(this); 
        AkSoundEngine.PostEvent(fragmentName, this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + fixedCamPos, .05f);
    }
}
