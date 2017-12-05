using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyWwiseCinemachineSounds : MonoBehaviour 
{

	[HideInInspector]
	public enum Particles {LampParticle, MergedParticle};

	public Particles setting = Particles.LampParticle;
	public bool soundOnDisable = false;


	void OnEnable ()
	{

		if (setting == Particles.LampParticle) 
		{
			//AkSoundEngine.PostEvent ("PARTICLE_APPEAR_LAMP", gameObject);
		} 
		else if (setting == Particles.MergedParticle) 
		{
			//AkSoundEngine.PostEvent("PARTICLE_MERGE", gameObject);
		}
	}


	void OnDisable()
	{

		//if (soundOnDisable)
		//AkSoundEngine.PostEvent("PARTICLE_ENTER_FOGWALL", gameObject);

	}

}
