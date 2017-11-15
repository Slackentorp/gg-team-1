using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleModeBas : MonoBehaviour
{

	[SerializeField, Tooltip("The name of the puzzle.")]
	private string puzzleName;

	[SerializeField, Tooltip("Defines the position of the puzzle camera positon.")]
	private Vector3 camPosition;

	[SerializeField, Tooltip("Defines the position the puzzle camera should look.")]
	private Vector3 camOrientation;

	[SerializeField, Tooltip("Defines the plane/Gameobject that the puzzle is being solved on")]
	private GameObject puzzleSurface;

	//	public delegate void EasyWwiseCallback();
	public string puzzleID
	{
		get
		{
			return puzzleID;
		}
	}
	public Vector3 CamPosition
	{
		get
		{
			return camPosition;
		}
		set
		{
			camPosition = value;
		}
	}
	public Vector3 CamOrientaion
	{
		get
		{
			return camOrientation;
		}
		set
		{
			camOrientation = value;
		}
	}
	public Vector3 CamForward
	{
		get
		{
			return
				(transform.position + camOrientation) -
				(transform.position + camPosition);
		}
	}
	void Start()
	{

	}

	void Update()
	{

	}
}
