using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private bool lampParticleStatus;
    private Renderer rend;
    private State currentLampState;
    [SerializeField]
    private bool[] localFragmentsState;
    [SerializeField]
    private bool lampStateCheck = false;
    private bool lampFlickerCheck = false;
    [SerializeField]
    private int lightMapIndex;
    private int getNrOfFragments = 0;
    private bool firstTimeFlickerCheck;

    private float flickerRangeLong, flickerRangeShort;

    [SerializeField]
    private bool isActivated;
    private bool lampFullOn;

    private AnimationCurve fragmentToLightsourceCurve;
    private IEnumerator Flickering;

    public bool Lit
    {
        get { return IsLit; }
        set { IsLit = value; }
    }

    public bool ISSwitchable
    {
        get { return isSwitchable; }
        set { isSwitchable = value; }
    }

    public bool LampActivated
    {
        get { return isActivated; }
        set { isActivated = value; }
    }
    public bool LampFullOn
    {
        get { return lampFullOn; }
        set { lampFullOn = value; }
    }

    public delegate void LightSourceAction();
    public static event LightSourceAction LightSourceCall;

    public delegate void LightMapSwitchAction(bool StateCheck, bool flickCheck, int indexNr);
    public static event LightMapSwitchAction LightMapSwitchCall;

    void OnEnable()
    {
        Interactable.InteractableCall += FragmentChecker;
    }

    void OnDisable()
    {
        Interactable.InteractableCall -= FragmentChecker;
    }

    private void Start()
    {
        fragmentToLightsourceCurve = GameController.Instance.FragmentToLightSourceCurve;
        FragmentChecker(null);
        firstTimeFlickerCheck = true;

    }

    public void FragmentChecker(Interactable sender)
    {
        int numPlayedFragments = interactables.Count(f => f.HasPlayed);

        if (interactables.Length == 0)
        {
            return;
        }

        if (interactables.Length == 3)
        {
            if (numPlayedFragments >= interactables.Length - 1)
            {
                LampON();
                if (sender != null)
                {
                    StartCoroutine(ParticleLerp(sender));
                }
            }
            else if (numPlayedFragments == interactables.Length - 2)
            {
                LampFlickering();
                if (sender != null)
                {
                    StartCoroutine(ParticleLerp(sender));
                }
            }
        }
        else if (interactables.Length == 1)
        {
            if (numPlayedFragments == interactables.Length)
            {
                LampON();
                if (sender != null)
                {
                    StartCoroutine(ParticleLerp(sender));
                }
            }
            else if (numPlayedFragments == 0)
            {
                LampFlickering();
                if (sender != null)
                {
                    StartCoroutine(ParticleLerp(sender));
                }
            }
        }

        LightSourceCallz();

    }

    public void LightSourceCallz()
    {
        if (LightSourceCall != null)
        {
            LightSourceCall();
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
        float endTime = fragmentToLightsourceCurve.keys[fragmentToLightsourceCurve.length - 1].time;

        GameObject particle = Instantiate(GameController.Instance.FragmentToLightSourceParticles, interactable.transform.position, Quaternion.identity);
        particle.transform.GetChild(0).gameObject.SetActive(true);
        particle.transform.GetChild(1).gameObject.SetActive(false);

        ParticleSystem explosionSystem = particle.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        while (time < endTime)
        {
            float t = fragmentToLightsourceCurve.Evaluate(time);

            particle.transform.position = Vector3.Lerp(interactable.transform.position, transform.position, t);

            time += Time.deltaTime;

            yield return null;
        }

        particle.transform.GetChild(1).gameObject.SetActive(true);
        FragmentChecker(null);
        yield return new WaitForSeconds(explosionSystem.main.duration + explosionSystem.main.startLifetime.constant + explosionSystem.main.startLifetime.constant / 2);
        Destroy(particle);
    }

    private void LightSwitch(State currentLampState)
    {
        if (GetComponentInChildren<Renderer>() != null &&
            GetComponentInChildren<ParticleSystem>() != null)
        {
            rend = GetComponentsInChildren<Renderer>()[1];
            particleSystemLamp = GetComponentsInChildren<ParticleSystem>()[0];

            if (currentLampState == State.LAMP_OFF)
            {
                StopCoroutine(Flickering);

                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;

                //LightMapSwitchCall(lampStateCheck, lightMapIndex);
            }
            else if (currentLampState == State.LAMP_FLICKERING)
            {
                if (firstTimeFlickerCheck)
                {
                    AkSoundEngine.PostEvent("LAMP_FLICKERING", gameObject);
                }
                lampFlickerCheck = true;

                Flickering = FlickeringSequence();
                StartCoroutine(Flickering);
                var em = particleSystemLamp.emission;
                em.enabled = true;
            }
            else if (currentLampState == State.LAMP_ON)
            {
                if (Flickering != null)
                {
                    StopCoroutine(Flickering);
                }

                AkSoundEngine.PostEvent("LAMP_ON", gameObject);
                lampFlickerCheck = false;
                rend.sharedMaterial = lampMaterialOn;
                var em = particleSystemLamp.emission;
                em.enabled = true;

            }
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