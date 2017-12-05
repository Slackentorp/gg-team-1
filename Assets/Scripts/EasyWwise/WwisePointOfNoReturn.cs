using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwisePointOfNoReturn : MonoBehaviour
{
	public GameObject Reference;
	public string WwiseEventName;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		GameController.OnPointOfNoReturn += TriggerOnPointOfNoReturn;
	}

	private void OnDisable()
	{
		GameController.OnPointOfNoReturn -= TriggerOnPointOfNoReturn;
	}

	void Awake()
	{
		if(Reference == null)
		{
			Reference = gameObject;
		}
	}

	private void TriggerOnPointOfNoReturn()
	{
		AkSoundEngine.PostEvent(WwiseEventName, Reference);
	}
}
