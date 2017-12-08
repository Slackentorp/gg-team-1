using Gamelogic.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothBehaviour
{
	public delegate void ReachedPosition();
	public event ReachedPosition OnReachedPosition;

	[SerializeField]
	private AnimationCurve MothFlightLerpCurve;

	[SerializeField]
	private float timeScale = 1;

	[SerializeField]
	int noiseReducerMax = 16;

	int noiseReducerMin = 10;

	[SerializeField]
	float mothDistanceToObject = 0.2f;

	GameObject moth;
	Transform mothChild;
	Camera camera;
	Vector3 hitPoint, mothStartPos;
	RaycastHit hit;
	Vector3 mothRotation, dampVelocity, parentPos;
	Vector3 pos, anchorPointPlusPos, currentVelocity;
	float veticalMothScreenPlacement, limitMothForwardFidgit;
	float turningTime, turningSpeed, proceduralLerpTime, time;
	float perlinNoiseX, perlinNoiseY, perlinNoiseZ;
	float distance, timeToTarget = 1.0f, timeSpentInDamp = 1.5f;
	float fidgitInFlightReducer = 0;
	float velocity;
	private float mothDistanceToCeiling;

	public float MothSpeed
	{
		get; set;
	}

	private bool lerpRunning = false;
	private bool mothDampProcedural = false;
	public bool fragmentMode { get; private set; }
	

	void OnValidate()
	{
		if (timeScale < 1)
		{
			timeScale = 1;
		}
	}

	public MothBehaviour(GameObject moth, Camera camera, float mothDistanceToObject, float MothSpeed, AnimationCurve curve,
						int noiseReducerMax, int noiseReducerMin, float speedModifier, AnimationCurve MothFlightLerpCurve,
						float verticalMothScreenPlacement, float limitMothForwardFidgit, float fidgitInFlightReducer, 
						float timeSpentInDamp, float mothDistanceToCeiling)
	{
		this.moth = moth;
		this.camera = camera;
		this.mothDistanceToObject = mothDistanceToObject;
		this.MothSpeed = MothSpeed;
		this.noiseReducerMax = noiseReducerMax;
		this.noiseReducerMin = noiseReducerMin;
		this.MothFlightLerpCurve = MothFlightLerpCurve;
		this.veticalMothScreenPlacement = verticalMothScreenPlacement;
		this.limitMothForwardFidgit = limitMothForwardFidgit;
		this.fidgitInFlightReducer = fidgitInFlightReducer;
		this.timeSpentInDamp = timeSpentInDamp;
		this.mothDistanceToCeiling = mothDistanceToCeiling;
	}

	public void SetFragmentMode(bool b)
	{
		fragmentMode = b;
	}


	public void Update()
	{
		if (mothChild == null)
		{
			mothChild = moth.transform.GetChild(0);
		}
		if (mothChild != null)
		{
			if(!fragmentMode)
			{
				ProceduralMovement();
			}
			MothGoToPosition();
			/*int layerMask = 1 << 11;
			layerMask = ~layerMask;
			if(Physics.CheckBox(moth.transform.position, mothBoxCollider.size/2, Quaternion.identity, layerMask))
			{
				hitPoint = moth.transform.position;
			}*/
		}
	}

	public void SetMothPos(RaycastHit hit, bool wall)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		mothStartPos = moth.transform.position;
		mothRotation = moth.transform.forward;
		if(wall)
		{
			hitPoint = hit.point + hit.normal * mothDistanceToObject;
		} else {
			hitPoint = hit.point + hit.normal * mothDistanceToCeiling;
		}

		DistancefromMothToHitpoint();
		lerpRunning = true;
		time = 0.0f;
		turningTime = 0.0f;
	}

	public void SetMothPos(Vector3 position)
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		mothStartPos = moth.transform.position;
		mothRotation = moth.transform.forward;
		hitPoint = position;

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
				if(OnReachedPosition != null)
				{
					OnReachedPosition();
				}
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
	}

	private void PointfromRaycast()
	{
		AkSoundEngine.PostEvent("MOTH_START_FLIGHT", moth);
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			hitPoint = hit.point + hit.normal * mothDistanceToObject;
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
			if (lerpRunning == true)
			{
				CalculatePerlinNoise(fidgitInFlightReducer, fidgitInFlightReducer);
			}
			
			CalculatePerlinNoise(noiseReducerMin, noiseReducerMax);

			proceduralLerpTime = 0;
			anchorPointPlusPos = parentPos + pos;
			mothDampProcedural = true;
		}
		if (mothDampProcedural == true && proceduralLerpTime * timeSpentInDamp < 1)
		{
			mothChild.transform.localPosition = Vector3.SmoothDamp(mothChild.transform.localPosition, anchorPointPlusPos, ref currentVelocity, timeToTarget);
		}
		else if (mothDampProcedural == true)
		{
			mothDampProcedural = false;
		}
	}

	Vector3 CalculatePerlinNoise(float minNoiseReduce, float maxNoiseReduce)
	{
		//perlinNoiseX = Mathf.PerlinNoise(1.0f * Time.time, 0.0f);
		//if (perlinNoiseX % 0.02f == 0)
		//{
		//	perlinNoiseX = -perlinNoiseX;
		//}
		//perlinNoiseY = Mathf.PerlinNoise(0.0f, 1.0f * Time.time);
		//if (perlinNoiseY % 0.02f == 0)
		//{
		//	perlinNoiseY = -perlinNoiseY;
		//}
		//perlinNoiseZ = Mathf.PerlinNoise(Time.time * 1.0f, 1.0f * Time.time);
		//if (perlinNoiseZ % 0.02f == 0)
		//{
		//	perlinNoiseZ = -perlinNoiseZ;
		//}
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

		pos.x = perlinNoiseX / (Random.Range(minNoiseReduce, maxNoiseReduce + 1));
		pos.y = perlinNoiseY / (Random.Range(minNoiseReduce, maxNoiseReduce + 1)) - veticalMothScreenPlacement;
		pos.z = perlinNoiseZ / ((Random.Range(minNoiseReduce, maxNoiseReduce + 1)* limitMothForwardFidgit));
		return pos;
	}

	public void SetMothAnimationState(string triggerName)
	{
        Animator mothAnimator = mothChild.gameObject.GetComponent<Animator>();
        mothAnimator.SetTrigger(triggerName);
    }
	public void SetMothAnimationState(string triggerName, string resetName)
	{
        Animator mothAnimator = mothChild.gameObject.GetComponent<Animator>();
        mothAnimator.ResetTrigger(resetName);
        mothAnimator.SetTrigger(triggerName);
    }
}
