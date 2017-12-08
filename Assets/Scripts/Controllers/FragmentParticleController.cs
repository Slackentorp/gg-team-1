using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentParticleController
{
	private Fragment[] fragmentPos;
	private Renderer[] fragmentMaterials;
	private Transform MothPosition;
	private AnimationCurve dissolveAmount, mainTexEmission, emissionIntCurve;
	private Material[] fragMaterialsArray;
	private GameObject fragMaterial;
	private bool done = false;
	private float mainTexLerp = 1.0f, dissolveLerp = 1.0f, emissionLerp = 0.5f;
	private float[] dissolveTime, maintexTime, emissionIntTime;
	private bool[] emissionIntReverse;
	private int maintexId, dissolveId, emissionIntId;
	Dictionary<Fragment, FragmentState> fragmentDictionary = new Dictionary<Fragment, FragmentState>();

	public enum FragmentState
	{
		NOT_PLAYED,
		DISCOVER,
		WHISPER,
		LEAVE
	}

	public FragmentParticleController(Fragment[] fragmentObjects, Transform mothPos, AnimationCurve dissolveAmount,
									  AnimationCurve mainTexEmission, AnimationCurve emissionInt)
	{
		if (done == false)
		{
			this.fragmentPos = fragmentObjects;
			this.MothPosition = mothPos;
			this.dissolveAmount = dissolveAmount;
			this.mainTexEmission = mainTexEmission;
			this.emissionIntCurve = emissionInt;
			fragMaterialsArray = new Material[fragmentPos.Length];
			fragmentMaterials = new Renderer[fragmentPos.Length];
			dissolveTime = new float[fragmentMaterials.Length];
			maintexTime = new float[fragmentMaterials.Length];
			emissionIntTime = new float[fragmentMaterials.Length];
			emissionIntReverse = new bool[fragmentMaterials.Length];
			maintexId = Shader.PropertyToID("_MaintexEmissionOverlayIntensity");
			dissolveId = Shader.PropertyToID("_DissolveAmount");
			emissionIntId = Shader.PropertyToID("_MaintexEmissionIntensity");

			if (fragmentPos != null)
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
					emissionIntTime[j] = 0.0f;
					fragMaterialsArray[j].SetFloat(maintexId, 0.5f);
					fragMaterialsArray[j].SetFloat(dissolveId, 0.0f);

				}
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
						FragMaterialDissolve(i, true);
					}
					if (maintexTime[i] < 1.0f)
					{
						FragMaterialEmission(i, true);
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
					FragmentMaterialEmissionInt(i, 0.4f);
				}
				if (Mathf.Abs(dist) >= fragmentPos[i].InternalInteractionDistance &&
						fragmentDictionary[fragmentPos[i]] == FragmentState.WHISPER)
				{
					fragmentDictionary[fragmentPos[i]] = FragmentState.LEAVE;
					PlaySoundEvents("LEAVE", i);
				}
				if (Mathf.Abs(dist) > fragmentPos[i].InternalInteractionDistance)
				{
					if (dissolveTime[i] >= 1)
					{
						FragmentMaterialEmissionInt(i, 0.4f);
					}
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.LEAVE)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
					}
				}
				if (fragmentPos[i].HasPlayed)
				{
					FragmentMaterialEmissionInt(i, 0.27f);
				}
			}
		}
	}

	void FragMaterialEmission(int i, bool reverse)
	{
		float t = 0.0f;
		if (reverse)
		{
			t = maintexTime[i];
		}
		else
		{
			t = 1 - dissolveTime[i];
		}
		fragMaterialsArray[i].SetFloat(maintexId, Mathf.Lerp(0.5f, 1.0f,
									   mainTexEmission.Evaluate(maintexTime[i])));
		maintexTime[i] += Time.deltaTime * mainTexLerp;
	}

	void FragMaterialDissolve(int i, bool reverse)
	{
		float t = 0.0f;
		if (reverse)
		{
			t = dissolveTime[i];
		}
		else
		{
			t = 1 - dissolveTime[i];
		}
		fragMaterialsArray[i].SetFloat(dissolveId, Mathf.Lerp(0.0f, 1.0f,
									   dissolveAmount.Evaluate(t)));
		dissolveTime[i] += Time.deltaTime * dissolveLerp;
	}

	void FragmentMaterialEmissionInt(int i, float maxValue)
	{
		float t = 0;
		if (emissionIntReverse[i])
		{
			t = emissionIntTime[i];
		}
		else
		{
			t = 1 - emissionIntTime[i];
		}
		fragMaterialsArray[i].SetFloat(emissionIntId, Mathf.Lerp(0.2f, maxValue,
										emissionIntCurve.Evaluate(t)));
		emissionIntTime[i] += Time.deltaTime * 0.7f;
		if (emissionIntTime[i] >= 1.0f)
		{
			emissionIntTime[i] = 0.0f;
			emissionIntReverse[i] = !emissionIntReverse[i];
		}
	}


	void InstanceMaterialsToParent(Vector3 fragmentPoss, int iteration)
	{
		fragMaterialsArray[iteration] = Material.Instantiate(fragmentMaterials[iteration].material,
										fragmentPoss, Quaternion.identity);
		fragmentPos[iteration].GetComponentInChildren<Renderer>().material = fragMaterialsArray[iteration];
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


