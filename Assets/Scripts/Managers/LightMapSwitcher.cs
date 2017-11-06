using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightMapSwitcher : MonoBehaviour
{
    private int arrayTrack;
    private int[] myLightMapIndex;
    private Renderer lmRenderer;

    [SerializeField]
    private Texture2D[] customLights;

    [SerializeField]
    private Texture2D[] lightsOn;
    [SerializeField]
    private Texture2D[] lightsDirOn;
    [SerializeField]
    private Texture2D[] lightsOff;
    [SerializeField]
    private Texture2D[] lightsDirOff;

    public LightmapData[] _lightsOnMaps;

    public LightmapData[] _lightsOffMaps;

    public LightmapData[] _currentData;

    private GameObject[] allObjects;

    void Start()
    {
        LightMapAssinger();

        LightMapIndexFinder();
    }

    public void SetLightsOn()
    {
        print("lights On!");
        LightmapSettings.lightmaps = _lightsOnMaps;
    }

    public void SetLightsOff()
    {
        print("lights Off!");
        LightmapSettings.lightmaps = _lightsOffMaps;
    }

    public void LightMapAssinger()
    {
        _lightsOnMaps = new LightmapData[lightsOn.Length];
        for (int i = 0; i < lightsOn.Length; i++)
        {
            _lightsOnMaps[i] = new LightmapData();
            _lightsOnMaps[i].lightmapColor = lightsOn[i];
        }

        for (int i = 0; i < lightsDirOn.Length; i++)
        {
            _lightsOnMaps[i].lightmapDir = lightsDirOn[i];
        }

        _lightsOffMaps = new LightmapData[lightsOff.Length];
        for (int i = 0; i < lightsOff.Length; i++)
        {
            _lightsOffMaps[i] = new LightmapData();
            _lightsOffMaps[i].lightmapColor = lightsOff[i];
        }

        for (int i = 0; i < lightsDirOff.Length; i++)
        {
            _lightsOnMaps[i].lightmapDir = lightsDirOff[i];
        }
    }

    public void LightMapIndexFinder()
    {
        allObjects = FindObjectsOfType<GameObject>();
        arrayTrack = 0;
        myLightMapIndex = new int[allObjects.Length];
        //print("arrayLengt: " + allObjects.Length);

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Renderer>() != null)
            {
                lmRenderer = obj.GetComponent<Renderer>();
                myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
                //print("arrayIndex: " + lmRenderer.lightmapIndex);
                arrayTrack++;
            }
        }
    }

    public void SwitchLightNr()
    {
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Renderer>() != null)
            {
                lmRenderer = obj.GetComponent<Renderer>();
                if (lmRenderer.lightmapIndex == 0)
                {
                    _currentData = new LightmapData[3];
                    for (int i = 0; i < 3; i++)
                    {
                        _currentData[i] = new LightmapData();
                        print("imHere");
                        customLights = lightsOn;
                        customLights[0] = lightsOff[0];
                        _currentData[i].lightmapColor = customLights[i];
                    }
                    LightmapSettings.lightmaps = _currentData;
                }
                //myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
                arrayTrack++;
            }
        }
    }

}