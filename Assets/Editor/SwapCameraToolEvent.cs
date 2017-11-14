using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SwapCameraToolEvent : ScriptableWizard {
    private ArrayList sceneViews;
    Quaternion cameraNewRotation;
    Vector3 cameraNewPosition;
    GameObject gameObjecty;

    private GameObject cameraMain;
    private GameObject cameraLevel;

    [MenuItem("Tools/Inspection Camera")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard<SwapCameraToolEvent>("InspectionCamera Tool", "Swap Cameras", "Return back");//first and second buttons
    }

    private void Start()
    {
        
    }


    public void OnWizardCreate()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        cameraLevel = GameObject.FindGameObjectWithTag("Level Camera");
        //qaty = Camera.main.transform.rotation;
        //vecy = Camera.main.transform.position;
        //cameraNewRotation = cameraLevel.transform.rotation;
        //cameraNewPosition = cameraLevel.transform.position;

        cameraNewRotation = cameraMain.transform.rotation;
        cameraNewPosition = cameraMain.transform.position;

        Debug.Log("i am touched");
        GameObject newObject = new GameObject("CameraScriptHolder");
        newObject.AddComponent<InspectionCameraTool>();
    }

    public void OnWizardOtherButton()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        cameraLevel = GameObject.FindGameObjectWithTag("Level Camera");
        // Camera.main.transform.rotation = qaty;
        // Camera.main.transform.position = vecy;
        //cameraLevel.transform.rotation = cameraNewRotation;
        //cameraLevel.transform.position = cameraNewPosition;

        cameraMain.transform.rotation = cameraNewRotation;
        cameraMain.transform.position = cameraNewPosition;
        gameObjecty = GameObject.Find("CameraScriptHolder");
        DestroyImmediate(gameObjecty);

    }

}
