using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class RotationPuzzleController : Singleton<RotationPuzzleController>
{
    [SerializeField, Tooltip("The amount the picture frames will rotate with on tap")]
    private float rotationAmount;
    [SerializeField, Tooltip("The allowed deviation in degrees of the correct rotation")]
    private float slack;

    [SerializeField, Tooltip("Individual picture frames to include in the puzzle, and their correct up rotation")]
    private List<PictureframeRotation> pictureFrames;
    

    public float GetRotationAmount()
    {
        return rotationAmount;
    }

    public void CheckForSolution()
    {
        bool isCorrect = true;
        foreach (var go in pictureFrames)
        {
            if (go.pictureFrame.transform.rotation.eulerAngles.y <
                go.correctYRotation - slack - 0.1f ||
                go.pictureFrame.transform.rotation.eulerAngles.y >
                go.correctYRotation + slack + 0.1f)
            {
                isCorrect = false;
            }
        }

        if (isCorrect)
        {
            PuzzleSolved();
        }
    }

    private void PuzzleSolved()
    {
        Debug.Log("Puzzle solved");
    }

    [System.Serializable]
    private struct PictureframeRotation
    {
        public GameObject pictureFrame;
        public float correctYRotation;
    }
}
