using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using UnityEngine;

public class CameraController
{
    [SerializeField, Tooltip("Sets the distance that the camera can move away from moth")]
    private float maxDistance = 2.0f;
    [SerializeField, Tooltip("Decides the speed of which the camera follow the moth")]
    private float followSpeed = 1.0f;
    [SerializeField, Tooltip("Determines the turn speed of the camera's Y-axis")]
    private float cameraTurnSpeedY = 1.0f;
    [SerializeField, Tooltip("Determines the turn speed of the camera's X-axis")]
    private float cameraTurnSpeedX = 1.0f;
    [SerializeField]
    float minimumVerticalAngle = 20.0f;
    [SerializeField]
    float maximumVerticalAngle = 160.0f;
    [SerializeField]

    private Transform targetPos;
    private Vector3 targetRot;
    private Transform prevPos;
    private Transform transform;
    private float damping = 0.2f;
    private Vector3 currentVelocity;

    public bool isDebug = true;
    public static bool isMouseTouchingObject;
    public Vector3 heading { get; private set; }
    public Vector3 InitialHeading { get { return initialHeading; } }
    Vector3 initialHeading;
    private float initialMagnitude;
    Vector3 flattened;
    private bool fragmentMode;
    private int collisionLayermask;
    
    float GnewAngleY = 0, GnewAngleX = 0;

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
        Transform target, bool fragmentMode, float damping, float cameraTurnSpeedY,
        float cameraTurnSpeedX, float minimumVerticalAngle, float maximumVerticalAngle)
    {
        this.transform = transform;
        this.maxDistance = maxDistance;
        this.followSpeed = followSpeed;
        this.targetPos = target;
        this.fragmentMode = fragmentMode;
        this.damping = damping;
        this.cameraTurnSpeedY = cameraTurnSpeedY;
        this.cameraTurnSpeedX = cameraTurnSpeedX;
        this.minimumVerticalAngle = minimumVerticalAngle;
        this.maximumVerticalAngle = maximumVerticalAngle;

        Vector3 reference = transform.rotation.eulerAngles;
        reference.z = 0;
        transform.rotation = Quaternion.Euler(reference);
        this.heading = heading;

        initialHeading = heading;
        initialMagnitude = initialHeading.magnitude;

        int touchObjectLayer = LayerMask.NameToLayer("Touch Object");
        collisionLayermask = (1 << touchObjectLayer);
        collisionLayermask = ~collisionLayermask;
    }

    public void Update()
    {
        Debug.DrawLine(targetPos.position, targetPos.position + heading, Color.blue);

        if (fragmentMode)
        {
            RotateCameraAroundSelf();
        }
        else
        {
            RotateAroundMoth();

            // Camera collision
            RaycastHit hit;
            if (Physics.Raycast(targetPos.position, heading.normalized, out hit, initialMagnitude, collisionLayermask))
            {
                heading = hit.point - targetPos.position;
                float magn = heading.magnitude;
                if (magn - 0.2f > 0.2f)
                {
                    heading = heading.ResizeMagnitude(magn - 0.2f);
                }
                heading = Vector3.ClampMagnitude(heading, initialMagnitude);
            }
            else
            {
                heading = heading.ResizeMagnitude(initialMagnitude);
            }

            Vector3 nextPos = Vector3.SmoothDamp(transform.position, heading + targetPos.position, ref currentVelocity, damping);

            transform.position = nextPos;

            if (heading.normalized != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(-heading.normalized);
                targetPos.rotation = Quaternion.LookRotation(heading);
            }
        }

    }

    private void RotateCameraAroundSelf()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY = -Input.GetAxis("Mouse X") * cameraTurnSpeedY;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeedX;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY = -Input.touches[0].deltaPosition.x * cameraTurnSpeedY /
                10;
            newAngleX = Input.touches[0].deltaPosition.y *
                cameraTurnSpeedX / 10;
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

    public void SetFragmentMode(bool state)
    {
        fragmentMode = state;
    }

    void RotateAroundMoth()
    {
        float newAngleY = 0, newAngleX = 0;
#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            GnewAngleY += -Input.GetAxis("Mouse X") * cameraTurnSpeedY;
            GnewAngleX += Input.GetAxis("Mouse Y") * cameraTurnSpeedX;
            newAngleY = -Input.GetAxis("Mouse X") * cameraTurnSpeedY;
            newAngleX = Input.GetAxis("Mouse Y") * cameraTurnSpeedX;
        }
#endif
        if (Input.touchCount > 0 && !InputManager.isTouchingObject && !isMouseTouchingObject)
        {
            newAngleY = -Input.touches[0].deltaPosition.x * cameraTurnSpeedY /
                10;
            newAngleX = Input.touches[0].deltaPosition.y *
                cameraTurnSpeedX / 10;
        }
/* 
        if (!(newAngleX > 0) && !(newAngleY > 0) && !(newAngleX < 0) &&
            !(newAngleY < 0))
        {
            return;
        }
        */

        // Yaw
        heading = Quaternion.AngleAxis(newAngleY, transform.up) * heading;

        // Pitch
        Vector3 nextHeading = Quaternion.AngleAxis(newAngleX, transform.right) * heading;


        float a = Vector3.Angle(nextHeading, Vector3.up);
        if(a > 10 && a < 170)
        {
            heading = nextHeading;
        }



      //  Quaternion rotation = Quaternion.Euler(currentY, currentX, 0); camTransform.position = lookAt.position + rotation * new;

     //   GnewAngleX = Mathf.Clamp(GnewAngleX, -89, 89);
    //    Vector3 v = new Vector3(GnewAngleX, GnewAngleY, 0);
  
    //    heading = Quaternion.Euler(v) * new Vector3(0,0,1).ResizeMagnitude(initialHeading.magnitude);

    //    targetPos.rotation = Quaternion.LookRotation(heading);

        // Camera rotation
        
       // transform.rotation = Quaternion.LookRotation(-heading.normalized);

       /* Vector3 nextHeading = q * heading;
        float a = Vector3.Angle(nextHeading, Vector3.up);
        if (a >= 20 && a <= 160)
        {
            heading = nextHeading;
        }*/

      //  transform.rotation = Quaternion.LookRotation(-heading.normalized);

      /* THIS WORKS
      
        GnewAngleX = Mathf.Clamp(GnewAngleX, -89, 89);
        Vector3 v = new Vector3(GnewAngleX, GnewAngleY, 0);
  
        heading = Quaternion.Euler(v) * new Vector3(0,0,1).ResizeMagnitude(initialHeading.magnitude);
         */
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

    public void SetHeading(Vector3 h)
    {
        heading = h;
    }
}

public static class Vector3Extensions
{
    public static Vector3 ResizeMagnitude(this Vector3 vector, float magnitude)
    {
        return vector.normalized * magnitude;
    }

    public static Vector3 Absolute(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

}