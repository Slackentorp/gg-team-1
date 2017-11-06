using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

[ExecuteInEditMode]
public class CombinationPuzzleController : BasePuzzle
{
    [SerializeField,
     Tooltip(
         "Allowed distance squared between the solution and current position of the object, before it snaps into place")]
    private float mercySquaredDistance;

    [SerializeField,
     Tooltip(
         "Allowed angular difference between the solution rotation and current rotation of the object, before it snaps into place")]
    private float mercyRotation;

    [SerializeField]
    private string onCorrectWwiseEvent;
    [SerializeField, Range(0,1)]
    private float onCorrectBrightness = 1;

    private PictureFrameTouch[] pictureFrames;

    void OnEnable()
    {
        pictureFrames = GetComponentsInChildren<PictureFrameTouch>();
        foreach (var frame in pictureFrames)
        {
            frame.controller = this;
        }
    }

    void Start()
    {
        CheckForSolution(null);
    }

    private void Update()
    {
        if (isSolved)
        {
            return;
        }
        int enabledFrames = pictureFrames.Count(o => o != null);
        if (enabledFrames == 0)
        {
            print("Everything is correct");
            OnSolved();
        }
    }

    public override void CheckForSolution(Component sender)
    {
        //Safeguard because the script is in edit mode
        if (!Application.isPlaying)
        {
            return;
        }
        foreach (PictureFrameTouch pictureFrame in pictureFrames)
        {
            if (pictureFrame == null || !pictureFrame.enabled)
            {
                continue;
            }

            float distance = Vector3.SqrMagnitude(pictureFrame.transform.position -
                (pictureFrame.correctPostion + pictureFrame.originPosition));
            float angleDifference =
                Quaternion.Angle(pictureFrame.transform.rotation,
                    Quaternion.Euler(pictureFrame.correctRotation));

            if (distance <= mercySquaredDistance &&
                angleDifference <= mercyRotation)
            {

                if ( (sender == pictureFrame || sender == null) && !string.IsNullOrEmpty(onCorrectWwiseEvent))
                {
                    print("Setting correct");
                    AkSoundEngine.PostEvent(onCorrectWwiseEvent, pictureFrame.gameObject);
                    Renderer rend = pictureFrame.gameObject.GetComponent<Renderer>();
                    rend.material.color = rend.material.color.WithBrightness(onCorrectBrightness);
                }

                pictureFrame.transform.position =
                    pictureFrame.correctPostion + pictureFrame.originPosition;
                pictureFrame.transform.rotation =
                    Quaternion.Euler(pictureFrame.correctRotation);
                if (Application.isPlaying)
                {
                    Destroy(pictureFrame);
                }

            }
        }
    }
}