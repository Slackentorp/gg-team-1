using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;

public class LightMapSwitcher : MonoBehaviour
{
    private int arrayTrack;
    public int[] myLightMapIndex;
    private Renderer lmRenderer;

    private Texture2D[] customLights, currentTexture;
    [SerializeField]
    private Texture2D[] lightsOn;
    [SerializeField]
    private PuzzleChecker getPuzzelChecker;
    [SerializeField]
    private Texture2D[] lightsDirOn;
    [SerializeField]
    private Texture2D[] lightsOff;
    [SerializeField]
    private Texture2D[] lightsDirOff;
    [SerializeField]
    private int lightNumber;
    private int[] lightArray;
    [SerializeField]
    private GameObject[] sceneLights;
    [SerializeField]
    private GameObject[] getLamps;

    public LightmapData[] _lightsOnMaps;

    public LightmapData[] _lightsOffMaps;

    public LightmapData[] _newData, _currentData;

    private GameObject[] allObjects;

    private bool lightSwitchON = true, lightSwitchOFF, solvedTutorial;

    void Start()
    {
        LightMapAssinger();
        SetLightsOff();

        //LightMapAssinger();

        //_newData = _lightsOffMaps;
        //LightmapSettings.lightmaps = _newData;



        //   LightMapIndexFinder();
    }

    private void Update()
    {
        if (getPuzzelChecker._SolvedPuzzels[0] && !solvedTutorial)
        {
            //LightMapAssinger();
            SetLightsOn();
            solvedTutorial = true;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!lightSwitchOFF)
            {
                SetLightsOff();
            }
            else if (!lightSwitchON)
            {
                SetLightsOn();
            }
        }
    }

    public void SetLightsOn()
    {
        print("All Lights On!");
        LightmapSettings.lightmaps = _lightsOnMaps;
        lightSwitchON = true;
        lightSwitchOFF = false;
    }

    public void SetLightsOff()
    {
        print("All Lights Off!");
        LightmapSettings.lightmaps = _lightsOffMaps;
        lightSwitchON = false;
        lightSwitchOFF = true;
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

    [Button]
    public void LightMapIndexFinder()
    {
        allObjects = FindObjectsOfType<GameObject>();
        myLightMapIndex = new int[allObjects.Length];

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Renderer>() != null)
            {
                lmRenderer = obj.GetComponent<Renderer>();
                myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
                arrayTrack++;
            }
        }
        LightMapAssinger();
    }

    public void SwitchLightNr()
    {

        foreach (GameObject obj in allObjects)
        {
            _currentData = LightmapSettings.lightmaps;

            if (obj.GetComponent<Renderer>() != null)
            {

                lmRenderer = obj.GetComponent<Renderer>();

                if (lmRenderer.lightmapIndex == lightNumber)
                {

                    _newData = new LightmapData[sceneLights.Length];
                    for (int i = 0; i < sceneLights.Length; i++)
                    {

                        _newData[i] = new LightmapData();
                        customLights = new Texture2D[sceneLights.Length];
                        customLights[i] = _currentData[i].lightmapColor;
                        customLights[lightNumber] = lightsOn[lightNumber];

                        _newData[i].lightmapColor = customLights[i];
                    }
                    LightmapSettings.lightmaps = _newData;
                }
                //myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
                arrayTrack++;
            }
        }
    }

    private void MaterialEmissionSwitch()
    {
        for (int i = 0; i < 2; i++)
        {
            //getLamps.
        }
    }
}
//public void AssignLights()
//{

//    for (int i = 0; i < sceneLights.Length; i++)
//    {
//        lightArray[i]
//    }
//}
