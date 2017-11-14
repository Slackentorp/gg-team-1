using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[InitializeOnLoad]
public class InspectionCamera
{
    private static SceneView sceneCam;
    private static GameObject cameraMain;

    private static bool isChecked = false;

    private static Vector3 previousPosition;
    private static Quaternion previousRotation; 

    [MenuItem("Tools/Inspection Camera &i", false, -2)]
    public static void StartInspection()
    {

        sceneCam = (SceneView)SceneView.sceneViews[0];
        isChecked = !isChecked;

        Menu.SetChecked("Tools/Inspection Camera &i", isChecked);
        Init(); 
    }

    private static void Update()
    {
        if (sceneCam == null)
            return;

        cameraMain.transform.position = sceneCam.camera.transform.position;
        cameraMain.transform.rotation = sceneCam.rotation; 
    }

    private static bool Init()
    {
        cameraMain = GameObject.FindObjectsOfType<Camera>()[0].gameObject;

        if (isChecked)
        {
            EditorApplication.update += Update;
            previousPosition = cameraMain.transform.position;
            previousRotation = cameraMain.transform.rotation;
        }
        else
        {
            EditorApplication.update -= Update;
            cameraMain.transform.position = previousPosition;
            cameraMain.transform.rotation = previousRotation;
        }

        return SceneView.sceneViews[0] != null; 
    }
}
