using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject puzzleOutline;

    [SerializeField, Tooltip("Dimensions which the puzzle should work in")]
    private bool x, y, z;

    public bool LastPuzzle;
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
            else if (transform.GetChild(i).CompareTag("PuzzleFeedforward"))
            {
                puzzleOutline = transform.GetChild(i).gameObject;
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

        if (fragmentIsOnn == true)
        {
            TwoSecondsBeforeEnd();
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
        if (puzzleOutline != null)
        {
            StartCoroutine(FadeOutline(puzzleOutline.transform));
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
        // Piece already flagged as correctly placed, no need to go through again.
        if (piecePlaced[piece]) return;

        AkSoundEngine.PostEvent("PIECE_PLACED_CORRECTLY", puzzlePieces[piece]);
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

    IEnumerator FadeOutline(Transform outline)
    {
        if(!outline.gameObject.activeSelf)
        {
            yield break;
        }

        Renderer[] rendereres = outline.GetComponentsInChildren<Renderer>().Where(r => r.material.HasProperty("_TintColor")).ToArray();
        float time = 0;
        Color startColor = Color.white;
        startColor = rendereres[0].material.GetColor("_TintColor");

        while (time < 1)
        {
            foreach (var item in rendereres)
            {
                item.material.SetColor("_TintColor", new Color(startColor.r, startColor.g, startColor.b, 1 - time));
            }
            time += Time.deltaTime;
            yield return null;
        }
        
        outline.gameObject.SetActive(false);
    }




    public override void Play(Interactable.EasyWwiseCallback Callback)
    {
        if (string.IsNullOrEmpty(StoryFragment))
        {
            Debug.LogWarning("StoryFragment: \"" + StoryFragment + "\" does not exist");
            Callback();
            return;
        }

        Debug.Log("Story fragment - " + StoryFragment + " - ACTIVATE!");
        uint markerId = AkSoundEngine.PostEvent(StoryFragment, gameObject,
                            (uint)AkCallbackType.AK_EnableGetSourcePlayPosition | (uint)AkCallbackType.AK_Duration
                          | (uint)AkCallbackType.AK_EndOfEvent, EndOfEventCallback, Callback);
       
        SubToolXML.Instance.InitSubs(markerId, StoryFragment);
        EndFragments(markerId, StoryFragment);
        
    }

    public override void EndOfEventCallback(object sender, AkCallbackType callbackType, object info)
    {
        if (callbackType == AkCallbackType.AK_Duration)
        {
            var i = info as AkDurationCallbackInfo;
            fragmentDurations[counter] = i.fDuration;
            //for(int ni=0; ni<fragmentDurations.Length; ni++)
            //{

            //    fragmentDurations[ni] = i.fDuration;
            //    Debug.Log(fragmentDurations[ni]);
            //}
        
            
                if (counter == 2)
                {
                    fragmentIsOn = true;
                    Durations(fragmentDurations[2], fragmentIsOn, gameObject);
                }

                counter++;

                if (counter == 3)
                {
                    fragmentIsOn = false;
                    counter = 0;
                }
            }
            base.EndOfEventCallback(sender, callbackType, info);

    }

    public int counter = 0;
    private float[] fragmentDurations = new float[3];
    public bool fragmentIsOn = false;
    public uint markerr;
    public string storyFragmentt;
    public int realDuration;
    public float durationn;
    public bool fragmentIsOnn = false;
    public GameObject thePlayedFragment;
    int uPosition;

    private int ClipDuration(int time)
    {
        return (int)(time) - 20;
    }

    private bool WwiseEventDoesntExist(string eventName)
    {
        return AkSoundEngine.PrepareEvent(PreparationType.Preparation_Load, new string[] { eventName }, 1) == AKRESULT.AK_IDNotFound;

    }
    public void EndFragments(uint marker, string storyFragment)
    {
        markerr = marker;
        storyFragmentt = storyFragment;

    }

    public void TwoSecondsBeforeEnd()
    {
        AkSoundEngine.GetSourcePlayPosition(markerr, out uPosition);
        uPosition = uPosition / 10;
       // Debug.Log(uPosition);
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");

        if (fragmentIsOnn)
        {
            if (uPosition > realDuration)
            {
                Debug.Log("we are two seconds before the shit is done");
                AkSoundEngine.PostEvent("FRAGMENT_END", thePlayedFragment);
                fragmentIsOnn = false;
                return;
            }
        }
    }

    public void Durations(float duration, bool fragmentIsOn, GameObject playedFragment)
    {
        thePlayedFragment = playedFragment;
        fragmentIsOnn = fragmentIsOn;
        durationn = duration / 10;
        realDuration = (int)durationn;
        realDuration = realDuration - 20;

       // Debug.Log(realDuration);
    }


   
}