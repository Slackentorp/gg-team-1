using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPuzzle : MonoBehaviour
{
    [SerializeField]
    private float distance;

    [SerializeField]
    private Transform[] startPositions;
    private Vector3[] scrambledPosition;

    private Transform[] correctPosition;

    private List<Vector3> startPos;

    private Vector3 centerPosition;


    private float globalWaitSeconds = 2f;

    void Start()
    {
        correctPosition = transform.GetComponentsInChildren<Transform>();
        startPos = new List<Vector3>();
        centerPosition = Vector3.zero;

        for (int i = 0; i < transform.childCount; i++)
        {
            startPos.Add(transform.GetChild(i).position);
            centerPosition += transform.GetChild(i).position;
        }

        centerPosition = centerPosition / startPos.Count;


        StartCoroutine(OpeningSequence());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPosition();
            StartCoroutine(OpeningSequence());
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
            transform.GetChild(i).position = startPos[i];
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawWireSphere(centerPosition, 0.1f);
            for (int i = 0; i < startPositions.Length; i++)
            {
                if (startPos[0] != null)
                    Gizmos.DrawWireSphere(startPos[i], 0.1f);
                Gizmos.DrawWireSphere(((startPos[i] - centerPosition)) * distance, 0.2f);
            }
        }
    }

    IEnumerator OpeningSequence()
    {
        yield return new WaitForSeconds(globalWaitSeconds);

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

        while (t < 1f)
        {
            piece.transform.localScale = Vector3.one + new Vector3(0.1f, 0.1f, 0.1f) * t;
            rot.z = Random.Range(-1f, 1f) * 5f * (t / 2);
            piece.transform.rotation = Quaternion.Euler(rot);
            t += Time.deltaTime;
            yield return null;
        }

        piece.transform.localScale = Vector3.one;
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
