using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTapController : MonoBehaviour
{
    //private CameraController gm;

    private ParticleSystem tapParticle;
    private string particlePath;
    private Vector3 particlePos;
    private ParticleSystem instantiatedParticle;

    [SerializeField]
    private float particleLiveTime = 2f;

    private void OnEnable()
    {
        RunState.particleTapCall += TapParticle;
    }
    private void OnDisable()
    {
        RunState.particleTapCall -= TapParticle;
    }

    void TapParticle(Vector3 partPos)
    {
        particlePos = partPos;
        particlePath = "ResourcesPrefabs/Glow";
        tapParticle = (ParticleSystem)Resources.Load(particlePath, typeof(ParticleSystem));
        AkSoundEngine.PostEvent("TAP_PARTICLE", gameObject);

        if (instantiatedParticle != null)
        {
            StopAllCoroutines();
            var em = instantiatedParticle.emission;
            em.enabled = false;
            Destroy(instantiatedParticle.gameObject);
        }

        instantiatedParticle = ParticleSystem.Instantiate(tapParticle, particlePos, Quaternion.identity);

        StartCoroutine(ParticleTapEffect());
        return;
    }


    IEnumerator ParticleTapEffect()
    {
        var em = instantiatedParticle.emission;
        em.enabled = true;
        yield return new WaitForSeconds(particleLiveTime);
        em.enabled = false;

        yield return new WaitForSeconds(particleLiveTime);
        Destroy(instantiatedParticle.gameObject);
        yield return null;
    }
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
