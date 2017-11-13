using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class CameraController
{
    float maxDistance = 2.0f;
    float followSpeed = 1.0f;
    float cameraTurnSpeed = 1.0f;

    private Transform targetPos;
    private Vector3 targetRot;
    private Transform prevPos;
    private Transform transform;

    /*[SerializeField]
    private Transform menuPosition;*/

    [SerializeField]
    private bool storyCam = true;

    public bool isDebug = true;
    public static bool isMouseTouchingObject;


    public CameraController(Transform transform, float maxDistance, float followSpeed, float cameraTurnSpeed, Transform target)
    {
        this.transform = transform;
        this.maxDistance = maxDistance;
        this.followSpeed = followSpeed;
        this.cameraTurnSpeed = cameraTurnSpeed;
        this.targetPos = target;

        Vector3 reference = transform.rotation.eulerAngles;
        reference.z = 0;
        transform.rotation = Quaternion.Euler(reference);
    }

    public Transform TargetPos
    {
        get
        {
            return targetPos;
        }
        set
        {
            Transform tmp = targetPos;
            targetPos = value;
            prevPos = tmp;
        }
    }

    public Vector3 TargetRot
    {
        get
        {
            return targetRot;
        }
        set
        {
            targetRot = value;
        }
    }

    public void Update()
    {
        CameraRotation();
        MoveTowardsTarget();
    }

    public void SetStoryCam(bool val)
    {
        storyCam = val;
    }

    private void RotateCameraAroundSelf()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !InputManager.Instance.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY =
                -Input.GetAxis("Mouse X") * cameraTurnSpeed;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeed;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.Instance.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY = -Input.touches[0].deltaPosition.x * cameraTurnSpeed /
                        10;
            newAngleX = Input.touches[0].deltaPosition.y *
                        cameraTurnSpeed / 10;
        }

        if (!(newAngleX > 0) && !(newAngleY > 0) && !(newAngleX < 0) &&
            !(newAngleY < 0))
        {
            return;
        }
        // Horizontal rotation(Yaw)
        Vector3 localUpAxis =
            transform.InverseTransformDirection(Vector3.up);
        transform.rotation *=
            Quaternion.AngleAxis(newAngleY, localUpAxis);

        // Vertical rotation (Pitch)
        Quaternion q = Quaternion.AngleAxis(newAngleX, Vector3.right);
        Quaternion finalRot = transform.rotation * q;
        Vector3 transformed = finalRot * Vector3.up;

        // Project the transformed vector onto the Right axis
        Vector3 flattened = transformed -
                            Vector3.Dot(transformed, Vector3.right) *
                            Vector3.right;
        flattened = flattened.normalized;
        float a = Mathf.Acos(Vector3.Dot(Vector3.up, flattened));

        if (a < 1.5f)
        {
            transform.rotation *= q;
        }

    }

    public void SetTarget(Vector3 newTarget)
    {
        TargetPos.position = newTarget; 
    }


    public void SetStoryTarget(Transform target)
    {
        if (isDebug) return;
        TargetPos = target;
    }

    public void SetStoryTarget(Vector3 target)
    {
        if (isDebug) return;
        TargetPos.position = target;
    }

    public void SetStoryTarget(Vector3 target, Vector3 orientation)
    {
        if (isDebug) return;
        TargetPos.position = target;
        TargetRot = orientation;
    }

    public void SetStoryRotation(Vector3 dir)
    {
        TargetRot = dir;
    }

    public void ResetControls()
    {
        TargetPos = prevPos;
        TargetRot = Vector3.zero;
    }

    private void ReturnToPreStoryPoint()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TargetPos = prevPos;
        }
    }

    private void CheckIfTargetPosIsNull()
    {
        if (targetPos == null)
        {
            return;
        }
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPos.position,
                                          followSpeed * Time.deltaTime);
    }

    private void CameraRotation()
    {
        if (Mathf.Abs(TargetRot.magnitude) > 0.1f)
        {
            transform.forward = Vector3.Lerp(transform.forward, TargetRot,
                                             cameraTurnSpeed * Time.deltaTime);
        }
        else
        {
            RotateCameraAroundSelf();
        }
    }
}
