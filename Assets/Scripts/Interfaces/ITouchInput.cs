using UnityEngine;

public interface ITouchInput
{
    void OnTap(Touch finger);
    void OnTouchDown(Touch finger);
    void OnTouchUp(Touch finger);
    void OnToucHold(Touch finger);
    void OnTouchExit();
    void OnSwipe(Touch finger, TouchDirection direction);
}

public enum TouchDirection
{
    Up,
    Down,
    Left,
    Right
};