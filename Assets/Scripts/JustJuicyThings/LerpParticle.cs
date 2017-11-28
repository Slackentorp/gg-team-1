using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EasyButtons;
using UnityEngine;

public class LerpParticle : MonoBehaviour {

	[SerializeField]
	private Vector3 pointStart, pointEnd;

	[SerializeField]
	private float time;
	[SerializeField, Tooltip("Should the \"Time\" variable override the particle duration of \"Trail Particle System\"")]
	private bool overrideParticleDuration;
	[SerializeField]
	private AnimationCurve VelocityOverTime;

	[SerializeField]
	private ParticleSystem TrailParticleSystem, EndParticleSystem;

	[Button]
	void Animate()
	{
		StartCoroutine(LerpBoi());
	}

	private IEnumerator LerpBoi()
	{
		if(time.Equals(0f) || VelocityOverTime.keys.Length == 0 || TrailParticleSystem == null ||  EndParticleSystem == null)
		{
			print("Set some vars dude");
			yield break;
		}

		float t = 0;

		if(overrideParticleDuration)
		{
			TrailParticleSystem.Stop();
			var ep = TrailParticleSystem.main;
			ep.duration = time;
		}
		
		TrailParticleSystem.Play();

		while(t < time)
		{
			float val = VelocityOverTime.Evaluate(t/time);
			transform.position = Vector3.Lerp(pointStart, pointEnd, val);
			t += Time.deltaTime;

			yield return null;
		}

		EndParticleSystem.Play();

	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(pointStart, .25f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(pointEnd, .25f);
	}
}
