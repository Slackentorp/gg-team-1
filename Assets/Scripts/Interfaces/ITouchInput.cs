using UnityEngine;

public interface ITouchInput
{
    void OnTap(Touch finger);
    void OnTouchDown(Touch finger, Vector3 worldPos);
    void OnTouchUp(Touch finger);
    void OnToucHold(Touch finger, Vector3 worldPos);
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