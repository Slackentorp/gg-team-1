using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField, Tooltip("Sets the distance that the camera can move away from moth")]
	float maxDistance = 2.0f;
	[SerializeField, Tooltip("Decides the speed of which the camera follow the moth")]
	float followSpeed = 1.0f;
	[SerializeField, Tooltip("Determines the turn speed of the camera")]
	float cameraTurnSpeed = 1.0f;
	private Vector3 targetPos;
	private Vector3 targetRot;
	private Vector3 prevPos;
	

	public  Vector3 TargetPos
	{
		get
		{
			return targetPos;  
		}
		set
		{
			Vector3	tmp = targetPos;
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

	private bool TooFarFromMoth()
	{
		float dist = Vector3.Distance(transform.position, TargetPos);
		return dist > maxDistance;
	}

	// Use this for initialization
	void Start()
	{
		print(TargetPos);
		TargetPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider != null)
				{
					TargetPos = hit.transform.position;
				}
			}
		}

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

		if (Input.GetKey(KeyCode.Space))
		{
			TargetPos = prevPos;
		}

		if (targetPos == null)
		{
			return; 
		}
		transform.position = Vector3.Lerp(transform.position, TargetPos, 
										  followSpeed * Time.deltaTime
										  * (TooFarFromMoth() ? 1f : 0f));
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

	private void FixedUpdate()
	{
		RotateCameraAroundSelf();
	}

	private void RotateCameraAroundSelf()
	{
		if (Input.touchCount > 0)
		{
			transform.Rotate(0f,
				-Input.touches[0].deltaPosition.x * cameraTurnSpeed / 10 * -1,
				0f, Space.World);
			transform.Rotate(
				Input.touches[0].deltaPosition.y * cameraTurnSpeed / 10 * -1, 0f,
				0f, Space.Self);
		}

	}

}
