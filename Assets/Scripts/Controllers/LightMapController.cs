using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class LightMapController : MonoBehaviour
{
    private LightmapData[] LightMapSetON, LightMapSetOFF, LightMapSetFlickering, customLightMapSet;
    [SerializeField]
    private Texture2D[] lightMapTexturesOFF;
    [SerializeField]
    private Texture2D[] lightMapTexturesON;
    [SerializeField]
    private Texture2D[] lightMapTexturesFlickering;
    [SerializeField]
    private bool[] LampsStates, flickStates;
    private bool flickerCheckPass;
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

    // Use this for initialization
    void Start()
    {
        LampsStates = new bool[lightMapTexturesOFF.Length];
        flickStates = new bool[lightMapTexturesOFF.Length];
        LightMapAssigner();
    }

    void LampAssigner(bool stateCheck, bool flickerCheck, int indexNr)
    {
        LampsStates[indexNr] = stateCheck;
        flickStates[indexNr] = flickerCheck;

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

        LightMapSetFlickering = new LightmapData[lightMapTexturesFlickering.Length];
        for (int i = 0; i < lightMapTexturesFlickering.Length; i++)
        {
            LightMapSetFlickering[i] = new LightmapData();
            LightMapSetFlickering[i].lightmapColor = lightMapTexturesFlickering[i];
        }
    }

    [Button]
    public void CustomSetManager()
    {
        customLightMapSet = new LightmapData[lightMapTexturesON.Length];
        for (int i = 0; i < lightMapTexturesON.Length; i++)
        {
            //Debug.Log("flickState" + flickStates.Length + "=" + flickStates[i]);

            if (LampsStates[i] && !flickStates[i])
            {
                customLightMapSet[i] = LightMapSetON[i];
            }
            else if (!LampsStates[i])
            {
                customLightMapSet[i] = LightMapSetOFF[i];
            }
            else if (flickStates[i])
            {
                customLightMapSet[i] = LightMapSetFlickering[i];
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
