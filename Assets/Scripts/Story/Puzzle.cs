﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CombinationPuzzleController))]
[System.Serializable]
public class Puzzle : Interactable
{
    //public delegate void EasyWwiseCallback();
    public delegate void PuzzleAction(GameObject puzzle);
    public static event PuzzleAction PuzzleCall;

    [SerializeField, Tooltip("The mercy distance")]
    private float mercyDistance;
    [SerializeField]
    private Vector3 boundingBoxSize = Vector3.one;
    [SerializeField]
    private Vector3 boundingBoxOffset;

    [SerializeField, Tooltip("The name of the puzzle.")]
    private string puzzleId;

    private List<GameObject> puzzlePieces;
    private List<Vector3> correctPositions;
    private bool[] piecePlaced;

    private Dictionary<GameObject, Vector3> correctPuzzle;

    private bool isSolved;

    [SerializeField, Tooltip("Dimensions which the puzzle should work in")]
    private bool x, y, z;

    public bool IsSolved { get { return isSolved; } private set { isSolved = value; } }
    public string PuzzleId { get { return PuzzleId; } }
    public Vector3 BoundingBoxSize()
    {
        return boundingBoxSize;
    }
    public Vector3 BoundingBoxOffset()
    {
        return boundingBoxOffset;
    }

    public override void Awake()
    {
        base.Awake();

        puzzlePieces = new List<GameObject>();
        correctPositions = new List<Vector3>();
        correctPuzzle = new Dictionary<GameObject, Vector3>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (transform.GetChild(i).CompareTag("PuzzlePiece"))
            {
                puzzlePieces.Add(child.gameObject);
                correctPositions.Add(child.position);
                Vector3 newRandomPos = new Vector3(
                    Random.Range(transform.localPosition.x - (boundingBoxSize.x + boundingBoxOffset.x) / 2f,
                        transform.position.x + (boundingBoxSize.x + boundingBoxOffset.x) / 2f),
                    puzzlePieces[i].transform.position.y,
                    Random.Range(transform.localPosition.z - (boundingBoxSize.z + boundingBoxOffset.z) / 2f,
                        transform.localPosition.z + (boundingBoxSize.z + boundingBoxOffset.z) / 2f)
                );
                puzzlePieces[i].transform.position = newRandomPos;
                correctPuzzle.Add(puzzlePieces[i], correctPositions[i]);
            }

            PictureFrameTouch pft = child.GetComponent<PictureFrameTouch>();
            if (pft != null)
            {
                pft.OnTouchUpEvent += UpdatePuzzle;
            }
        }

        GetComponent<BoxCollider>().size = boundingBoxSize;
        GetComponent<BoxCollider>().center = boundingBoxOffset;

        piecePlaced = new bool[puzzlePieces.Count];
        PositionPieceCorrectly(0);

        //PuzzleChecker();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SolvePuzzleNow();
        }
    }

    public void SolvePuzzleNow()
    {
        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            PositionPieceCorrectly(i);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + boundingBoxOffset, boundingBoxSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mercyDistance);
    }

    private void OnSolved(GameObject puzzleObj)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            PictureFrameTouch pft = child.GetComponent<PictureFrameTouch>();
            if (pft != null)
            {
                pft.OnTouchUpEvent -= UpdatePuzzle;
            }
        }

        if (PuzzleCall != null)
        {
            PuzzleCall(puzzleObj);
        }
    }

    public void UpdatePuzzle()
    {
        float distance = 0f;

        for (int i = 0; i < puzzlePieces.Count; i++)
        {
            distance = Vector3.Distance(puzzlePieces[i].transform.position, correctPositions[i]);
            if (Mathf.Abs(distance) < mercyDistance)
            {
                PositionPieceCorrectly(i);
            }
        }
    }

    private void PositionPieceCorrectly(int piece)
    {
        piecePlaced[piece] = true;
        puzzlePieces[piece].transform.position = correctPuzzle[puzzlePieces[piece]];
        puzzlePieces[piece].GetComponent<PictureFrameTouch>().isCorrect = true;
        puzzlePieces[piece].GetComponent<PictureFrameTouch>().enabled = false;
        puzzlePieces[piece].GetComponent<BoxCollider>().enabled = false;
        isSolved = CheckAllCorrect();
    }

    private bool CheckAllCorrect()
    {
        for (int i = 0; i < piecePlaced.Length; i++)
        {
            if (!piecePlaced[i])
                return false;
        }

        OnSolved(gameObject);
        return true;
    }
}