using UnityEngine;

[CreateAssetMenu(fileName = "InputHandlerSettings",
    menuName = "Scriptable Objects/InputHandlerSettings", order = 1)]
public class InputHandlerSettings : ScriptableObject
{
    [Tooltip("The layer(s) the objects should be on to receive touch commands")]
    public LayerMask _TouchInputMask;

    [SerializeField,
     Tooltip(
         "The time in seconds between touch and release that constitutes to a tap")]
    public float _TapTimeThreshold = 1f;

    [Header("Swipe")]
    [Tooltip("The squared distance required to trigger a 'swipe' event")]
    public float _SwipeSquaredDistanceThreshold = 1f;

    [Tooltip(
         "Defines how 'straight' a swipe should be. The closer to 1, the more perfect the direction of the swipe must be"),
     Range(0, .99f)] public float _SwipeStraightness = 1f;

    [Tooltip(
        "Time in seconds for how quick the user has to be between touch and release to trigger a swipe")]
    public float _SwipeTimeThreshold = 1f;

    void OnValidate()
    {
        if (_TapTimeThreshold <= 0)
        {
            _TapTimeThreshold = 0;
        }
        if (_SwipeTimeThreshold <= 0)
        {
            _SwipeTimeThreshold = 0;
        }
    }
}