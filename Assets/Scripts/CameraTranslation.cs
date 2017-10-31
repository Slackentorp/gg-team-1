using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslation : MonoBehaviour {

	[SerializeField, Tooltip("Specifies the object the camera follows")]
	GameObject moth;
	[SerializeField, Tooltip("Sets the distance that the camera can move away from moth")]
	float maxDistance;
	[SerializeField, Tooltip("Decides the speed of which the camera follow the moth")]
	float followSpeed;
	float difference; 
	Camera mainCamera;
	
	void Start ()
	{
		mainCamera = Camera.main;
	}

	void LateUpdate ()
	{
		difference  = Vector3.Distance(mainCamera.transform.position, moth.transform.position);
		AboveDistance();
	}

	void AboveDistance()
	{
		if (difference * maxDistance > maxDistance)
		{
			mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, moth.transform.position, followSpeed * Time.deltaTime);
		}
		else
		{
			mainCamera.transform.position = mainCamera.transform.position;
		}
	}

}
