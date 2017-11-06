using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TMPTutorialPuzzle : MonoBehaviour, ITouchInput 
{
    private bool solved = false;

    public void OnTap(Touch finger)
    {
        solved = true;
        GameController.Instance.SolveTutorial(); 
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {

    }

    public void OnTouchUp(Touch finger)
    {

    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {

    }

    public void OnTouchExit()
    {

    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }
}
