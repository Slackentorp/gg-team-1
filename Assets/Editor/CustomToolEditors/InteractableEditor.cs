using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : Editor
{
    private Interactable interactable;

    private Vector3 cameraPosition;
    private Quaternion cameraRotation;
    private bool follow = false;
    private bool didEnterPlayMode = false;
    private Camera camera;

    private void OnEnable()
    {
        interactable = (Interactable)target;
        if (GameObject.Find("Main Camera") != null)
            camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        else if (GameObject.FindObjectsOfType<Camera>().Length > 0)
            camera = GameObject.FindObjectsOfType<Camera>()[0];

        cameraPosition = camera.transform.position;
        cameraRotation = camera.transform.rotation;
    }

    private void OnDisable()
    {
        ResetCamera();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Default Editor Layout");
        DrawDefaultInspector();
        GUILayout.Label("Custom Editor");

        GUI.backgroundColor = follow ? Color.grey : Color.white;
        if (GUILayout.Button("Toggle Preview Camera"))
        {
            follow = !follow;

            if (follow)
            {
                cameraPosition = camera.transform.position;
                cameraRotation = camera.transform.rotation;
                camera.transform.position = interactable.transform.position + interactable.CamPosition;
                camera.transform.forward = interactable.CamForward;
            }
            else
            {
                ResetCamera();
            }

        }

        if (follow)
        {
            camera.transform.position = interactable.transform.position + interactable.CamPosition;
            if (Mathf.Abs(interactable.CamForward.magnitude) > 0)
                camera.transform.forward = interactable.CamForward;
        }

        if (EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode && !didEnterPlayMode)
        {
            didEnterPlayMode = true;
        }
    }

    private void ResetCamera()
    {
        camera.transform.position = cameraPosition;
        camera.transform.rotation = cameraRotation;
        follow = false;
    }

}
