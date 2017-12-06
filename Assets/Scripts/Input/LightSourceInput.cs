using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using Gamelogic.Extensions;
using UnityEngine;

/// <summary>
/// Touch input specifically for light sources
/// </summary>
public class LightSourceInput : MonoBehaviour
{
	[SerializeField]
	private bool IsLit = false;
	[SerializeField]
	private bool isSwitchable = false;

	[SerializeField]
	private Interactable[] interactables;

	[SerializeField] 
	private Material lampMaterialOn, lampMaterialOff;
	private ParticleSystem particleSystemLamp;
	private Renderer rend;
	private State currentLampState;
	[SerializeField]
	private bool lampStateCheck = false;
	private bool lampFlickerCheck = false;
	[SerializeField]
	private int lightMapIndex;
	private bool firstTimeFlickerCheck;
	[SerializeField]
	private Vector3 lerpParticleOffset = new Vector3(0f, 0.3f, 0f);

	private float flickerRangeLong, flickerRangeShort;

	[SerializeField]
	private bool isActivated;
	[SerializeField]
	private bool lampFullOn;

	private AnimationCurve fragmentToLightsourceCurve;
	private IEnumerator Flickering;
	private IEnumerator IEParticle;

	public bool Lit
	{
		get
		{
			return IsLit;
		}
		set
		{
			IsLit = value;
		}
	}

	public bool ISSwitchable
	{
		get
		{
			return isSwitchable;
		}
		set
		{
			isSwitchable = value;
		}
	}

	public bool LampActivated
	{
		get
		{
			return isActivated;
		}
		set
		{
			isActivated = value;
		}
	}
	public bool LampFullOn
	{
		get
		{
			return lampFullOn;
		}
		set
		{
			lampFullOn = value;
		}
	}

	public delegate void LightSourceAction(bool beingLoaded);
	public static event LightSourceAction LightSourceCall;

	public delegate void LightMapSwitchAction(bool StateCheck, bool flickCheck, int indexNr);
	public static event LightMapSwitchAction LightMapSwitchCall;

	void OnEnable()
	{
		Interactable.InteractableCall += FragmentCheckerLerp;
	}

	void OnDisable()
	{
		Interactable.InteractableCall -= FragmentCheckerLerp;
	}

	private void Start()
	{
		fragmentToLightsourceCurve = GameController.Instance.FragmentToLightSourceCurve;
		firstTimeFlickerCheck = true;
		FragmentCheckerSwitch(interactables[0]); // set tutorial lamp to Flicker
	}

	public void FragmentCheckerLerp(Interactable sender, bool beingLoaded)
	{
		int numPlayedFragments = interactables.Count(f => f.HasPlayed);

		if (interactables.Length == 0 || sender == null)
		{
			return;
		}

		foreach (Interactable localInteractables in interactables)
		{
			if (localInteractables == sender)
			{
				if (!localInteractables.HasHasPlayed && !beingLoaded)
				{
					if (interactables.Length == 3)
					{
						if (numPlayedFragments >= interactables.Length - 1)
						{
							if (sender != null && IEParticle == null)
							{
								IEParticle = ParticleLerp(sender);
								StartCoroutine(IEParticle);
							}
						}
						else if (numPlayedFragments == interactables.Length - 2)
						{
							if (sender != null && IEParticle == null)
							{
								IEParticle = ParticleLerp(sender);
								StartCoroutine(IEParticle);
							}
						}
					}
					else if (interactables.Length == 1)
					{
						if (numPlayedFragments == interactables.Length)
						{
							if (sender != null && IEParticle == null)
							{
								IEParticle = ParticleLerp(sender);
								StartCoroutine(IEParticle);
							}
						}
						else if (numPlayedFragments == 0)
						{
							if (sender != null)
							{
								StartCoroutine(ParticleLerp(sender));
							}
						}
					}
				}
				localInteractables.HasHasPlayed = true;
			}
		}
		LightSourceCallz(beingLoaded);
	}

	public void FragmentCheckerSwitch(Interactable sender)
	{
		int numPlayedFragments = interactables.Count(f => f.HasPlayed);

		if (interactables.Length == 0 || sender == null)
		{
			return;
		}

		foreach (Interactable localInteractables in interactables)
		{
			if (localInteractables == sender)
			{
				if (interactables.Length == 3)
				{
					if (numPlayedFragments >= interactables.Length - 1)
					{
						LampON();

					}
					else if (numPlayedFragments == interactables.Length - 2)
					{
						LampFlickering();

					}
				}
				else if (interactables.Length == 1)
				{
					if (numPlayedFragments == interactables.Length)
					{
						LampON();

					}
					else if (numPlayedFragments == 0)
					{
						LampFlickering();
					}
				}

			}
		}
	//	LightSourceCallz();
	}


	public void LightSourceCallz(bool beingLoaded)
	{
		if (LightSourceCall != null)
		{
			LightSourceCall(beingLoaded);
		}
	}

	enum State
	{
		LAMP_OFF,
		LAMP_FLICKERING,
		LAMP_ON
	};

	private void LampOFF()
	{
		currentLampState = State.LAMP_OFF;
		lampStateCheck = false;
		LightSwitch(currentLampState);
	}
	private void LampFlickering()
	{
		lampStateCheck = false;
		currentLampState = State.LAMP_FLICKERING;
		LightSwitch(currentLampState);
		isActivated = true;
		lampFullOn = false;
	}

	[Button]
	public void ForceSwitchOn()
	{
		LampON();
		LightSourceCallz(true);
	}


	[Button]
	public void ForceSwitchOff()
	{
		LampOFF();
	}

	private void LampON()
	{
		currentLampState = State.LAMP_ON;
		lampStateCheck = true;
		isActivated = true;
		lampFullOn = true;

		LightSwitch(currentLampState);
		InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
	}

	private IEnumerator ParticleLerp(Interactable interactable)
	{
		float time = 0;
		float endTime = 1;
		if(fragmentToLightsourceCurve != null)
		{
			endTime = fragmentToLightsourceCurve.keys[fragmentToLightsourceCurve.length - 1].time;
		}

		GameObject particle = Instantiate(GameController.Instance.FragmentToLightSourceParticles, interactable.transform.position, Quaternion.identity);
		particle.transform.GetChild(0).gameObject.SetActive(true);
		particle.transform.GetChild(1).gameObject.SetActive(false);

		ParticleSystem explosionSystem = particle.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
		AkSoundEngine.PostEvent("PARTICLE_APPEAR_FRAGMENT", particle);

		while (time < endTime)
		{
			float t = time;
			if(fragmentToLightsourceCurve != null)
			{
				t = fragmentToLightsourceCurve.Evaluate(time);
			}

			particle.transform.position = Vector3.Lerp(interactable.transform.position, transform.position + lerpParticleOffset, t);

			time += Time.deltaTime;
			yield return null;
		}

		particle.transform.GetChild(1).gameObject.SetActive(true);
		FragmentCheckerSwitch(interactable);
		AkSoundEngine.PostEvent("PARTICLE_ENTER_LAMP", particle);
		yield return new WaitForSeconds(explosionSystem.main.duration + explosionSystem.main.startLifetime.constant + explosionSystem.main.startLifetime.constant / 2);
		Destroy(particle);
		IEParticle = null;
	}

	private void LightSwitch(State currentLampState)
	{
		if (GetComponentInChildren<Renderer>() != null)
		//&& GetComponentInChildren<ParticleSystem>() != null)
		{
			rend = GetComponentsInChildren<Renderer>()[0];
			//  particleSystemLamp = GetComponentsInChildren<ParticleSystem>()[0];

			if (currentLampState == State.LAMP_OFF)
			{
				if (Flickering != null)
				{
					StopCoroutine(Flickering);
				}

				rend.sharedMaterial = lampMaterialOff;
				//var em = particleSystemLamp.emission;
				//em.enabled = false;

				//LightMapSwitchCall(lampStateCheck, lightMapIndex);
			}
			else if (currentLampState == State.LAMP_FLICKERING)
			{
				if (firstTimeFlickerCheck)
				{
				    AkSoundEngine.PostEvent("LAMP_FLICKERING", gameObject);
            firstTimeFlickerCheck = false;
        }
				lampFlickerCheck = true;
				Flickering = FlickeringSequence();
				StartCoroutine(Flickering);
				//var em = particleSystemLamp.emission;
				//em.enabled = true;
			}
			else if (currentLampState == State.LAMP_ON)
			{
				if (Flickering != null)
				{
					StopCoroutine(Flickering);
				}

				AkSoundEngine.PostEvent("LAMP_ON", gameObject);
				Debug.Log("lamp_on");
				lampFlickerCheck = false;
				rend.sharedMaterial = lampMaterialOn;
				//var em = particleSystemLamp.emission;
				//em.enabled = true;

			}
		}
		else
		{
			Debug.Log("ParticleSystem or Renderer not on lamp nr. " + lightMapIndex);
		}
	}

	[SerializeField]
	float flickTimeLongMin = 0.5f, flickTimeLongMax = 3f,
		flickTimeShortMin = 0.05f, flickTimeShortMax = 0.1f;
	[SerializeField]
	int nrOfFlicksMin = 2, nrOfFlicksMax = 3;
	[SerializeField]
	int LongONSequenceMin = 1, LongOnSequenceMax = 4, LongOnSequenceOutOF = 6;

	IEnumerator FlickeringSequence()
	{
		for (int j = 0; j < LongOnSequenceOutOF; j++)
		{
			rend.sharedMaterial = lampMaterialOff;
			lampStateCheck = false;
			InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
			flickerRangeLong = Random.Range(flickTimeLongMin, flickTimeLongMax);
			yield return new WaitForSeconds(flickerRangeLong);

			int longONFrequency = Random.Range(LongONSequenceMin, LongOnSequenceMax);
			if (j == longONFrequency)
			{
				for (int k = 0; k < longONFrequency; k++)
				{
					AkSoundEngine.PostEvent("LAMP_FLICKER_ON", gameObject);
					rend.sharedMaterial = lampMaterialOn;
					lampStateCheck = true;
					InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
					flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
					yield return new WaitForSeconds(flickerRangeShort);

					AkSoundEngine.PostEvent("LAMP_FLICKER_OFF", gameObject);
					rend.sharedMaterial = lampMaterialOff;
					lampStateCheck = false;
					InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
					flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
					yield return new WaitForSeconds(flickerRangeShort);
				}

				AkSoundEngine.PostEvent("LAMP_FLICKER_ON", gameObject);
				rend.sharedMaterial = lampMaterialOn;
				lampStateCheck = true;
				InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
				float randomflickerCount2 = Random.Range(1f, 4f);
				yield return new WaitForSeconds(randomflickerCount2);
			}
			int randomflickerCount = Random.Range(nrOfFlicksMin, nrOfFlicksMax);
			for (int k = 0; k < randomflickerCount; k++)
			{
				AkSoundEngine.PostEvent("LAMP_FLICKER_ON", gameObject);
				rend.sharedMaterial = lampMaterialOn;
				lampStateCheck = true;
				InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
				flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
				yield return new WaitForSeconds(flickerRangeShort);

				rend.sharedMaterial = lampMaterialOff;
				AkSoundEngine.PostEvent("LAMP_FLICKER_OFF", gameObject);
				lampStateCheck = false;
				InvokeLightMapSwitch(lampStateCheck, lampFlickerCheck, lightMapIndex);
				flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
				yield return new WaitForSeconds(flickerRangeShort);
			}
		}
		if (currentLampState != State.LAMP_FLICKERING)
		{
			yield return null;
		}
		else
		{
			Flickering = FlickeringSequence();
			StartCoroutine(Flickering);
		}
	}

	private void InvokeLightMapSwitch(bool lampStateCheck, bool lampFlickerCheck, int lightMapIndex)
	{
		if (LightMapSwitchCall != null)
		{
			LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
		}
	}

	private int CountArray(bool[] array, bool flag)
	{
		int value = 0;

		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == flag) value++;
		}

		return value;
	}
}