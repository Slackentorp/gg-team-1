using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[InitializeOnLoad]
public class ResetCamera
{
    private static GameObject cameraMain;
    private static GameObject moth;

    [MenuItem("Tools/Reset Camera and Moth")]
    public static void ResetObjects()
    {
        Init(); 
    }

    private static void Init()
    {
        cameraMain = Camera.main.gameObject;
        moth = GameController.Instance.Moth;

        cameraMain.transform.position = new Vector3(-5.658f, 0.934f, 3.539f);
        cameraMain.transform.rotation = Quaternion.Euler(new Vector3(0, 155.874f, 0));

        moth.transform.position = new Vector3(-5.5f, 0.834f, 3.126f);
        moth.transform.rotation = Quaternion.Euler(new Vector3(0, -195.507f, 0));
    }
}
