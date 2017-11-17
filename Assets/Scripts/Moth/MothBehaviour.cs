using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothBehaviour
{
	[SerializeField]
	private AnimationCurve lerpCurve;
	[SerializeField]
	private float timeScale = 1;

	GameObject moth;
	Camera camera;
	Vector3 hitPoint;
	Vector3 mothToPoint;
	Vector3 mothStartPos;
	float time;
	float turningTime;
	float turningSpeed;
	RaycastHit hit;
	Vector3 hitDotPoint;
	Vector3 hitDotNormal;
	Vector3 mothRotation;
	Ray ray;

	public float MothSpeed
	{
		get; set;
	}

	private bool lerpRunning = false;
	private bool mothTurning = true;

	void OnValidate()
	{
		if (timeScale < 1)
		{
			timeScale = 1;
		}
	}

	public MothBehaviour(GameObject moth, Camera camera, float speed)
	{
		this.moth = moth;
		this.camera = camera;
		this.MothSpeed = speed;
	}


	public void Update()
	{
		MothGoToPosition();
	}

	public void SetMothPos(RaycastHit hit)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		MothSpeed = 0.3f;
		mothStartPos = moth.transform.position;
		mothRotation = moth.transform.forward;
		hitPoint = hit.point + hit.normal * 0.2f;

		lerpRunning = true;
		time = 0.0f;
		turningTime = 0.0f;
	}

	void MothGoToPosition()
	{
		turningSpeed = 1.7f;
		if (Vector3.Angle(moth.transform.forward, moth.transform.position - hitPoint) != 0)
		{
			MothTargetRotate();
		}
		if (Vector3.Angle(moth.transform.forward, moth.transform.position - hitPoint) == 0 || lerpRunning == true)
		{
			if (moth.transform.position == hitPoint && lerpRunning == true)
			{
				AkSoundEngine.PostEvent("MOTH_END_FLIGHT", moth);
				time = 0.0f;
				lerpRunning = false;
			}
			if (lerpRunning)
			{
				time += Time.deltaTime * MothSpeed;
				moth.transform.position = Vector3.Lerp(mothStartPos, hitPoint, time);
			}
		}

	}

	private void PointfromRaycast()
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			hitPoint = hit.point + hit.normal * 0.2f;
			hitDotPoint = hit.point;
			hitDotNormal = hit.normal;

			lerpRunning = true;
		}
	}

	void MothTargetRotate()
	{
		turningTime += Time.deltaTime * turningSpeed;

		moth.transform.forward = Vector3.Lerp(mothRotation, (moth.transform.position - hitPoint).normalized, turningTime);
	}
}
