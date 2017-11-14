using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWallController : MonoBehaviour {

    [SerializeField]
    private GameObject[] roomLamps;
    private bool[] getLamps = new bool[3];
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LampChecker()
    {
        for (int i = 0; i < getLamps.Length; i++)
        {
            getLamps[i] = roomLamps[i].GetComponent<LightSourceInput>().LampActivated;
        }
    }
}
