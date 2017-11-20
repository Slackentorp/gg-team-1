using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CombinationPuzzleController))]
public class Puzzle : Interactable
{
    public delegate void EasyWwiseCallback();
    public delegate void PuzzleAction();
    public static event PuzzleAction PuzzleCall;

    [SerializeField, Tooltip("The mercy distance")]
    private float mercyDistance;
    [SerializeField]
    private Vector3 boundingBoxSize = Vector3.one;
    [SerializeField]
    private Vector3 boundingBoxOffset;

    [SerializeField, Tooltip("The name of the puzzle.")]
    private string puzzleId;

    private GameObject[] puzzlePieces;
    private Vector3[] correctPositions; 

    private bool isSolved;


    public bool IsSolved { get { return isSolved; } private set { isSolved = value; } }
    public string PuzzleId { get { return PuzzleId; } }


    public override void Awake()
    {
        base.Awake();
        puzzlePieces = transform.GetComponentsInChildren<GameObject>();
        correctPositions = new Vector3[puzzlePieces.Length]; 
        
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            puzzlePieces[i].layer = LayerMask.NameToLayer("Touch Object");
            correctPositions[i] = puzzlePieces[i].transform.position; 
        }

        PuzzleChecker();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + boundingBoxOffset, boundingBoxSize);
    }

    private void OnSolved()
    {

    }

    private void PuzzleChecker()
    {
        float distanceToPosition;

        foreach (var piece in puzzlePieces)
        {
            if (piece == null)
            {
                continue;
            }

            distanceToPosition = Vector3.Distance(piece.transform.position, piece.transform.position + new Vector3(0f, 0f, 1f));
            distanceToPosition = Mathf.Abs(distanceToPosition); 

            if (distanceToPosition <= mercyDistance)
            {
                Debug.Log("Here I am..."); 
            }
        }
    }



    void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        var t = sender as EasyWwiseCallback;
        if (t != null)
        {
            t.Invoke();
        }
    }

    public void TurnOffCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        Debug.Log("No Play function in Puzzles");
    }
}
