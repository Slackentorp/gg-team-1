using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class CameraRotation : Singleton<CameraRotation>
{
    [SerializeField, Tooltip("Controls the camera's turning speed.")]
    private float cameraTurnSpeed;

    private Vector3 mousePos, mouseOrigin;
    private int invert = 1;
    
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        RotateCameraAroundSelf();
    }

    public void InvertRotation()
    {
        invert *= -1;
    }

    private void RotateCameraAroundSelf()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            mainCamera.transform.Rotate(0f,
                -Input.GetAxis("Mouse X") * cameraTurnSpeed * invert,
                0f, Space.World);
            mainCamera.transform.Rotate(
                Input.GetAxis("Mouse Y") * cameraTurnSpeed * invert, 0f,
                0f, Space.Self);
        }
#endif
        if (Input.touchCount > 0)
        {
            mainCamera.transform.Rotate(0f,
                -Input.touches[0].deltaPosition.x * cameraTurnSpeed / 10 *
                invert,
                0f, Space.World);
            mainCamera.transform.Rotate(
                Input.touches[0].deltaPosition.y * cameraTurnSpeed / 10 *
                invert, 0f,
                0f, Space.Self);
        }
    }
}