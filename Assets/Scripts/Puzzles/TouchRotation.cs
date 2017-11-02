using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotation : MonoBehaviour, ITouchInput
{
    private RotationPuzzleController puzzleController;
    private float rotationAmount;

	// Use this for initialization
	void Start ()
	{
	    puzzleController = RotationPuzzleController.Instance;

	    if (puzzleController == null)
	    {
	        print("No puzzlecontroller found in scene");
	    }

        rotationAmount = puzzleController.GetRotationAmount();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTap(Touch finger)
    {
        transform.rotation *= Quaternion.AngleAxis(rotationAmount, transform.up);
        puzzleController.CheckForSolution();
    }

    public void OnTouchDown(Touch finger)
    {
    }

    public void OnTouchUp(Touch finger)
    {
    }

    public void OnToucHold(Touch finger)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
    }
}
