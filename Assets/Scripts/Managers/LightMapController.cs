using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class LightMapController : MonoBehaviour
{
    private LightmapData[] LightMapSetON, LightMapSetOFF, customLightMapSet;
    [SerializeField]
    private Texture2D[] lightMapTexturesOFF;
    [SerializeField]
    private Texture2D[] lightMapTexturesON;
    [SerializeField]
    private bool[] LampsStates;
    //[SerializeField]
    //private int nrOfLamps;
    //private LightSourceInput[] lamps;

    void OnEnable()
    {
        LightSourceInput.LightMapSwitchCall += LampAssigner;
    }

    void OnDisable()
    {
        LightSourceInput.LightMapSwitchCall -= LampAssigner;
    }

    //// Use this for initialization
    void Start()
    {
        LampsStates = new bool[lightMapTexturesOFF.Length];
        LightMapAssigner();
    }

    void LampAssigner(bool stateCheck, int indexNr)
    {
        LampsStates[indexNr] = stateCheck;
        CustomSetManager();
    }

    void LightMapAssigner()
    {
        LightMapSetON = new LightmapData[lightMapTexturesON.Length];
        for (int i = 0; i < lightMapTexturesON.Length; i++)
        {
            LightMapSetON[i] = new LightmapData();
            LightMapSetON[i].lightmapColor = lightMapTexturesON[i];
        }

        LightMapSetOFF = new LightmapData[lightMapTexturesOFF.Length];
        for (int i = 0; i < lightMapTexturesOFF.Length; i++)
        {
            LightMapSetOFF[i] = new LightmapData();
            LightMapSetOFF[i].lightmapColor = lightMapTexturesOFF[i];
        }
    }
    [Button]
    public void CustomSetManager()
    {
        customLightMapSet = new LightmapData[lightMapTexturesON.Length];
        for (int i = 0; i < lightMapTexturesON.Length; i++)
        {
            if (LampsStates[i])
            {
                customLightMapSet[i] = LightMapSetON[i];
            }
            else if (!LampsStates[i])
            {
                customLightMapSet[i] = LightMapSetOFF[i];
            }
        }
        LightmapSettings.lightmaps = customLightMapSet;
    }

    public void SetLightsOn()
    {
        print("All Lights On!");
        LightmapSettings.lightmaps = LightMapSetON;
    }

    public void SetLightsOff()
    {
        print("All Lights Off!");
        LightmapSettings.lightmaps = LightMapSetOFF;
    }

}
