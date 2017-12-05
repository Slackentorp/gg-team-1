using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentParticleController
{
	Fragment[] fragmentPos;
	GameObject[] fragParticleArray;
	Transform MothPosition;
	private GameObject fragParticleParent;
	private GameObject fragParticle;
	bool loaded = false;
	bool fragmentPlayed = false;
	bool playingDiscover = false, playingWhisper = false, playingLeave = false;

	List<FragmentParticleController> fragmentParticles = new List<FragmentParticleController>();

	public FragmentParticleController(Fragment[] fragmentObjects, GameObject fragmentParticles,
									  Transform mothPos)
	{
		if (loaded == false)
		{
			this.fragmentPos = fragmentObjects;
			this.fragParticle = fragmentParticles;
			this.MothPosition = mothPos;
			fragParticleParent = new GameObject("Fragment Particles");
			fragParticleArray = new GameObject[fragmentObjects.Length];

			if (fragmentPos != null)
			{
				for (int i = 0; i < fragmentPos.Length; i++)
				{
					InstanceParticlesToParent(fragmentPos[i].gameObject.transform.position, i);
					fragmentDictionary.Add(fragmentPos[i], FragmentState.NOT_PLAYED);
				}
				loaded = true;
			}
			loaded = true;
		}
	}


	public void Update()
	{
		if (fragmentPos != null || fragParticleArray != null)
		{
			for (int i = 0; i < fragmentPos.Length; i++)
			{
				float dist = Vector3.SqrMagnitude(MothPosition.position - fragmentPos[i].transform.position);
				if (Mathf.Abs(dist) < fragmentPos[i].InternalInteractionDistance)
				{
					fragParticleArray[i].SetActive(true);

					if (fragmentDictionary[fragmentPos[i]] == FragmentState.NOT_PLAYED )
					{
						PlaySoundEvents("DISCOVER",i);
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
					fragParticleArray[i].SetActive(false);
					PlaySoundEvents("LEAVE", i);
				}
				else if (Mathf.Abs(dist) > fragmentPos[i].InternalInteractionDistance)
				{
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.LEAVE)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
						fragParticleArray[i].SetActive(false);
					}

				}
				if (fragmentPos[i].HasPlayed == true)
				{
					fragParticleArray[i].SetActive(false);
				}
			}
		}
	}

	void InstanceParticlesToParent(Vector3 fragmentPos, int iteration)
	{
		fragParticleArray[iteration] = GameObject.Instantiate(fragParticle, fragmentPos, Quaternion.identity);
		fragParticleArray[iteration].transform.parent = fragParticleParent.transform;
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


