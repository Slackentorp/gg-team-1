using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentParticleController
{
	Fragment[] fragmentPos;
	Renderer[] fragmentMaterials;
	GameObject[] fragParticleArray;
	Transform MothPosition;
	AnimationCurve dissolveAmount;
	AnimationCurve mainTexEmission;
	private GameObject fragParticleParent;
	private GameObject fragParticle;
	bool done = false;
	bool fragmentPlayed = false;
	bool playingDiscover = false, playingWhisper = false, playingLeave = false;
	float mainTexLerp = 0.0f;
	float dissolveLerp = 0.0f;

	List<FragmentParticleController> fragmentParticles = new List<FragmentParticleController>();

	public FragmentParticleController(Fragment[] fragmentObjects, GameObject fragmentParticles,
									  Transform mothPos, AnimationCurve dissolveAmount,
									  AnimationCurve mainTexEmission)
	{
		if (done == false)
		{
			this.fragmentPos = fragmentObjects;
			this.fragParticle = fragmentParticles;
			this.MothPosition = mothPos;
			this.dissolveAmount = dissolveAmount;
			this.mainTexEmission = mainTexEmission;
			fragParticleParent = new GameObject("Fragment Particles");
			fragParticleArray = new GameObject[fragmentObjects.Length];
			fragmentMaterials = new Renderer[fragmentPos.Length];

			//instantiatedMaterials = new Renderer[fragmentMaterials.Length];

			if (fragmentPos != null && done == false)
			{
				for (int i = 0; i < fragmentPos.Length; i++)
				{
					InstanceParticlesToParent(fragmentPos[i].gameObject.transform.position, i);
					fragmentDictionary.Add(fragmentPos[i], FragmentState.NOT_PLAYED);
					fragmentMaterials[i] = fragmentPos[i].GetComponentInChildren<Renderer>();
					//InstanceMaterials(fragmentMaterials[i].gameObject.transform.position, i);
				}
				done = true;
			}
			done = true;
		}
	}

	public void Update()
	{
		if (fragmentPos != null || fragParticleArray != null)
		{
			for (int i = 0; i < fragmentPos.Length; i++)
			{
				float dist = Vector3.SqrMagnitude(MothPosition.position - fragmentPos[i].transform.position);
				if (Mathf.Abs(dist) < fragmentPos[i].InternalInteractionDistion)
				{
					FragMaterialEmission(i, true);
					fragParticleArray[i].SetActive(true);

					if (fragmentPos[i].HasPlayed == false && fragmentDictionary[fragmentPos[i]] == FragmentState.NOT_PLAYED)
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
				else if (Mathf.Abs(dist) >= fragmentPos[i].InternalInteractionDistion &&
						fragmentDictionary[fragmentPos[i]] == FragmentState.WHISPER)
				{
					fragmentDictionary[fragmentPos[i]] = FragmentState.LEAVE;
					fragParticleArray[i].SetActive(false);
					PlaySoundEvents("LEAVE", i);
				}
				else if (Mathf.Abs(dist) > fragmentPos[i].InternalInteractionDistion)
				{
					FragMaterialEmission(i, false);
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.LEAVE)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
						fragParticleArray[i].SetActive(false);
					}
					else
					{
						fragParticleArray[i].SetActive(false);
					}
				}
			}
		}
	}

	void FragMaterialEmission(int i, bool apply)
	{
		if (apply)
		{
			//make transition more smooth.
			//make the intensity blink when active
			//make the dissolve amount go from 1.0 to 0.0 when Discover. 
			mainTexLerp += Time.deltaTime * 0.5f;
			dissolveLerp += Time.deltaTime * 0.5f;
			if (fragmentMaterials[i] == null)
			{
				return;
			}
			Material mat = fragmentMaterials[i].material;
			mat.SetFloat("_MaintexEmissionOverlayIntensity", Mathf.Lerp(0.5f, 1.0f, mainTexEmission.Evaluate(mainTexLerp)));
			mat.SetFloat("_DissolveAmount", Mathf.Lerp(0.0f, 1.0f, dissolveAmount.Evaluate(dissolveLerp)));
		}
		else
		{
			mainTexLerp += Time.deltaTime * 0.5f;
			dissolveLerp += Time.deltaTime * 0.5f;
			if (fragmentMaterials[i] == null)
			{
				return;
			}
			Material mat = fragmentMaterials[i].material;
			mat.SetFloat("_MaintexEmissionOverlayIntensity", Mathf.Lerp(1.0f, 0.5f, mainTexEmission.Evaluate(mainTexLerp)));

			mat.SetFloat("_DissolveAmount", Mathf.Lerp(1.0f, 0.0f, dissolveAmount.Evaluate(dissolveLerp)));
		}
	}

	void InstanceParticlesToParent(Vector3 fragmentPos, int iteration)
	{
		fragParticleArray[iteration] = GameObject.Instantiate(fragParticle, fragmentPos, Quaternion.identity);
		fragParticleArray[iteration].transform.parent = fragParticleParent.transform;
	}

	/*void InstanceMaterials(Vector3 fragmentMaterialsPos, int iteration)
	{
		instantiatedMaterials[iteration] = Material.Instantiate(fragmentMaterials[iteration], fragmentMaterialsPos, Quaternion.identity);
	}*/

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


