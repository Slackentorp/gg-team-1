using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBiteTrigger : MonoBehaviour, ITouchInput
{
    [SerializeField, Tooltip("The event which should be called.")]
    private string storyBiteName;
    [SerializeField, Tooltip("If the event requires fixed camera.")]
    private bool fixedCamera = false;
    [SerializeField, Tooltip("If the event requires fixed camera, where should it be?")]
    private Vector3 fixedCamPos; 

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    public void OnTap(Touch finger)
    {
        CallEvent(); 
    }

    public void OnTouchDown(Touch finger)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnToucHold(Touch finger)
    {
       
    }

    public void OnTouchUp(Touch finger)
    {
        CallEvent(); 
    }

    private void CallEvent()
    {
        AkSoundEngine.PostEvent(storyBiteName, this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + fixedCamPos, .1f);
    }
}
