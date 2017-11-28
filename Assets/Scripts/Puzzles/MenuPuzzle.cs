using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPuzzle : MonoBehaviour
{
    [SerializeField]
    private float distance;

    [SerializeField]
    private Transform[] startPositions;

    private Transform[] correctPosition;

    private List<Vector3> startPos;

    private Vector3 centerPosition;

    // Use this for initialization
    void Start()
    {
        correctPosition = transform.GetComponentsInChildren<Transform>();
        startPos = new List<Vector3>();

        Debug.Log("Child count " + transform.childCount); 
        for (int i = 0; i < transform.childCount; i++)
        {
            startPos.Add(transform.GetChild(0).position); 
            //Debug.Log("Child " + i + " " + correctPosition[i].position); 
            //startPos[i] = correctPosition[i].position;
            //centerPosition += correctPosition[i].position;
        }

        centerPosition = centerPosition / correctPosition.Length;
        GoToStartPoints();
        //startPositions = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
            GoToStartPoints();
        }
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
            transform.GetChild(i).position = correctPosition[i].position;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < startPositions.Length; i++)
        {
            if (startPos[0] != null)
                Gizmos.DrawWireSphere(startPos[i], 0.1f * i + 0.1f);
            Gizmos.DrawWireSphere(startPositions[i].position, 0.5f);
        }
    }

    IEnumerator Spring(GameObject piece, Vector3 endPos)
    {
        float t = 0f;
        float x = 0f;
        float factor = 0.4f;

        Vector3 startPos = piece.transform.position;

        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

        while (t < 1)
        {
            x = Mathf.Pow(2, -10f * t) * Mathf.Sin((t - factor / 4) * (2 * Mathf.PI) / factor) + 1;
            piece.transform.position = endPos * x + startPos * (1 - x);
            t += Time.deltaTime;
            yield return null;
        }

        //factor = 0.4
        //pow(2, -10 * x) * sin((x - factor / 4) * (2 * PI) / factor) + 1
    }
}
