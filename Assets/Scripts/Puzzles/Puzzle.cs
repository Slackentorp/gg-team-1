using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombinationPuzzleController))]
public class Puzzle : Interactable
{
    public delegate void EasyWwiseCallback();
    public delegate void PuzzleAction();
    public static event PuzzleAction PuzzleCall;

    [SerializeField, Tooltip("The name of the puzzle.")]
    private string puzzleId;

    private CombinationPuzzleController puzzleController;
    private bool isSolved;


    public bool IsSolved { get { return isSolved; } private set { isSolved = value; } }
    public string PuzzleId { get { return PuzzleId; } }


    public override void Awake()
    {
        base.Awake();
        puzzleController = GetComponent<CombinationPuzzleController>();
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
