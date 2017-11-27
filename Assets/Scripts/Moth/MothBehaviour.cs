using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothBehaviour
{
	[SerializeField]
	private AnimationCurve MothFlightLerpCurve;

	[SerializeField]
	private float timeScale = 1;

	[SerializeField, Range(0, 1), Tooltip("Adjusts the allowed distance between moth and anchor point")]
	float allowedDistance = 0.2f;

	[SerializeField]
	private AnimationCurve mothChildCurve;

	[SerializeField]
	int noiseReducerMax = 16;

	int noiseReducerMin = 10;

	[SerializeField]
	float mothSpeedModifier = 1.0f;

	[SerializeField]
	float mothDistanceToObject = 0.2f;

	GameObject moth;
	Transform mothChild;
	Camera camera;
	Vector3 hitPoint, mothStartPos;
	RaycastHit hit;
	Vector3 hitDotPoint, hitDotNormal, mothRotation, dampVelocity, mothOriginPos, parentPos;
	Vector3 pos, anchorPointPlusPos;
	Ray ray;
	float turningTime, turningSpeed, proceduralLerpTime, time;
	float perlinNoiseX, perlinNoiseY, perlinNoiseZ, levelOfNoise = 0.5f;
	float distance;
	float velocity;

	public float MothSpeed
	{
		get; set;
	}

	private bool lerpRunning = false;
	private bool mothTurning = true;
	private bool mothDampProcedural = false;

	void OnValidate()
	{
		if (timeScale < 1)
		{
			timeScale = 1;
		}
	}

	public MothBehaviour(GameObject moth, Camera camera, float mothDistanceToObject, float MothSpeed, AnimationCurve curve,
						int noiseReducerMax, int noiseReducerMin, float speedModifier, AnimationCurve MothFlightLerpCurve)
	{
		this.moth = moth;
		this.camera = camera;
		this.mothDistanceToObject = mothDistanceToObject;
		this.MothSpeed = MothSpeed;
		this.mothChildCurve = curve;
		this.noiseReducerMax = noiseReducerMax;
		this.noiseReducerMin = noiseReducerMin;
		this.mothSpeedModifier = speedModifier;
		this.MothFlightLerpCurve = MothFlightLerpCurve;
	}


	public void Update()
	{
		if (mothChild == null)
		{
			mothChild = moth.transform.GetChild(0);
		}
		if (mothChild != null)
		{
			ProceduralMovement();
			MothGoToPosition();
		}
	}

	public void SetMothPos(RaycastHit hit)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		mothStartPos = moth.transform.position;
		mothRotation = moth.transform.forward;
		hitPoint = hit.point + hit.normal * mothDistanceToObject;

		DistancefromMothToHitpoint();
		lerpRunning = true;
		time = 0.0f;
		turningTime = 0.0f;
	}

	void MothGoToPosition()
	{
		turningSpeed = 1.7f;
		if (Vector3.Angle(moth.transform.forward, moth.transform.position - hitPoint) != 0)
		{
			turningTime += Time.deltaTime * (time * 2.0f);

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
				time += Time.deltaTime;
				moth.transform.position = Vector3.Lerp(mothStartPos, hitPoint, MothFlightLerpCurve.Evaluate((time/distance) * MothSpeed));
			}
		}
	}

	void DistancefromMothToHitpoint()
	{
		distance = Vector3.Distance(mothStartPos, hitPoint);
		//velocity = distance / MothSpeed;
		
	}

	private void PointfromRaycast()
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			hitPoint = hit.point + hit.normal * mothDistanceToObject;
			hitDotPoint = hit.point;
			hitDotNormal = hit.normal;

			lerpRunning = true;
		}
	}

	void MothTargetRotate()
	{
		turningTime += Time.deltaTime * turningSpeed;
		Vector3 nextPos = Vector3.Lerp(mothRotation, (moth.transform.position - hitPoint).normalized, turningTime);
		if (nextPos != Vector3.zero)
		{
			moth.transform.forward = nextPos;
		}
	}

	void ProceduralMovement()
	{
		parentPos = Vector3.zero;
		proceduralLerpTime += Time.deltaTime;

		if (mothDampProcedural == false)
		{
			mothOriginPos = mothChild.transform.localPosition;

			CalculatePerlinNoise();

			proceduralLerpTime = 0;
			anchorPointPlusPos = parentPos + pos;
			mothDampProcedural = true;
		}
		if (mothDampProcedural == true && proceduralLerpTime * 0.7 < 1)
		{
			mothChild.transform.localPosition = Vector3.Lerp(mothOriginPos,
			anchorPointPlusPos, mothChildCurve.Evaluate(proceduralLerpTime * mothSpeedModifier));
		}
		else if (mothDampProcedural == true)
		{
			mothDampProcedural = false;
		}
	}

	Vector3 CalculatePerlinNoise()
	{
		perlinNoiseX = Mathf.PerlinNoise(1.0f * Time.time, 0.0f);
		if (perlinNoiseX < 0.51f)
		{
			perlinNoiseX = -perlinNoiseX;
		}
		perlinNoiseY = Mathf.PerlinNoise(0.0f, 1.0f * Time.time);
		if (perlinNoiseY < 0.51f)
		{
			perlinNoiseY = -perlinNoiseY;
		}
		perlinNoiseZ = Mathf.PerlinNoise(Time.time * 1.0f, 1.0f * Time.time);
		if (perlinNoiseZ < 0.51f)
		{
			perlinNoiseZ = -perlinNoiseZ;
		}
		pos.x = perlinNoiseX / (Random.Range(noiseReducerMin, noiseReducerMax + 1));
		pos.y = perlinNoiseY / (Random.Range(noiseReducerMin, noiseReducerMax + 1)) -0.08f;
		pos.z = perlinNoiseZ / ((Random.Range(noiseReducerMin, noiseReducerMax + 1)* 1.5f));
		return pos;
	}
}
