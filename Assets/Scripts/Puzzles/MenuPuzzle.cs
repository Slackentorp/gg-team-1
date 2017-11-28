using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPuzzle : MonoBehaviour
{
    [SerializeField]
    private float distance;

    private Transform[] startPositions;

    private Transform[] correctionPositions;

    private Vector3 centerPosition;

    // Use this for initialization
    void Start()
    {
        correctionPositions = transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < correctionPositions.Length; i++)
        {
            centerPosition += correctionPositions[i].position;
        }

        centerPosition = centerPosition / correctionPositions.Length;

        startPositions = transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GoToStartPoints()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.3f);

        for (int i = 0; i < correctionPositions.Length; i++)
        {
            Gizmos.DrawWireSphere((transform.position - correctionPositions[i].position) * distance, 0.5f);
        }
    }
}
