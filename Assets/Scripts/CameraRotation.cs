using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	[SerializeField, Tooltip("Controls the camera's turning speed.")]
	private float cameraTurnSpeed;
	Camera mainCamera;

	void Start()
	{
		mainCamera = Camera.main;
	}

	private void FixedUpdate()
	{
		RotateCameraAroundSelf();
	}


	private void RotateCameraAroundSelf()
	{
		mainCamera.transform.Rotate(0f, Input.GetAxis("Horizontal") * cameraTurnSpeed, 0f, Space.World);
		mainCamera.transform.Rotate(-Input.GetAxis("Vertical") * cameraTurnSpeed, 0f, 0f, Space.Self);
	}

}
	
	
	
