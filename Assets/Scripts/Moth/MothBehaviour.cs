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
	RaycastHit hit;
	Vector3 hitDotPoint;
	Vector3 hitDotNormal;
	Ray ray;

	public float MothSpeed
	{
		get; set;
	}

	private bool inTransit;
	private bool lerpRunning = false;

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
		Debug.DrawLine(hitPoint, hitPoint + hitDotPoint, Color.red);
		Debug.DrawLine(hitPoint, hitPoint + hitDotNormal, Color.blue);

		MothGoToPosition();
	}

	public void SetMothPos(RaycastHit hit)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		MothSpeed = 0.3f;
		mothStartPos = moth.transform.position;

		hitPoint = hit.point + hit.normal * 0.2f;
		hitDotPoint = hit.point;
		hitDotNormal = hit.normal;
		
		lerpRunning = true;
		time = 0.0f;
	}

	void MothGoToPosition()
	{
		if (lerpRunning)
		{
			time += Time.deltaTime * MothSpeed;
			moth.transform.position = Vector3.Lerp(mothStartPos, hitPoint, time);
		}
		if (moth.transform.position == hitPoint && lerpRunning == true)
		{
			AkSoundEngine.PostEvent("MOTH_END_FLIGHT", moth);
			time = 0.0f;
			lerpRunning = false;
		}
	/*	if (Input.GetMouseButton(1))
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
