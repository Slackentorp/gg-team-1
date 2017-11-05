using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        CheckForSolution();
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

    public override void CheckForSolution()
    {
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
                pictureFrame.transform.position =
                    pictureFrame.correctPostion + pictureFrame.originPosition;
                pictureFrame.transform.rotation =
                    Quaternion.Euler(pictureFrame.correctRotation);
                Destroy(pictureFrame);
            }
        }
    }
}