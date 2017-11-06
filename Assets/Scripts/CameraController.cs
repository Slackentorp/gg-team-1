using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Sets the distance that the camera can move away from moth")]
    float maxDistance = 2.0f;
    [SerializeField, Tooltip("Decides the speed of which the camera follow the moth")]
    float followSpeed = 1.0f;
    [SerializeField, Tooltip("Determines the turn speed of the camera")]
    float cameraTurnSpeed = 1.0f;
    [SerializeField]
    private Transform targetPos;
    private Vector3 targetRot;
    private Transform prevPos;


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

    private bool TooFarFromTarget()
    {
        float dist = Vector3.Distance(transform.position, TargetPos.position);
        return dist > maxDistance;
    }

    // Use this for initialization
    void Start()
    {
        if (TargetPos == null)
            TargetPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCameraToStoryBit();

        //RotateCameraTowardsObject();

        //ReturnToPreStoryPoint();

        CheckIfTargetPosIsNull();

        MoveTowardsTarget();

        CameraRotation();
    }

    private void FixedUpdate()
    {
        //RotateCameraAroundSelf();
    }

    private void RotateCameraAroundSelf()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            newAngleY =
                -Input.GetAxis("Mouse X") * cameraTurnSpeed;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeed;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.Instance.isTouchingObject)
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


    public void SetStoryTarget(Vector3 target)
    {
        TargetPos.position = target;
    }

    public void SetStoryTarget(Vector3 target, Vector3 orientation)
    {
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

    private void MoveCameraToStoryBit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    TargetPos = hit.transform;
                }
            }
        }
    }
    private void RotateCameraTowardsObject()
    {

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    TargetRot = (hit.transform.position - transform.position).normalized;
                }
            }
        }
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
                                          followSpeed * Time.deltaTime
                                          * (TooFarFromTarget() ? 1f : 0f));
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
