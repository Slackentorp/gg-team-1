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

	[SerializeField, Range(0, 1), Tooltip("Adjusts the allowed distance between moth and anchor point")]
	float allowedDistance = 0.2f;

	[SerializeField]
	private AnimationCurve mothChildCurve;

	[SerializeField]
	int noiseReducer = 16;

	[SerializeField]
	float mothSpeedModifier = 1.0f;

	GameObject moth;
	Camera camera;
	Vector3 hitPoint;
	Vector3 mothToPoint;
	Vector3 mothStartPos;
	float time;
	float timePerlin;
	float turningTime;
	float turningSpeed;
	RaycastHit hit;
	Vector3 hitDotPoint;
	Vector3 hitDotNormal;
	Vector3 mothRotation;
	Vector3 dampVelocity, mothOriginPos, parentPos; 
	Ray ray;
	float perlinNoiseX, perlinNoiseY, perlinNoiseZ, mothXAxisScale = 0.7f, mothYAxisScale = 0.7f;
	float levelOfNoise = 0.5f;
	float mothYOriginPos, distanceToMothChild;
	Vector3 pos, currentPos, AnchorPointPlusPos;
	Transform mothChild;


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

	public MothBehaviour(GameObject moth, Camera camera, float speed, AnimationCurve curve, 
						int noiseReducer, float speedModifier)
	{
		this.moth = moth;
		this.camera = camera;
		this.MothSpeed = speed;
		this.mothChildCurve = curve;
		this.noiseReducer = noiseReducer;
		this.mothSpeedModifier = speedModifier;
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
			Debug.DrawRay(Vector3.zero, Vector3.up * 4, Color.green);
			Debug.DrawRay(Vector3.zero, Vector3.right * 4, Color.red);
		}
	}

	public void SetMothPos(RaycastHit hit)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		MothSpeed = 0.3f;
		mothStartPos = moth.transform.position;
		mothRotation = moth.transform.forward;
		hitPoint = hit.point + hit.normal * 0.2f;
		//mothXOriginPos = moth.transform.localPosition.x;
		//mothYOriginPos = moth.transform.localPosition.y;

		lerpRunning = true;
		time = 0.0f;
		turningTime = 0.0f;
	}

	void MothGoToPosition()
	{
		turningSpeed = 1.7f;
		if (Vector3.Angle(moth.transform.forward, moth.transform.position - hitPoint) != 0)
		{
			turningTime += Time.deltaTime * (time * 2f);

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
		Vector3 nextPos = Vector3.Lerp(mothRotation, (moth.transform.position - hitPoint).normalized, turningTime);
		if (nextPos != Vector3.zero)
		{
			moth.transform.forward = nextPos;
		}
	}

	void ProceduralMovement()
	{
		parentPos = moth.transform.position;
		timePerlin += Time.deltaTime;
		Debug.Log(mothDampProcedural);
		if (mothDampProcedural == false)
		{
			mothOriginPos = mothChild.transform.localPosition;
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
			perlinNoiseZ = Mathf.PerlinNoise(Time.time, 1.0f * Time.time);
			if (perlinNoiseZ < 0.51f)
			{
				perlinNoiseZ = -perlinNoiseZ;
			}
			pos.x = perlinNoiseX / noiseReducer;
			pos.y = perlinNoiseY / noiseReducer;
			pos.z = perlinNoiseZ / (noiseReducer * 1.5f);


			mothDampProcedural = true;
			timePerlin = 0;
			AnchorPointPlusPos = parentPos + pos;

		}
		if (mothDampProcedural == true && timePerlin * 2 < 1)
		{
			mothChild.transform.localPosition = Vector3.Lerp(mothOriginPos,
										  AnchorPointPlusPos, mothChildCurve.Evaluate(timePerlin * mothSpeedModifier));
			//currentPos = mothChild.transform.localPosition;
		}
		/*if (DistanceToParent() == false)
		{
			//orignalPos = mothChild.transform.localPosition;

		}*/
		else if (mothDampProcedural == true)
		{
			mothDampProcedural = false;
		}
		Debug.Log(mothDampProcedural);
	}

	bool DistanceToParent()
	{
		distanceToMothChild = Vector3.Distance(moth.transform.position, mothChild.transform.position);
		if (distanceToMothChild <= allowedDistance)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
