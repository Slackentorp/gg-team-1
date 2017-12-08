using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InspectionCameraTool : MonoBehaviour {
#if UNITY_EDITOR

    [SerializeField]
    private bool newPossition = false;

    private ArrayList sceneViews;
    private GameObject gameObjecty;
    private GameObject cameraMain;

    private void Start()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update ()
    {
        SwapCameras();
        SaveNewPosition();
    }

    void SwapCameras()
    {
        sceneViews = UnityEditor.SceneView.sceneViews;
        UnityEditor.SceneView sceneView = (UnityEditor.SceneView)sceneViews[0];
        cameraMain.transform.rotation = sceneView.rotation;
        cameraMain.transform.position = sceneView.pivot;
    }

    void SaveNewPosition()
    {
        if (newPossition == true)
        {
            sceneViews = UnityEditor.SceneView.sceneViews;
            UnityEditor.SceneView sceneView = (UnityEditor.SceneView)sceneViews[0];

            cameraMain.transform.rotation = sceneView.rotation;
            cameraMain.transform.position = sceneView.pivot;

            newPossition = false;
            gameObjecty = GameObject.Find("CameraScriptHolder");
            DestroyImmediate(gameObjecty);
        }
    }
#endif
}
