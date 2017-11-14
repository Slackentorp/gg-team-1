using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWallController : MonoBehaviour
{

    [SerializeField]
    private LightSourceInput[] roomLamps;
    private ParticleSystem doorParticleSystem;
    private bool[] getLamps = new bool[3];
    private bool[] checkOnes = new bool[3];
    private int getNrOfLamps = 0;
    private int LampsON = 0;
    private string lampsForWise;

    void OnEnable()
    {
        LightSourceInput.LightSourceCall += LampChecker;
    }

    void OnDisable()
    {
        LightSourceInput.LightSourceCall -= LampChecker;
    }

    void LampChecker()
    {
        for (int i = 0; i < getLamps.Length; i++)
        {
            getLamps[i] = roomLamps[i].LampActivated;
        }

        getNrOfLamps = CountArray(getLamps, true);
        lampsForWise = "LAMP_" + getNrOfLamps.ToString();
        AkSoundEngine.SetState("LAMPS_ON", lampsForWise);

        if ((getLamps[0] && getLamps[1]) ||
            (getLamps[0] && getLamps[2]) ||
            (getLamps[1] && getLamps[2]))
        {
            doorParticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
            GetComponent<Collider>().enabled = false;
            var em = doorParticleSystem.emission;
            em.enabled = false;
        }
    }

    private int CountArray(bool[] array, bool flag)
    {
        int value = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == flag) value++;
        }

        return value;
    }
}

