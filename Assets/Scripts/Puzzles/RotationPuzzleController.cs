using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleController : MonoBehaviour
{

    [SerializeField]
    private float rotationAmount;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetRotationAmount()
    {
        return rotationAmount;
    }
}
