using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;

public class CameraTranslation : MonoBehaviour
{

	[SerializeField, Tooltip("Specifies the object the camera follows")]
	GameObject moth;
	[SerializeField]
	Camera camera;
	[SerializeField, Tooltip("Sets the distance that the camera can move away from moth")]
	float maxDistance;
	[SerializeField, Tooltip("Decides the speed of which the camera follow the moth")]
	float followSpeed;
	[SerializeField, Tooltip("Determines the speed of the camera from point a, to point b.")]
	float cameraMoveSpeed = 0.2f;
	[SerializeField, Tooltip("Determines the turn speed of the camera")]
	float cameraTurnSpeed = 0.12f;
	[SerializeField, Tooltip("The point that the camera should turn to.")]
	Transform cameraPOI;
	[SerializeField, Tooltip("Specifies the position that should be used for as " +
							 "the camera's position for the story.")]
	Transform storyPos;
	[SerializeField, Tooltip("The ID for the different Types of locks")]
	int lockTypeID = 0;

	private Vector3 mousePos, mouseOrigin;
	private int invert = 1;

	float difference;
	Vector3 cameraStartPos;
	bool isMoving = false;
	bool isLocked = false;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Debug.Log("Left button pressed");
			StoryObjectClicked();
		}
	}

	private void FixedUpdate()
	{
		RotateCameraAroundSelf();

	}

	void LateUpdate()
	{
		if (moth != null && isLocked == false)
		{
			difference = Vector3.Distance(camera.transform.position, moth.transform.position);
			AboveDistance();
		}

	}

	void AboveDistance()
	{
		if (difference * maxDistance > maxDistance)
		{
			camera.transform.position = Vector3.Lerp(camera.transform.position, moth.transform.position, followSpeed * Time.deltaTime);
		}
		else
		{
			return;
		}
	}

	public void SetMoth(GameObject target)
	{
		moth = target;
	}

	public GameObject GetMoth()
	{
		return moth;
	}

	private void StoryObjectClicked()
	{
		if (isMoving) return;
		isMoving = true;
		isLocked = true;
		cameraStartPos = camera.transform.position;
		Debug.Log("Story Object clicked");
		StartCoroutine(CameraPosChange());
		StartCoroutine(CameraRotationChange());
		isMoving = false;
	}

	IEnumerator CameraPosChange()
	{
		float time = 0.0f;
		while (time < 1)
		{
			time += cameraMoveSpeed * Time.deltaTime;
			transform.position = Vector3.Lerp(cameraStartPos, storyPos.transform.position, time);

			yield return null;
		}
		cameraStartPos = camera.transform.position;
		Debug.Log("Camera Position Changed");
	}

	IEnumerator CameraRotationChange()
	{
		float time = 0.0f;
		while (time < 1)
		{
			time += cameraTurnSpeed * Time.deltaTime;
			transform.forward = Vector3.Lerp(storyPos.transform.position, cameraPOI.transform.position, Time.deltaTime * cameraTurnSpeed);

			yield return null;
		}
		Debug.Log("Camera rotation changed");
	}

	bool PosAndRotateLock(int lockTypeID)
	{
		if (lockTypeID == 0)
		{
			print("Position locked");
			return false;
		}
		else
		{
			print("Position and Rotation Locked");
			return true;
		}
	}

	public void InvertRotation()
	{
		invert *= -1;
	}

	private void RotateCameraAroundSelf()
	{
		if (Input.touchCount > 0)
		{
			camera.transform.Rotate(0f,
				-Input.touches[0].deltaPosition.x * cameraTurnSpeed / 10 * invert,
				0f, Space.World);
			camera.transform.Rotate(
				Input.touches[0].deltaPosition.y * cameraTurnSpeed / 10 * invert, 0f,
				0f, Space.Self);
		}

		if (Input.GetMouseButtonDown(0))
		{
			camera.transform.Rotate(0f,
				-Input.mousePosition.x * cameraTurnSpeed / 10 * invert,
				0f, Space.World);
			camera.transform.Rotate(
				Input.GetAxis("MOUSE y") * cameraTurnSpeed / 10 * invert, 0f,
				0f, Space.Self);
		}

	}


}
