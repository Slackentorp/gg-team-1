using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
	[SerializeField, Tooltip("Controls the camera's turning speed.")]
	private float cameraTurnSpeed;
	private Vector3 mousePos, mouseOrigin;
	//TouchInput touchInput; 
	Camera mainCamera;

	void Start()
	{
		//touchInput = GetComponent<TouchInput>();
		mainCamera = Camera.main;
	}

	private void FixedUpdate()
	{
		RotateCameraAroundSelf();
	}


	private void RotateCameraAroundSelf()
	{
		if (MouseClicked())
		{
			if (UnityEditor.EditorApplication.isRemoteConnected && Input.touchCount > 0)
			{
				mainCamera.transform.Rotate(0f, Input.touches[0].deltaPosition.x * cameraTurnSpeed, 0f, Space.World);
				mainCamera.transform.Rotate(-Input.touches[0].deltaPosition.y * cameraTurnSpeed, 0f, 0f, Space.Self);
			}
			else
			{
				mainCamera.transform.Rotate(0f, Input.GetAxis("Mouse X") * cameraTurnSpeed, 0f, Space.World);
				mainCamera.transform.Rotate(-Input.GetAxis("Mouse Y") * cameraTurnSpeed, 0f, 0f, Space.Self);
			}
		}
	}

	private bool MouseClicked()
	{
		if (Input.GetMouseButton(0))
		{
			return true;
		}

		return false; 
	}

}
	
	
	
