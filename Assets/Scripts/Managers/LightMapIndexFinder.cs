using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMapIndexFinder : MonoBehaviour
{
    private int arrayTrack;
    private int[] myLightMapIndex;
    private Renderer lmRenderer;

    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        arrayTrack = 0;
        myLightMapIndex = new int[allObjects.Length];
        //print("arrayLengt: " + allObjects.Length);

        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Renderer>() != null)
            {
                lmRenderer = obj.GetComponent<Renderer>();
                myLightMapIndex[arrayTrack] = lmRenderer.lightmapIndex;
                print("arrayIndex: " + lmRenderer.lightmapIndex);
                arrayTrack++;
            }
        }
    }
}
