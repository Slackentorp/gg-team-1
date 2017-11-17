using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class CameraController
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
    private Transform transform;

    private float damping = 0.2f;
    private Vector3 currentVelocity;

    [SerializeField]
    private bool storyCam = true;

    public bool isDebug = true;
    public static bool isMouseTouchingObject;
    public Vector3 heading { get; private set; }
    Vector3 initialHeading;
    Vector3 flattened;
    private bool fragmentMode;

    Vector3 headingProjected, upProjected;

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

    public CameraController(Transform transform, float maxDistance, Vector3 heading, float followSpeed,
                            float cameraTurnSpeed, Transform target, bool fragmentMode, float damping)
    {
        this.transform = transform;
        this.maxDistance = maxDistance;
        this.followSpeed = followSpeed;
        this.cameraTurnSpeed = cameraTurnSpeed;
        this.targetPos = target;
        this.fragmentMode = fragmentMode;
        this.damping = damping;

        Vector3 reference = transform.rotation.eulerAngles;
        reference.z = 0;
        transform.rotation = Quaternion.Euler(reference);
        this.heading = heading;
    }

    void Start()
    {
        if (TargetPos == null)
            TargetPos = transform;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            storyCam = !storyCam;
        }

        if (fragmentMode)
        {
            RotateCameraAroundSelf();
        }
        else
        {
            RotateAroundMoth();

            Vector3 nextPos = Vector3.SmoothDamp(transform.position, heading + targetPos.position, ref currentVelocity, damping);

			transform.position = nextPos;
            transform.rotation = Quaternion.LookRotation(-heading.normalized);
            targetPos.rotation = Quaternion.LookRotation(heading);
        }

    }

    private void RotateCameraAroundSelf()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY =
                -Input.GetAxis("Mouse X") * cameraTurnSpeed;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeed;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.isTouchingObject && !isMouseTouchingObject)
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

    void RotateAroundMoth()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY =
                -Input.GetAxis("Mouse X") * cameraTurnSpeed;
            newAngleX += Input.GetAxis("Mouse Y") * cameraTurnSpeed;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.isTouchingObject && !isMouseTouchingObject)
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
        // Yaw
        Vector3 localUpAxis =
            transform.InverseTransformDirection(Vector3.up);
        heading = Quaternion.AngleAxis(newAngleY, localUpAxis) * heading;

        // Pitch
        Quaternion q = Quaternion.AngleAxis(newAngleX, transform.right);



        Vector3 nextHeading = q * heading;
        float a = Vector3.Angle(nextHeading, Vector3.up);
        if (a >= 20 && a <= 160)
        {
            heading = nextHeading;
        }

    }

    public void SetTarget(Vector3 newTarget)
    {
        TargetPos.position = newTarget;
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
        transform.position = Vector3.Lerp(transform.position, TargetPos.position * maxDistance,
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
