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
    }

    public void OnTouchDown(Touch finger)
    {
    }

    public void OnTouchUp(Touch finger)
    {
        print("On touchup");
        EventBus.Instance.SetMothPosition(transform.position + LandingPosition);
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position + LandingPosition, .1f);
    }


}