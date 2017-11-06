using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

/// <summary>
/// Touch input specifically for light sources
/// </summary>
public class LightSourceInput : MonoBehaviour, ITouchInput
{
    [SerializeField]
    private Vector3 LandingPosition;
    [SerializeField]
    private Vector3 cameraPosition; 
    [SerializeField]
    private bool IsLit = false;

    public bool Lit
    {
        get { return IsLit; }
        set { IsLit = value; }
    }


    public void OnTap(Touch finger)
    {
        if (IsLit)
        {
            EventBus.Instance.SetMothPosition(transform.TransformPoint(LandingPosition));
        }
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
    }

    public void OnTouchUp(Touch finger)
    {
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
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
        Gizmos.DrawIcon(transform.TransformPoint(LandingPosition), "MothIcon.tif", true);
        Gizmos.DrawIcon(transform.TransformPoint(cameraPosition), "CameraIcon.tif", true);
    }

    [System.Serializable]
    private struct GameObjectMaterialKVP
    {
        public GameObject Model;
        public Material LitMaterial;
    }

    public Vector3 GetLandingPos()
    {
        return LandingPosition;
    }
}