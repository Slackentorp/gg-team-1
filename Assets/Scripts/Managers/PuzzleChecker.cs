using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuzzleChecker : MonoBehaviour
{
    //[SerializeField]
    //private int nrOfPuzzels = 4;
    [SerializeField]
    public bool[] _SolvedPuzzels;
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
        _SolvedPuzzels = new bool[puzzels.Length];
        endLightPosition = endLight.transform.position;
        mothPosition = BootstrapManager.Instance.mothObject.transform.position;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
      //      UpdatePuzzels();
        //}
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SolveAllPuzzels();
        }
       // if (Input.GetKeyDown(KeyCode.F3))
       // {
            DistanceCheck();
        //}
    }

    private void UpdatePuzzels()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            if (puzzels[i].GetComponent<TMPTutorialPuzzle>() != null)
            {
                bool thisPuzzelStatus = puzzels[i].GetComponent<TMPTutorialPuzzle>().solved;
                _SolvedPuzzels[i] = thisPuzzelStatus;

                //print("PuzzelSolved: " + i + "-" + thisPuzzelStatus);
            }

            if (puzzels[i].GetComponent<BasePuzzle>() != null)
            {
                bool thisPuzzelStatus = puzzels[i].GetComponent<BasePuzzle>().isSolved;
                _SolvedPuzzels[i] = thisPuzzelStatus;

                //print("PuzzelSolved: " + i + "-" + thisPuzzelStatus);
            }
        }

        CheckWinCondition();
    }

    public void SolveAllPuzzels()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            _SolvedPuzzels[i] = true;
        }

        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        for (int i = 0; i < puzzels.Length; i++)
        {
            if (!_SolvedPuzzels[i])
            {
                return;
            }
        }
        endLight.Lit = true;
        return;
    }

    private void DistanceCheck()
    {
        mothPosition = BootstrapManager.Instance.mothObject.transform.position;
        distanceToEndLight = Vector3.Distance(mothPosition, endLightPosition);
        //print(distanceToEndLight);
        if (distanceToEndLight < 1)
        {
            //print("GG");
            endGameCanvas.SetActive(true);
        }
    }

    public void LoadScene()
    {
        Application.Quit();
        //endGameCanvas.SetActive(false);
        //BootstrapManager.Instance.ChangeLevelScene("Bootstrap");
        //BootstrapManager.Instance.ChangeLevelScene("AlphaLevelTesting");
    }
}
