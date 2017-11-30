using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentParticleController
{
	Fragment[] fragmentPos;
	GameObject[] fragParticleArray;
	Transform MothPosition;
	private GameObject fragParticle;
	private float particlesActiveDist;
	bool done = false;

	public FragmentParticleController(Fragment[] fragmentObjects, GameObject fragmentParticles,
									  Transform mothPos)
	{
		this.fragmentPos = fragmentObjects;
		this.fragParticle = fragmentParticles;
		this.MothPosition = mothPos;
		fragParticleArray = new GameObject[fragmentObjects.Length];

		if (fragmentPos != null && done == false)
		{
			for (int i = 0; i < fragmentPos.Length; i++)
			{
				Debug.Log(fragmentPos[i].gameObject.transform.position);
				InstantiateParticles(fragmentPos[i].gameObject.transform.position, i);
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
				}
				else
				{
					fragParticleArray[i].SetActive(false);
				}
			}
		}
	}

	void InstantiateParticles(Vector3 fragmentPos, int iteration)
	{
		fragParticleArray[iteration] = GameObject.Instantiate(fragParticle, fragmentPos, Quaternion.identity);
	}
}


