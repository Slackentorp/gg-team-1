using UnityEngine;

public interface ITouchInput
{
    void OnTap();
    void OnTouchDown(Vector3 worldPos);
    void OnTouchUp();
    void OnToucHold(Vector3 worldPos);
    void OnTouchExit();
    void OnSwipe(TouchDirection direction);
}

public enum TouchDirection
{
    Up,
    Down,
    Left,
    Right
};