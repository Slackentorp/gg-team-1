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
	bool done = false;
	bool fragmentPlayed = false;
	bool playingDiscover = false, playingWhisper = false, playingLeave = false;

	List<FragmentParticleController> fragmentParticles = new List<FragmentParticleController>();

	int Id;
	Fragment[] fragmentPosition;
	ParticleSystem[] fragmentParticleArr;
	bool fragmentPlayedState;

	public FragmentParticleController(Fragment[] fragmentObjects, GameObject fragmentParticles,
									  Transform mothPos)
	{
		this.fragmentPos = fragmentObjects;
		this.fragParticle = fragmentParticles;
		this.MothPosition = mothPos;
		fragParticleParent = new GameObject("Fragment Particles");
		fragParticleArray = new GameObject[fragmentObjects.Length];

		if (fragmentPos != null && done == false)
		{
			for (int i = 0; i < fragmentPos.Length; i++)
			{
				InstanceParticlesToParent(fragmentPos[i].gameObject.transform.position, i);
				fragmentDictionary.Add(fragmentPos[i], FragmentState.NOT_PLAYED);
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
					fragParticleArray[i].SetActive(true);

					if (fragmentPos[i].HasPlayed == true && fragmentDictionary[fragmentPos[i]] == FragmentState.NOT_PLAYED)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.DISCOVER;
						EnterFragmentArea(i);
					}
					else if (fragmentDictionary[fragmentPos[i]] == FragmentState.DISCOVER)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.WHISPER;
						AfterPlayEnterFragmentArea(i);
					}
				}
				else if (Mathf.Abs(dist) >= fragmentPos[i].InternalInteractionDistion &&
						fragmentDictionary[fragmentPos[i]] == FragmentState.WHISPER)
				{
					fragmentDictionary[fragmentPos[i]] = FragmentState.LEAVE;
					fragParticleArray[i].SetActive(false);
					LeaveFragmentArea(i);
				}
				else if (Mathf.Abs(dist) > fragmentPos[i].InternalInteractionDistion)
				{
					if (fragmentDictionary[fragmentPos[i]] == FragmentState.LEAVE)
					{
						fragmentDictionary[fragmentPos[i]] = FragmentState.LEFT;
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

	void InstanceParticlesToParent(Vector3 fragmentPos, int iteration)
	{
		fragParticleArray[iteration] = GameObject.Instantiate(fragParticle, fragmentPos, Quaternion.identity);
		fragParticleArray[iteration].transform.parent = fragParticleParent.transform;
	}

	void EnterFragmentArea(int iteration)
	{

		AkSoundEngine.PostEvent("FRAGMENT_DISCOVER", fragmentPos[iteration].gameObject);
		Debug.Log("Should be playing Discover");
	}

	void AfterPlayEnterFragmentArea(int iteration)
	{
		//if (playingWhisper) return;
		AkSoundEngine.PostEvent("FRAGMENT_WHISPER", fragmentPos[iteration].gameObject);
		Debug.Log("Should be playing Whisper");
	}

	void LeaveFragmentArea(int iteration)
	{
		//if (plaingLeave) return;
		AkSoundEngine.PostEvent("FRAGMENT_LEAVE", fragmentPos[iteration].gameObject);
		Debug.Log("Should be playing Leave");
	}

	Dictionary<Fragment, FragmentState> fragmentDictionary = new Dictionary<Fragment, FragmentState>();

	public enum FragmentState
	{
		NOT_PLAYED,
		DISCOVER,
		WHISPER,
		LEAVE,
		LEFT
	}

	//Create dictionary where the key is the fragments, and value is the state for each fragment.
	//Check if fragment is in specific state, if yes then set to a new state, if no do something else....
	//Discover play once when discovered, (Should cascade sounds if mulitple are "Discovered" at the same time)
	//same for the activation of particles.
	//Whisper play when discovered and played once, && within a range.
	//Leave play when leaving the area for fragment interaction && then stop playing
}


