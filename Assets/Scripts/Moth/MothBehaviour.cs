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
	RaycastHit hit;
	Vector3 hitDotPoint;
	Vector3 hitDotNormal;
	Vector3 mothForwardRotation;
	Ray ray;

	public float MothSpeed
	{
		get; set;
	}

	private bool inTransit;
	private bool lerpRunning = false;
	//private bool mothTurning = true;

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

		hitPoint = hit.point + hit.normal * 0.2f;

		lerpRunning = true;
		time = 0.0f;
		turningTime = 0.0f;
	}

	void MothGoToPosition()
	{
		if (moth.transform.forward != hitPoint)
		{
			//mothTurning = true;
			turningTime += Time.deltaTime * (time * 2f);
			
			Vector3 nextPos = moth.transform.position - hitPoint;
			if(nextPos != Vector3.zero)
			{
				moth.transform.forward = nextPos;
			}
		}
		
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



		/*if (Input.GetMouseButton(1))
			{
				MothSpeed = 0.3f;

				mothStartPos = moth.transform.position;
				PointfromRaycast();
				time = 0.0f;
			}*/
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
}
