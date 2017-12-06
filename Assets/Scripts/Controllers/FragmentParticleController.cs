using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentParticleController
{
	private Fragment[] fragmentPos;
	private Renderer[] fragmentMaterials;
	private Transform MothPosition;
	private AnimationCurve dissolveAmount, mainTexEmission;
	private Material[] fragMaterialsArray;
	private GameObject fragMaterialParent, fragMaterial;
	private bool fragmentPlayed = false, done = false;
	private bool playingDiscover = false, playingWhisper = false, playingLeave = false;
	private float mainTexLerp = 1.0f, dissolveLerp = 1.0f;
	private float[] dissolveTime;
	private float[] maintexTime;

	List<FragmentParticleController> fragmentParticles = new List<FragmentParticleController>();

	public FragmentParticleController(Fragment[] fragmentObjects, Transform mothPos,
									  AnimationCurve dissolveAmount, AnimationCurve mainTexEmission)
	{
		if (done == false)
		{
			this.fragmentPos = fragmentObjects;
			this.MothPosition = mothPos;
			this.dissolveAmount = dissolveAmount;
			this.mainTexEmission = mainTexEmission;
			fragMaterialParent = new GameObject("Fragment Materials");
			fragMaterialsArray = new Material[fragmentPos.Length];
			fragmentMaterials = new Renderer[fragmentPos.Length];
			dissolveTime = new float[fragmentMaterials.Length];
			maintexTime = new float[fragmentMaterials.Length];

			if (fragmentPos != null && done == false)
			{
				for (int i = 0; i < fragmentPos.Length; i++)
				{
					fragmentDictionary.Add(fragmentPos[i], FragmentState.NOT_PLAYED);
					fragmentMaterials[i] = fragmentPos[i].GetComponentInChildren<Renderer>();
					InstanceMaterialsToParent(fragmentPos[i].transform.position, i);
				}

				for (int j = 0; j < fragmentMaterials.Length; j++)
				{
					dissolveTime[j] = 0.0f;
					maintexTime[j] = 0.0f;
					fragMaterialsArray[j].SetFloat("_MaintexEmissionOverlayIntensity", Mathf.Lerp(0.5f, 1.0f,
													mainTexEmission.Evaluate(maintexTime[j])));
				}
				done = true;
			}
			done = true;
		}
	}

	public void Update()
	{
		if (fragmentPos != null || fragMaterialsArray != null)
		{
			for (int i = 0; i < fragmentPos.Length; i++)
			{
				float dist = Vector3.SqrMagnitude(MothPosition.position - fragmentPos[i].transform.position);
				if (Mathf.Abs(dist) < fragmentPos[i].InternalInteractionDistance)
				{
					if (dissolveTime[i] < 1.0f)
					{
						FragMaterialDissolve(i);
					}
					if (maintexTime[i] < 1.0f)
					{
						FragMaterialEmission(i);
					}
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.NOT_PLAYED)
					{
						PlaySoundEvents("DISCOVER", i);
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
					}
					else if (fragmentDictionary[fragmentPos[i]] == FragmentState.DISCOVER)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.WHISPER;
						PlaySoundEvents("WHISPER", i);
					}
				}
				else if (Mathf.Abs(dist) >= fragmentPos[i].InternalInteractionDistance &&
						fragmentDictionary[fragmentPos[i]] == FragmentState.WHISPER)
				{
					fragmentDictionary[fragmentPos[i]] = FragmentState.LEAVE;
					PlaySoundEvents("LEAVE", i);
				}
				else if (Mathf.Abs(dist) > fragmentPos[i].InternalInteractionDistance)
				{
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.LEAVE)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
					}
				}
			}
		}
	}

	void FragMaterialEmission(int i)
	{
			maintexTime[i] += Time.deltaTime * mainTexLerp;
			if (fragMaterialsArray[i] == null)
			{
				return;
			}
			fragMaterialsArray[i].SetFloat("_MaintexEmissionOverlayIntensity", Mathf.Lerp(0.5f, 1.0f,
													mainTexEmission.Evaluate(maintexTime[i])));
	}

	void FragMaterialDissolve(int i)
	{
		dissolveTime[i] += Time.deltaTime * dissolveLerp;
		if (fragMaterialsArray[i] == null)
		{
			return;
		}
		fragMaterialsArray[i].SetFloat("_DissolveAmount", Mathf.Lerp(0.0f, 1.0f,
					dissolveAmount.Evaluate(dissolveTime[i])));
	}

	float MaterialGetFloat(int i, string attribute, float currentValue)
	{
		currentValue = fragmentMaterials[i].material.GetFloat(attribute);

		return currentValue;
	}

	void InstanceMaterialsToParent(Vector3 fragmentPoss, int iteration)
	{
		fragMaterialsArray[iteration] = Material.Instantiate(fragmentMaterials[iteration].material, fragmentPoss, Quaternion.identity);
		fragmentPos[iteration].GetComponentInChildren<Renderer>().material = fragMaterialsArray[iteration];
	}

	Dictionary<Fragment, FragmentState> fragmentDictionary = new Dictionary<Fragment, FragmentState>();

	public enum FragmentState
	{
		NOT_PLAYED,
		DISCOVER,
		WHISPER,
		LEAVE
	}

	void PlaySoundEvents(string soundEvent, int fragmentNumber)
	{
		switch (soundEvent)
		{
			case "DISCOVER":
				AkSoundEngine.PostEvent("FRAGMENT_DISCOVER", fragmentPos[fragmentNumber].gameObject);
				break;

			case "WHISPER":
				AkSoundEngine.PostEvent("FRAGMENT_WHISPER", fragmentPos[fragmentNumber].gameObject);
				break;

			case "LEAVE":
				AkSoundEngine.PostEvent("FRAGMENT_LEAVE", fragmentPos[fragmentNumber].gameObject);
				break;
		}
	}
}


