using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuzzleChecker : MonoBehaviour
{
    //[SerializeField]
    //private int nrOfPuzzels = 4;
    [SerializeField]
    private bool[] SolvedPuzzels;
    [SerializeField]
    private GameObject[] puzzels;
    [SerializeField]
    private LightSourceInput endLight;
    [SerializeField]
    private GameObject cameraObject;
    [SerializeField]
    private GameController getGameController;
    [SerializeField]
    private Vector3 endLightPosition, mothPosition;
    [SerializeField]
    private GameObject endGameCanvas;
    private float distanceToEndLight;

    void Start()
    {
        SolvedPuzzels = new bool[puzzels.Length];
        endLightPosition = endLight.transform.position;
        mothPosition = getGameController.mothObject.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UpdatePuzzels();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SolveAllPuzzels();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DistanceCheck();
        }

        //mothPosition = getGameController.mothObject.transform.position;
        //distanceToEndLight = Vector3.Distance(mothPosition, endLightPosition);
        //if (distanceToEndLight < 0.35)
        //{
        //    print("GG");
        //}

    }

    private void UpdatePuzzels()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            if (puzzels[i].GetComponent<TMPTutorialPuzzle>() != null)
            {
                bool thisPuzzelStatus = puzzels[i].GetComponent<TMPTutorialPuzzle>().solved;
                SolvedPuzzels[i] = thisPuzzelStatus;

                print("PuzzelSolved: " + i + "-" + thisPuzzelStatus);
            }

            if (puzzels[i].GetComponent<WorldMap>() != null)
            {
                bool thisPuzzelStatus = puzzels[i].GetComponent<WorldMap>().worldMapCompleted;
                SolvedPuzzels[i] = thisPuzzelStatus;

                print("PuzzelSolved: " + i + "-" + thisPuzzelStatus);
            }

            if (puzzels[i].GetComponent<BasePuzzle>() != null)
            {
                bool thisPuzzelStatus = puzzels[i].GetComponent<BasePuzzle>().isSolved;
                SolvedPuzzels[i] = thisPuzzelStatus;

                print("PuzzelSolved: " + i + "-" + thisPuzzelStatus);
            }
        }

        CheckWinCondition();
    }

    public void SolveAllPuzzels()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            SolvedPuzzels[i] = true;
        }

        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            if (!SolvedPuzzels[i])
            {
                return;
            }
        }
        endLight.Lit = true;
        return;
    }

    private void DistanceCheck()
    {
        mothPosition = getGameController.mothObject.transform.position;
        distanceToEndLight = Vector3.Distance(mothPosition, endLightPosition);
        if (distanceToEndLight < 0.35)
        {
            print("GG");
            endGameCanvas.SetActive(true);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }
}
