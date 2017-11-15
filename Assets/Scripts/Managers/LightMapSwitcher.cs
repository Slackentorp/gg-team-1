using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EasyButtons;

public class LightMapSwitcher : MonoBehaviour
{
    private int arrayTrack;
    //public int[] myLightMapIndex;
    private Renderer lmRenderer;

    private int roomID;

    [SerializeField]
    public Texture2D[] lightMapRoom1ID_1, lightMapRoom1ID_2, lightMapRoom1ID_3, lightMap1ID_4,
        lightMap1ID_5, lightMap1ID_6, lightMap1ID_7, lightMap1ID_8;

    [SerializeField]
    public Texture2D[] lightMapRoom2ID_1, lightMapRoom2ID_2, lightMapRoom2ID_3, lightMap2ID_4,
        lightMap2ID_5, lightMap2ID_6, lightMap2ID_7, lightMap2ID_8;

    [SerializeField]
    public Texture2D[] lightMapRoom3ID_1, lightMapRoom3ID_2, lightMapRoom3ID_3, lightMap3ID_4,
        lightMap3ID_5, lightMap3ID_6, lightMap3ID_7, lightMap3ID_8;

    [SerializeField]
    public Texture2D[] lightMapRoom4ID_1, lightMapRoom4ID_2, lightMapRoom4ID_3, lightMap4ID_4,
        lightMap4ID_5, lightMap4ID_6, lightMap4ID_7, lightMap4ID_8;

    [SerializeField]
    public Texture2D[] lightMapRoom5ID_1, lightMapRoom5ID_2, lightMapRoom5ID_3, lightMap5ID_4,
        lightMap5ID_5, lightMap5ID_6, lightMap5ID_7, lightMap5ID_8;

    private LightmapData[] lightMapData_1, lightMapData_2, lightMapData_3, lightMapData_4,
        lightMapData_5, lightMapData_6, lightMapData_7, lightMapData_8;


    [SerializeField]
    private int roomIndex = 5;

    private int tmpInt = 1;

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
        //LightMapAssinger2();
        //SetLightsOff();

        LightMapAssigner();

        //LightMapAssinger();

        //_newData = _lightsOffMaps;
        //LightmapSettings.lightmaps = _newData;



        //   LightMapIndexFinder();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            LightMapSwitch();
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

    [Button]
    private void LightMapAssigner()
    {
        lightMapData_1 = new LightmapData[lightMapRoom1ID_1.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_1[i] = new LightmapData();
            lightMapData_1[i].lightmapColor = lightMapRoom1ID_1[i];
        }
        lightMapData_2 = new LightmapData[lightMapRoom1ID_2.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_2[i] = new LightmapData();
            lightMapData_2[i].lightmapColor = lightMapRoom1ID_2[i];
        }
        lightMapData_3 = new LightmapData[lightMapRoom1ID_3.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_3[i] = new LightmapData();
            lightMapData_3[i].lightmapColor = lightMapRoom1ID_3[i];
        }
        lightMapData_4 = new LightmapData[lightMap1ID_4.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_4[i] = new LightmapData();
            lightMapData_4[i].lightmapColor = lightMap1ID_4[i];
        }
        lightMapData_5 = new LightmapData[lightMap1ID_5.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_5[i] = new LightmapData();
            lightMapData_5[i].lightmapColor = lightMap1ID_5[i];
        }
        lightMapData_6 = new LightmapData[lightMap1ID_6.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_6[i] = new LightmapData();
            lightMapData_6[i].lightmapColor = lightMap1ID_6[i];
        }
        lightMapData_7 = new LightmapData[lightMap1ID_7.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_7[i] = new LightmapData();
            lightMapData_7[i].lightmapColor = lightMap1ID_7[i];
        }
        lightMapData_8 = new LightmapData[lightMap1ID_8.Length];
        for (int i = 0; i < lightMapRoom1ID_1.Length; i++)
        {
            lightMapData_8[i] = new LightmapData();
            lightMapData_8[i].lightmapColor = lightMap1ID_8[i];
        }
    }

    private void LightMapSwitch()
    {
        tmpInt++;
        switch (tmpInt)
        {
            case 1:
                LightmapSettings.lightmaps = lightMapData_1;
                break;
            case 2:
                LightmapSettings.lightmaps = lightMapData_2;
                break;
            case 3:
                LightmapSettings.lightmaps = lightMapData_3;
                break;
            case 4:
                LightmapSettings.lightmaps = lightMapData_4;
                break;
            case 5:
                LightmapSettings.lightmaps = lightMapData_5;
                break;
            case 6:
                LightmapSettings.lightmaps = lightMapData_6;
                break;
            case 7:
                LightmapSettings.lightmaps = lightMapData_7;
                break;
            case 8:
                LightmapSettings.lightmaps = lightMapData_8;
                tmpInt = 0;
                break;
        }
    }

    public void LightMapAssinger2()
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
            _lightsOffMaps[i].lightmapDir = lightsDirOff[i];
        }
    }

    // [Button]
    //public void LightMapIndexFinder()
    //{
    //    allObjects = FindObjectsOfType<GameObject>();
    //    myLightMapIndex = new int[allObjects.Length];

    //    foreach (GameObject obj in allObjects)
    //    {
    //        if (obj.GetComponent<Renderer>() != null)
    //        {
    //            lmRenderer = obj.GetComponent<Renderer>();
    //            myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
    //            arrayTrack++;
    //        }
    //    }
    //    LightMapAssinger2();
    //}

    //public void SwitchLightNr()
    //{

    //    foreach (GameObject obj in allObjects)
    //    {
    //        _currentData = LightmapSettings.lightmaps;

    //        if (obj.GetComponent<Renderer>() != null)
    //        {

    //            lmRenderer = obj.GetComponent<Renderer>();

    //            if (lmRenderer.lightmapIndex == lightNumber)
    //            {

    //                _newData = new LightmapData[sceneLights.Length];
    //                for (int i = 0; i < sceneLights.Length; i++)
    //                {

    //                    _newData[i] = new LightmapData();
    //                    customLights = new Texture2D[sceneLights.Length];
    //                    customLights[i] = _currentData[i].lightmapColor;
    //                    customLights[lightNumber] = lightsOn[lightNumber];

    //                    _newData[i].lightmapColor = customLights[i];
    //                }
    //                LightmapSettings.lightmaps = _newData;
    //            }
    //            //myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
    //            arrayTrack++;
    //        }
    //    }
    //}

    //private void MaterialEmissionSwitch()
    //{
    //    for (int i = 0; i < 2; i++)
    //    {
    //        //getLamps.
    //    }
    //}
}
//public void AssignLights()
//{

//    for (int i = 0; i < sceneLights.Length; i++)
//    {
//        lightArray[i]
//    }
//}
