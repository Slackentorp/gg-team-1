using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPuzzle : MonoBehaviour
{
    public delegate void PuzzleSolved();
    public static event PuzzleSolved OnFinished;

    // Setup Variables
    [SerializeField]
    private float distance;
    [SerializeField]
    private Transform[] startPositions;
    private Vector3[] scrambledPosition;
    private Transform[] correctPosition;
    private List<Vector3> startPos;
    private Vector3 centerPosition;

    private Camera inputCamera;
    private GameObject currentPiece;

    private Dictionary<GameObject, Vector3> piecesPos;

    // Puzzle Variables
    [SerializeField]
    private float mercyDistance;

    private float globalWaitSeconds = 0.6f;

    public void InitializePuzzle()
    {
        piecesPos = new Dictionary<GameObject, Vector3>();

        inputCamera = Camera.main;
        correctPosition = transform.GetComponentsInChildren<Transform>();
        startPos = new List<Vector3>();
        centerPosition = Vector3.zero;

        for (int i = 0; i < transform.childCount; i++)
        {
            startPos.Add(transform.GetChild(i).position);
            centerPosition += transform.GetChild(i).position;
            piecesPos.Add(transform.GetChild(i).gameObject, transform.GetChild(i).position);
        }

        centerPosition = centerPosition / startPos.Count;

        StartCoroutine(OpeningSequence());
    }

    public bool CorrectPosition(Vector3 piece, Vector3 correctPos)
    {
        float distance = Vector3.Distance(piece, correctPos);
        return distance < mercyDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
            StartCoroutine(OpeningSequence());
        }

        if (Input.GetMouseButtonDown(0) && currentPiece == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (piecesPos.ContainsKey(hit.transform.gameObject))
                {
                    currentPiece = hit.transform.gameObject;
                    AkSoundEngine.PostEvent("PIECE_PICK", currentPiece);
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && currentPiece != null)
        {
            bool close = Vector3.Distance(piecesPos[currentPiece], currentPiece.transform.position) < mercyDistance;
            AkSoundEngine.PostEvent("PIECE_PLACE", currentPiece);
            if (close)
            {
                currentPiece.transform.position = piecesPos[currentPiece];
                piecesPos.Remove(currentPiece);
                AkSoundEngine.PostEvent("PIECE_PLACED_RIGHT", currentPiece);
                if (piecesPos.Count <= 0)
                {
                    OnPuzzleSolved();
                }
            }
            currentPiece = null;
        }

        if (Input.GetMouseButton(0) && currentPiece != null)
        {
            currentPiece.transform.position = MovePiece();
        }
    }

    private Vector3 MovePiece()
    {
        Vector3 result;
        result = inputCamera.ScreenToWorldPoint(Input.mousePosition);
        result.z = 0f;
        return result;
    }

    private void GoToStartPoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            StartCoroutine(Spring(transform.GetChild(i).gameObject, startPositions[i].position));
        }
    }

    private void ResetPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = startPos[i];
        }
    }

    private void OnPuzzleSolved()
    {
        if (OnFinished != null)
        {
            OnFinished();
        }
    }

    IEnumerator OpeningSequence()
    {
        yield return new WaitForSeconds(globalWaitSeconds);

        AkSoundEngine.PostEvent("PUZZLE_OPENING", gameObject);

        for (int i = 1; i < transform.childCount; i++)
        {
            StartCoroutine(ScaleUp(transform.GetChild(i).gameObject));
        }
        yield return ScaleUp(transform.GetChild(0).gameObject);

        GoToStartPoints();
    }

    IEnumerator ScaleUp(GameObject piece)
    {
        float t = 0;
        Vector3 rot = Vector3.zero;
        Vector3 startSize = piece.transform.localScale;

        while (t < 1f)
        {
            rot.z = Random.Range(-1f, 1f) * 2f * (t / 2);
            piece.transform.rotation = Quaternion.Euler(rot);
            t += Time.deltaTime;
            yield return null;
        }

        piece.transform.rotation = Quaternion.Euler(Vector3.zero);
    }


    IEnumerator Spring(GameObject piece, Vector3 endPos)
    {
        float t = 0f;
        float x = 0f;
        float factor = 0.7f;

        Vector3 startPos = piece.transform.position;

        yield return new WaitForSeconds(Random.Range(0f, 0.15f));


        while (t < 1)
        {
            x = Mathf.Pow(2, -10f * t) * Mathf.Sin((t - factor / 4) * (2 * Mathf.PI) / factor) + 1;
            piece.transform.position = endPos * x + startPos * (1 - x);
            t += Time.deltaTime;
            yield return null;
        }


    }
}
