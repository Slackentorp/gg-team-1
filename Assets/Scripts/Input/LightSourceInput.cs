using Gamelogic.Extensions;
using UnityEngine;

/// <summary>
/// Touch input specifically for light sources
/// </summary>
public class LightSourceInput : MonoBehaviour, ITouchInput
{
    [SerializeField]
    private Vector3 LandingPosition;

    public void OnTap(Touch finger)
    {
        EventBus.Instance.SetMothPosition(transform.TransformPoint(LandingPosition));
    }

    public void OnTouchDown(Touch finger)
    {
    }

    public void OnTouchUp(Touch finger)
    {        
    }

    public void OnToucHold(Touch finger)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.TransformPoint(LandingPosition), .05f);
    }


}