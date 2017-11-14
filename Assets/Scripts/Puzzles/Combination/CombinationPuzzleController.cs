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

    [SerializeField, Tooltip("Specify the bounds of the puzzle. This is the area the pieces can be dragged within")]
    private Vector3 bounds;

    [SerializeField, Tooltip("The distance picture frames should be raised when picked up")]
    private float raiseAmount = .15f;
    public float RaiseAmount { get { return raiseAmount; } }

    public Vector3 Bounds { get { return bounds; }}

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

        CheckForSolution(null);
        int enabledFrames = pictureFrames.Count(o => !o.isCorrect);

        int correctFrames = pictureFrames.Length - enabledFrames;
        //  AkSoundEngine.SetState("PICTUREPUZZLE_STATE", "PIECE_" + correctFrames);

        if (enabledFrames == 0)
        {
            print("Everything is correct");
            CheckForSolution(null);
            OnSolved();
        }
    }

    /// <summary>
    /// Loops through all picture pieces, and destroys the touch component on pieces where the position is correct
    /// </summary>
    /// <param name="sender"></param>
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
                    Quaternion.Euler(pictureFrame.correctRotation) * transform.rotation);

            if (distance <= mercySquaredDistance &&
                angleDifference <= mercyRotation)
            {

                if ( (sender == pictureFrame || sender == null) && !string.IsNullOrEmpty(onCorrectWwiseEvent) && !pictureFrame.isCorrect)
                {
                    print("Setting correct");
                    AkSoundEngine.PostEvent(onCorrectWwiseEvent, pictureFrame.gameObject);
                    Renderer rend = pictureFrame.gameObject.GetComponent<Renderer>();
                    rend.material.color = rend.material.color.WithBrightness(onCorrectBrightness);
                    pictureFrame.isCorrect = true;
                }

                pictureFrame.transform.position =
                    pictureFrame.correctPostion + pictureFrame.originPosition;
                pictureFrame.transform.rotation =
                    Quaternion.Euler(pictureFrame.correctRotation) * transform.rotation;
                if (Application.isPlaying)
                {
                    Destroy(pictureFrame.GetComponent<BoxCollider>());
                  //  Destroy(pictureFrame);
                }

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, transform.rotation, Vector3.one);
        Gizmos.matrix *= rotationMatrix;
        Gizmos.DrawWireCube(transform.position, bounds);    
    }
    
}