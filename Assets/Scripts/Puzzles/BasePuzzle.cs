﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    public string onSolvedWwiseEvent, onSolvedStorybitName, onIncorrectPlacementWwiseEvent, onPickupWwiseEvent;
    public GameObject particles;
    public bool isSolved;
    public abstract void CheckForSolution(Component sender);

    public virtual void OnBeginSolving()
    {
        
    }

    public virtual void OnSolved()
    {
        isSolved = true;
        AkSoundEngine.PostEvent("PUZZLE_SOLVED", gameObject);
        if (!string.IsNullOrEmpty(onSolvedWwiseEvent))
        {
            AkSoundEngine.PostEvent(onSolvedWwiseEvent, gameObject);
        }
        if (!string.IsNullOrEmpty(onSolvedStorybitName))
        {
            // TODO: Implement story bit call
            print("Triggering storybit: " +onSolvedStorybitName);
        }

        if (particles != null)
        {
            particles.SetActive(true);
        }
        print("Everything is correct");
        CameraController.isMouseTouchingObject = false;
        CheckForSolution(null);
    }

    [System.Serializable]
    public struct DirectionsStruct {
        public bool X;
        public bool Y;
        public bool Z;
    }
}
