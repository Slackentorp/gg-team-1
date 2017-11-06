using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotation : MonoBehaviour, ITouchInput
{
    private float rotationAmount;

    [SerializeField]
    private RotateOn RotateOnTouchType;


    public void OnTap(Touch finger)
    {
        if (RotateOnTouchType == RotateOn.TAP)
        {
        }
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
        if (RotateOnTouchType == RotateOn.TOUCH_DOWN)
        {
        }
    }

    public void OnTouchUp(Touch finger)
    {
        if (RotateOnTouchType == RotateOn.TOUCH_UP)
        {
        }
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {
        if (RotateOnTouchType == RotateOn.TOUCH_HOLD)
        {
        }
    }

    public void OnTouchExit()
    {
        if (RotateOnTouchType == RotateOn.TOUCH_EXIT)
        {
        }
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
        switch (RotateOnTouchType)
        {
            case RotateOn.TOUCH_SWIPE_UP:
                break;
            case RotateOn.TOUCH_SWIPE_RIGHT:
                break;
            case RotateOn.TOUCH_SWIPE_DOWN:
                break;
            case RotateOn.TOUCH_SWIPE_LEFT:
                break;
        }
    }

    private enum RotateOn
    {
        TAP,
        TOUCH_DOWN,
        TOUCH_UP,
        TOUCH_HOLD,
        TOUCH_EXIT,
        TOUCH_SWIPE_UP,
        TOUCH_SWIPE_RIGHT,
        TOUCH_SWIPE_DOWN,
        TOUCH_SWIPE_LEFT
    }
}