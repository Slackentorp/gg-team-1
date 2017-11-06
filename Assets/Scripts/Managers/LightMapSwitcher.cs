using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightMapSwitcher : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] lightsOn;
    [SerializeField]
    private Texture2D[] lightsDirOn;
    [SerializeField]
    private Texture2D[] lightsOff;
    [SerializeField]
    private Texture2D[] lightsDirOff;

    private LightmapData[] lightsOnMaps;

    private LightmapData[] lightsOffMaps;

    void Start()
    {
        lightsOnMaps = new LightmapData[lightsOn.Length];
        for (int i = 0; i < lightsOn.Length; i++)
        {
            lightsOnMaps[i] = new LightmapData();
            lightsOnMaps[i].lightmapColor = lightsOn[i];
            lightsOnMaps[i].lightmapDir = lightsDirOn[i];
        }

        lightsOffMaps = new LightmapData[lightsOff.Length];
        for (int i = 0; i < lightsOff.Length; i++)
        {
            lightsOffMaps[i] = new LightmapData();
            lightsOffMaps[i].lightmapColor = lightsOff[i];
            lightsOffMaps[i].lightmapDir = lightsDirOff[i];
        }
    }

    public void SetLightsOn()
    {
        print(lightsOnMaps.Length);
        print("lights On!");
        LightmapSettings.lightmaps = lightsOnMaps;
    }

    public void SetLightsOff()
    {
        print("lights Off!");
        LightmapSettings.lightmaps = lightsOffMaps;
    }

}