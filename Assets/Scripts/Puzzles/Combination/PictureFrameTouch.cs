using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class PictureFrameTouch : MonoBehaviour, ITouchInput
{
    [HideInInspector]
    public CombinationPuzzleController controller;
    [HideInInspector]
    public Vector3 originPosition;

    public Vector3 correctPostion, correctRotation;

    [SerializeField]
    private Color gizmoColor = Color.black;

    [SerializeField]
    private string pickupWwiseEvent, placeWwiseEvent;

    [SerializeField, Tooltip("Allowed directions to move")]
    private BasePuzzle.DirectionsStruct Directions;
    
    private Vector3 distanceWorldPos;
    private Renderer cachedRenderer;
    private MeshFilter cachedMeshFilter;
    private float originalRaise;

    private void Start()
    {
   //     originPosition = transform.position;
    }

    private void OnDrawGizmos()
    {
        if (cachedRenderer == null)
        {
            cachedRenderer = transform.GetComponent<Renderer>();
        }
        if (cachedMeshFilter == null)
        {
            cachedMeshFilter = transform.GetComponent<MeshFilter>();
        }
        if (originPosition == Vector3.zero)
        {
            originPosition = transform.position;
        }

        gizmoColor.a = Mathf.Clamp(gizmoColor.a, 0, .5f);
        Gizmos.color = gizmoColor;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(correctPostion + originPosition, Quaternion.Euler(correctRotation), transform.lossyScale);
        Gizmos.matrix *= rotationMatrix;

        Gizmos.DrawMesh(cachedMeshFilter.sharedMesh);
    }

    public void OnTap(Touch finger)
    {
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
        if (controller != null)
        {
            controller.OnBeginSolving();
            originalRaise = transform.position.y;
            transform.SetY(controller.RaiseAmount);
        }
        distanceWorldPos = worldPos - transform.position;
        PlayEvent(pickupWwiseEvent);
        
       
    }

    public void OnTouchUp(Touch finger)
    {
        PlayEvent(placeWwiseEvent);
        transform.SetY(originalRaise);
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {
        Vector3 newPosition = worldPos - distanceWorldPos;

        if (!Directions.X) {
            newPosition.x = transform.position.x;
        }
        if (!Directions.Y) {
            newPosition.y = transform.position.y;
        }
        if (!Directions.Z) {
            newPosition.z = transform.position.z;
        }
        
        if (controller != null)
        {
            controller.CheckForSolution(this);
            if(BoundsContain(controller.Bounds, controller.transform.position, newPosition))
            {
                transform.position = newPosition;
            }
        }
    }

    public void OnTouchExit()
    {
        PlayEvent(placeWwiseEvent);
        transform.SetY(originalRaise);
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    private void PlayEvent(string wwiseevent)
    {
        if (string.IsNullOrEmpty(wwiseevent))
        {
            AkSoundEngine.PostEvent(wwiseevent, gameObject);
        }
    }

    /// <summary>
    /// Checks whether the position is contained in bounds
    /// </summary>
    /// <param name="bounds">A Vector3 specifiying the bounds</param>
    /// <param name="center">A Vector3 specifiying the center of the bounds</param>
    /// <param name="position">A Vector3 position to check if is inside the bounds</param>
    /// <returns></returns>
    private bool BoundsContain(Vector3 bounds, Vector3 center, Vector3 position)
    {
        float halfX = bounds.x / 2;
        float halfZ = bounds.z / 2;
        if (position.x <= center.x + halfX && position.x >= center.x - halfX
            && position.z <= center.z + halfZ && position.z >= center.z - halfZ)
        {
            return true;
        }

        return false;
    }
}
