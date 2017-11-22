using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWallController : MonoBehaviour
{

    [SerializeField]
    private LightSourceInput[] roomLamps;
    private ParticleSystem doorParticleSystem;
    [SerializeField]
    private bool[] getLamps;
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

    private void Start()
    {
        getLamps = new bool[roomLamps.Length];
    }

    void LampChecker()
    {

        getLamps = new bool[roomLamps.Length];
        for (int i = 0; i < roomLamps.Length; i++)
        {
            getLamps[i] = roomLamps[i].LampActivated;
        }

        getNrOfLamps = CountArray(getLamps, true);
       // print("")
        lampsForWise = "LAMP_" + getNrOfLamps.ToString();
        AkSoundEngine.SetState("LAMPS_ON", lampsForWise);
        if (getLamps.Length == 3)
        {
            if (getNrOfLamps == getLamps.Length ||
                getNrOfLamps == getLamps.Length - 1)
            {
                gameObject.SetActive(false);
                //doorParticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
                //GetComponent<Collider>().enabled = false;
            }
        }
        else if (getNrOfLamps == getLamps.Length)
        {
            gameObject.SetActive(false);
            //doorParticleSystem = GetComponentsInChildren<ParticleSystem>()[0];
            //GetComponent<Collider>().enabled = false;
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

