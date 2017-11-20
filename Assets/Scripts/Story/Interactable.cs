using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    public delegate void InteractableAction();
    public static event InteractableAction InteractableCall; 

    [SerializeField, Tooltip("Defines the fixed position for the camera.")]
    private Vector3 cameraPosition;
    [SerializeField, Tooltip("Defines the fixed orientation for the camera.")]
    private Vector3 cameraOrientation;

    public delegate void EasyWwiseCallback();

    public abstract void Play(EasyWwiseCallback Callback); 

    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Touch Object"); 
    }


    public Vector3 CamPosition { get { return cameraPosition; } set { cameraPosition = value; } }
    public Vector3 CamOrientaion { get { return cameraOrientation; } set { cameraOrientation = value; } }
    public Vector3 CamForward
    {
        get
        {
            return
                (transform.position + cameraOrientation) -
                (transform.position + cameraPosition);
        }
    }

    private void InvokeInteractableCall()
    {
        if (InteractableCall != null)
        {
            InteractableCall(); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + cameraPosition, "CameraIcon.tif");
        Gizmos.DrawLine(transform.position + cameraPosition, transform.position + cameraOrientation);
    }
}
