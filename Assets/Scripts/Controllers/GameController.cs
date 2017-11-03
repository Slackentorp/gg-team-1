using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private LightSourceInput startLight;
    [SerializeField]
    private MothBehaviour mothBehaviour; 
    [SerializeField]
    private CameraController camControl; 

    // Use this for initialization
    void Start()
    {
        if (startLight != null)
            EventBus.Instance.SetMothPosition(startLight.transform.TransformPoint(startLight.GetLandingPos()));

        camControl = GameObject.Find("Main Camera").GetComponent<CameraController>(); //GameObject.FindObjectOfType<CameraController>();
        camControl.TargetPos = mothBehaviour.transform;
    }

    private void Update()
    {
        if (camControl == null)
            camControl = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
}
