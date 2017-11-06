using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePuzzle : MonoBehaviour
{
    public string onSolvedWwiseEvent, onSolvedStorybitName;
    public bool isSolved;
    public abstract void CheckForSolution(Component sender);

    public virtual void OnBeginSolving()
    {
        
    }

    public virtual void OnSolved()
    {
        isSolved = true;
        if (!string.IsNullOrEmpty(onSolvedWwiseEvent))
        {
            AkSoundEngine.PostEvent(onSolvedWwiseEvent, gameObject);
        }
        if (!string.IsNullOrEmpty(onSolvedStorybitName))
        {
            // TODO: Implement story bit call
            print("Triggering storybit: " +onSolvedStorybitName);
        }

        print("Everything is correct");
    }

    [System.Serializable]
    public struct DirectionsStruct {
        public bool X;
        public bool Y;
        public bool Z;
    }
}
