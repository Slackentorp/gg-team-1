using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class CameraRotation : Singleton<CameraRotation>
{
    [SerializeField, Tooltip("Controls the camera's turning speed.")]
    private float cameraTurnSpeed;

    private int invert = 1;
    
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
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
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            newAngleY =
                -Input.GetAxis("Mouse X") * cameraTurnSpeed * invert;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeed * invert;
        }
#endif
        if (Input.touchCount > 0)
        {
            newAngleY = -Input.touches[0].deltaPosition.x * cameraTurnSpeed /
                        10 *
                        invert;
            newAngleX = Input.touches[0].deltaPosition.y *
                        cameraTurnSpeed / 10 *
                        invert;
        }

        if (!(newAngleX > 0) && !(newAngleY > 0) && !(newAngleX < 0) &&
            !(newAngleY < 0))
        {
            return;
        }
        // Horizontal rotation(Yaw)
        Vector3 localUpAxis =
            transform.InverseTransformDirection(Vector3.up);
        mainCamera.transform.rotation *=
            Quaternion.AngleAxis(newAngleY, localUpAxis);

        // Vertical rotation (Pitch)
        Quaternion q = Quaternion.AngleAxis(newAngleX, Vector3.right);
        Quaternion finalRot = mainCamera.transform.rotation * q;
        Vector3 transformed = finalRot * Vector3.up;

        // Project the transformed vector onto the Right axis
        Vector3 flattened = transformed -
                            Vector3.Dot(transformed, Vector3.right) *
                            Vector3.right;
        flattened = flattened.normalized;
        float a = Mathf.Acos(Vector3.Dot(Vector3.up, flattened));

        if (a < 1.5f) {
            mainCamera.transform.rotation *= q;
        }
    }
}