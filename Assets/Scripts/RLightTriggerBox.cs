using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLightTriggerBox : MonoBehaviour
{
    [SerializeField]
    private GameObject[] realTimeLight;
    private bool lightState;

    private void Start()
    {
        for (int i = 0; i < realTimeLight.Length; i++)
        {
            if (!lightState)
            {
                lightState = true;
                realTimeLight[i].SetActive(true);
            }
            else if (lightState)
            {
                lightState = false;
                realTimeLight[i].SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < realTimeLight.Length; i++)
        {
            if (!lightState)
            {
                lightState = true;
                realTimeLight[i].SetActive(true);
            }
            else if (lightState)
            {
                lightState = false;
                realTimeLight[i].SetActive(false);
            }
        }
    }
}
