using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

//[ExecuteInEditMode]
public class PictureFrameTouch : MonoBehaviour, ITouchInput
{
    public delegate void TouchUpDelegate();
    public event TouchUpDelegate OnTouchUpEvent;

    [HideInInspector]
    public bool isCorrect;

    public Vector3 correctPostion, correctRotation;

    [SerializeField]
    private string pickupWwiseEvent, placeWwiseEvent;
    [SerializeField]
    private Puzzle parentPuzzle;

    [SerializeField, Tooltip("Allowed directions to move")]
    private BasePuzzle.DirectionsStruct Directions;

    private Vector3 distanceWorldPos;
    private float startY;

    private void Start()
    {
        startY = transform.position.y;
    }

    public void OnTap()
    {
    }

    public void OnTouchDown(Vector3 worldPos)
    {
        if (isCorrect)
            return;

        distanceWorldPos = worldPos - transform.position;
        PlayEvent(pickupWwiseEvent);
    }

    public void OnTouchUp()
    {
        if (isCorrect)
            return;

        PlayEvent(placeWwiseEvent);
        if(OnTouchUpEvent != null)
        {
            OnTouchUpEvent();
        }
    }

    public void OnToucHold(Vector3 worldPos)
    {
        if (isCorrect)
            return;

        Vector3 newPosition = worldPos - distanceWorldPos;

        if (!Directions.X)
        {
            newPosition.x = transform.position.x;
        }
        if (!Directions.Y)
        {
            newPosition.y = transform.position.y;
        }
        if (!Directions.Z)
        {
            newPosition.z = transform.position.z;
        }

        Vector3 boundingCenter = parentPuzzle.transform.position + parentPuzzle.BoundingBoxOffset();
        Vector3 boundingSize = parentPuzzle.BoundingBoxSize();
        
        if(newPosition.x >= boundingCenter.x - boundingSize.x/2 &&
           newPosition.x <= boundingCenter.x + boundingSize.x/2 &&
           newPosition.y >= boundingCenter.y - boundingSize.y/2 &&
           newPosition.y <= boundingCenter.y + boundingSize.y/2 &&
           newPosition.z >= boundingCenter.z - boundingSize.z/2 &&
           newPosition.z <= boundingCenter.z + boundingSize.z/2)
        {
            transform.position = newPosition;
        }
    }

    public void OnTouchExit()
    {
        if (isCorrect)
            return;

        PlayEvent(placeWwiseEvent);
        transform.SetY(startY);
        if(OnTouchUpEvent != null)
        {
            OnTouchUpEvent();
        }
    }

    public void OnSwipe(TouchDirection direction)
    {
    }

    private void PlayEvent(string wwiseevent)
    {
        if (!string.IsNullOrEmpty(wwiseevent))
        {
            AkSoundEngine.PostEvent(wwiseevent, gameObject);
        }
    }
}