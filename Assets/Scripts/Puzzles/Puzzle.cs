using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CombinationPuzzleController))]
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
                    transform.localPosition.y,
                     Random.Range(transform.localPosition.z - (boundingBoxSize.z + boundingBoxOffset.z) / 2f,
                    transform.localPosition.z + (boundingBoxSize.z + boundingBoxOffset.z) / 2f)
                    );
                puzzlePieces[i].transform.position = newRandomPos;
                correctPuzzle.Add(puzzlePieces[i], correctPositions[i]);
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
            for (int i = 0; i < puzzlePieces.Count; i++)
            {
                PositionPieceCorrectly(i);
            }
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

    private void PuzzleChecker()
    {
        //float distanceToPosition;

        //foreach (var piece in puzzlePieces)
        //{
        //    if (piece == null)
        //    {
        //        continue;
        //    }

        //    distanceToPosition = Vector3.Distance(piece.transform.position, piece.transform.position + new Vector3(0f, 0f, 1f));
        //    distanceToPosition = Mathf.Abs(distanceToPosition); 

        //    if (distanceToPosition <= mercyDistance)
        //    {
        //        Debug.Log("Here I am..."); 
        //    }
        //}
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