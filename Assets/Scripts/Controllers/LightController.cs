using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField]
    private List<LightSourceInput> lightSources;

    [SerializeField]
    private List<bool> lightsOn;

    public void LoadLights()
    {
        lightSources.Clear();
        lightSources = new List<LightSourceInput>();
        lightsOn = new List<bool>(); 

        LightSourceInput[] tmp = GameObject.FindObjectsOfType<LightSourceInput>();
        for (int i = 0; i < tmp.Length; i++)
        {
            lightSources.Add(tmp[i]);
            lightsOn.Add(tmp[i].Lit); 
        }
      //  TurnOffAllLights(); 
    }

    private void TurnOffAllLights()
    {
        foreach (var item in lightSources)
        {
            item.Lit = false;
            print(item + " is " + item.Lit); 
        }
    }
}
