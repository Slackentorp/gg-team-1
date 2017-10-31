using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProxy : MonoBehaviour {

    public void InvertCamera()
    {
        CameraRotation.Instance.InvertRotation();
    }
}
