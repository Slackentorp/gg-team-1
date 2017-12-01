using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPuzzlePiece : MonoBehaviour, ITouchInput
{
    MenuPuzzle parentPuzzle; 

    private bool isCorrect;
    public bool IsCorrect { get { return isCorrect; } set { isCorrect = value; } }

    private Vector3 distanceWorldPos; 

    public void OnSwipe(TouchDirection direction)
    {
    }

    public void OnTap()
    {
    }

    public void OnTouchDown(Vector3 worldPos)
    {
        distanceWorldPos = worldPos - transform.position; 
    }

    public void OnTouchExit()
    {
    }

    public void OnToucHold(Vector3 worldPos)
    {
        Vector3 newPosition = worldPos - distanceWorldPos;
        newPosition.z = 0; 
        transform.position = newPosition;
        Debug.Log("HEllo"); 
    }

    public void OnTouchUp()
    {
    }

    // Use this for initialization
    void Start()
    {
        parentPuzzle = GetComponentInParent<MenuPuzzle>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
