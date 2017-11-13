using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class SwapCameraToolEvent : ScriptableWizard {
    private ArrayList sceneViews;
    Quaternion qaty;
    Vector3 vecy;
    GameObject gameObjecty;
    [MenuItem("Tools/Inspection Camera")]
    static void SelectAllOfTagWizard()
    {
        ScriptableWizard.DisplayWizard<SwapCameraToolEvent>("InspectionCamera Tool", "Swap Cameras", "Return back");//first and second buttons
    }

    public void OnWizardCreate()
    {
        qaty = Camera.main.transform.rotation;
        vecy = Camera.main.transform.position;
        GameObject newObject = new GameObject("CameraScriptHolder");
        newObject.AddComponent<InspectionCameraTool>();
    }

    public void OnWizardOtherButton()
    {
        Camera.main.transform.rotation = qaty;
        Camera.main.transform.position = vecy;
        gameObjecty = GameObject.Find("CameraScriptHolder");
        DestroyImmediate(gameObjecty);

    }

}
