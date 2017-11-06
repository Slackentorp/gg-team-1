using System.Collections;
using System.Collections.Generic;
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

    [SerializeField, Tooltip("Allowed directions to move")]
    private DirectionsStruct Directions;
    
    private Vector3 distanceWorldPos;
    private Renderer cachedRenderer;
    private MeshFilter cachedMeshFilter;

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
        }
        distanceWorldPos = worldPos - transform.position;
    }

    public void OnTouchUp(Touch finger)
    {
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
            controller.CheckForSolution();
        }
        transform.position = newPosition;
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }

    [System.Serializable]
    private struct DirectionsStruct
    {
        public bool X;
        public bool Y;
        public bool Z;
    }
}
