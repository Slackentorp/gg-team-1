using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[CustomEditor(typeof(Fragment))]
public class FragmentEditor : Editor
{
    private Camera cameraObject;
    private Vector3 camPos;
    private Quaternion camRot;
    private bool follow = false;
    private bool didEnterPlayMode = false; 
    Fragment fragment;

    private void OnEnable()
    {
        fragment = (Fragment)target;

        if (GameObject.Find("Main Camera") != null)
            cameraObject = GameObject.Find("Main Camera").GetComponent<Camera>();
        else if (GameObject.FindObjectsOfType<Camera>().Length > 0)
            cameraObject = GameObject.FindObjectsOfType<Camera>()[0];
    }


    public override void OnInspectorGUI()
    {
        GUILayout.Label("Default Editor Layout"); 
        DrawDefaultInspector();
        if (GUILayout.Button("Reset Camera Transforms"))
        {
            fragment.CamPosition = Vector3.zero;
            fragment.CamOrientaion = Vector3.zero;
        }

        EditorGUILayout.Space();
        GUILayout.Label("Camera Preview Controls"); 

        if (GUILayout.Button("Preview Camera"))
        {
            camPos = cameraObject.transform.position;
            camRot = cameraObject.transform.rotation;
            cameraObject.transform.position = fragment.transform.position + fragment.CamPosition;
            cameraObject.transform.forward = fragment.CamForward;
            follow = true;

        }

        if (follow)
        {
            cameraObject.transform.position = fragment.transform.position + fragment.CamPosition;
            if (Mathf.Abs(fragment.CamForward.magnitude) > 0)
                cameraObject.transform.forward = fragment.CamForward;
        }

        if (GUILayout.Button("Reset Camera"))
        {
            ResetCamera();
        }

        if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode && !didEnterPlayMode)
        {
            didEnterPlayMode = true; 
        }
        
    }

    private void ResetCamera()
    {
        cameraObject.transform.position = camPos;
        cameraObject.transform.rotation = camRot;
        follow = false;
    }
}
