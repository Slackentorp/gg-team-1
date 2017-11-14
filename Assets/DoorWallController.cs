using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWallController : MonoBehaviour {

    [SerializeField]
    private GameObject[] roomLamps;
    private ParticleSystem doorParticleSystem;
    private bool[] getLamps = new bool[3];

	void Start ()
    {
		
	}
	
	void Update ()
    {
        LampChecker();
    }

    void LampChecker()
    {
        for (int i = 0; i < getLamps.Length; i++)
        {
            getLamps[i] = roomLamps[i].GetComponent<LightSourceInput>().LampActivated;
        }

        if ((getLamps[0] && getLamps[1]) && 
            (getLamps[0] && getLamps[2]) &&
            (getLamps[1] && getLamps[2]))
        {
            doorParticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
            GetComponent<Collider>().enabled = false;
            var em = doorParticleSystem.emission;
            em.enabled = false;
        }
    }
}
