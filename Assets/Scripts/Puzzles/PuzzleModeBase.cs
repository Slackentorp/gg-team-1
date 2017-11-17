using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleModeBase : MonoBehaviour
{
	[SerializeField, Tooltip("The name of the puzzle.")]
	private string puzzleName;

	[SerializeField, Tooltip("Defines the position of the puzzle camera positon.")]
	private Vector3 camPosition;

	[SerializeField, Tooltip("Defines the position the puzzle camera should look.")]
	private Vector3 camOrientation;

	[SerializeField, Tooltip("Defines the collider that the puzzle is being solved on")]
	private Collider puzzleCollider;

	[SerializeField, Tooltip("Defines the collider that the puzzle is being solved on")]
	private Vector3 sizeOfCollider;

	public delegate void EasyWwiseCallback();
	public string PuzzleID
	{
		get
		{
			return PuzzleID;
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
		//puzzleCollider = GetComponent<Collider>();
	}

	void Update()
	{


	}

	public delegate void PuzzleAction();
	public static event PuzzleAction PuzzleCall;

	private void Awake()
	{
		gameObject.layer = LayerMask.NameToLayer("Touch Object");
	}

	private bool isSolved;
	public bool IsSolved
	{
		get
		{
			return isSolved;
		}
		private set
		{
			isSolved = value;
		}
	}

	/*public void Play(EasyWwiseCallback Callback)
	{
		IsSolved = true;
		PuzzleCall();
		Debug.Log("Story fragment - " + storyFragment + " - ACTIVATE!");
		uint markerId = AkSoundEngine.PostEvent(storyFragment, gameObject,
						(uint)AkCallbackType.AK_EnableGetSourcePlayPosition, EndOfEventCallback, Callback);
		SubToolXML.Instance.InitSubs(markerId, storyFragment);
	}*/

	void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
	{
		var t = sender as EasyWwiseCallback;
		if (t != null)
		{
			t.Invoke();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position + camPosition, "CameraIcon.tif");
		Gizmos.DrawLine(transform.position + camPosition, transform.position + camOrientation);
		Gizmos.DrawWireCube(puzzleCollider.transform.position, sizeOfCollider);
	}
}
