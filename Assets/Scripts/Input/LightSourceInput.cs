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
    [SerializeField]
    private bool isSwitchable = false;

    public Vector3 CameraPosition { get { return transform.TransformPoint(cameraPosition); } }

    public bool Lit
    {
        get { return IsLit; }
        set { IsLit = value; }
    }

    public bool ISSwitchable
    {
        get { return isSwitchable; }
        set { isSwitchable = value; }
    }


    public void OnTap()
    {
        if (IsLit)
        {
            EventBus.Instance.SetMothPosition(transform.TransformPoint(LandingPosition));
       //     GameController.Instance.SetCameraTarget(CameraPosition);
        }
    }

    public void OnTouchDown(Vector3 worldPos)
    {
    }

    public void OnTouchUp()
    {
    }

    public void OnToucHold(Vector3 worldPos)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(TouchDirection direction)
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