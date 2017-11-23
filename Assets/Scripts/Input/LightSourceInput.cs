using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;
using System.Collections;

/// <summary>
/// Touch input specifically for light sources
/// </summary>
public class LightSourceInput : MonoBehaviour
{
    [SerializeField]
    private Vector3 LandingPosition;
    [SerializeField]
    private Vector3 cameraPosition;
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

    private float flickerRangeLong, flickerRangeShort;

    [SerializeField]
    private bool isActivated;

    public Vector3 CameraPosition { get { return transform.TransformPoint(cameraPosition); } }

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
        FragmentChecker();
    }

    public void FragmentChecker()
    {
        localFragmentsState = new bool[interactables.Length];
        for (int i = 0; i < interactables.Length; i++)
        {
            localFragmentsState[i] = interactables[i].HasPlayed;
        }

        getNrOfFragments = CountArray(localFragmentsState, true);

        if (localFragmentsState.Length < 1)
        {
            return;
        }

        if (interactables.Length == 3)
        {
            if (getNrOfFragments == interactables.Length)
            {
                LampON();
            }
            else if (getNrOfFragments == interactables.Length - 2)
            {
                LampFlickering();
            }
        }
        else if (interactables.Length == 1)
        {
            if (getNrOfFragments == interactables.Length)
            {
                LampON();
            }
            else if (getNrOfFragments == 0)
            {
                LampFlickering();
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
    }
    private void LampON()
    {
        currentLampState = State.LAMP_ON;
        lampStateCheck = true;
        isActivated = true;

        LightSwitch(currentLampState);
        LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);

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
                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;

                //LightMapSwitchCall(lampStateCheck, lightMapIndex);
            }
            else if (currentLampState == State.LAMP_FLICKERING)
            {
                AkSoundEngine.PostEvent("LAMP_FLICKERING", gameObject);
                lampFlickerCheck = true;
                StartCoroutine(FlickeringSequence());
                var em = particleSystemLamp.emission;
                em.enabled = true;

                //LightMapSwitchCall(lampStateCheck, lightMapIndex);
            }
            else if (currentLampState == State.LAMP_ON)
            {
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
            LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
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
                    LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
                    flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
                    yield return new WaitForSeconds(flickerRangeShort);

                    rend.sharedMaterial = lampMaterialOff;
                    AkSoundEngine.PostEvent("LAMP_FLICKER_OFF", gameObject);
                    lampStateCheck = false;
                    LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
                    flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
                    yield return new WaitForSeconds(flickerRangeShort);
                }

                AkSoundEngine.PostEvent("LAMP_FLICKER_ON", gameObject);
                rend.sharedMaterial = lampMaterialOn;
                lampStateCheck = true;
                LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
                float randomflickerCount2 = Random.Range(1f, 4f);
                yield return new WaitForSeconds(randomflickerCount2);
            }
            int randomflickerCount = Random.Range(nrOfFlicksMin, nrOfFlicksMax);
            for (int k = 0; k < randomflickerCount; k++)
            {
                AkSoundEngine.PostEvent("LAMP_FLICKER_ON", gameObject);
                rend.sharedMaterial = lampMaterialOn;
                lampStateCheck = true;
                LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
                flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
                yield return new WaitForSeconds(flickerRangeShort);

                rend.sharedMaterial = lampMaterialOff;
                AkSoundEngine.PostEvent("LAMP_FLICKER_OFF", gameObject);
                lampStateCheck = false;
                LightMapSwitchCall(lampStateCheck, lampFlickerCheck, lightMapIndex);
                flickerRangeShort = Random.Range(flickTimeShortMin, flickTimeShortMax);
                yield return new WaitForSeconds(flickerRangeShort);
            }
        }
        if(currentLampState != State.LAMP_FLICKERING)
        {
            yield return null;
        }
        else
        {
            StartCoroutine(FlickeringSequence());
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.TransformPoint(LandingPosition), "MothIcon.tif", true);
        Gizmos.DrawIcon(transform.TransformPoint(cameraPosition), "CameraIcon.tif", true);
    }

    public Vector3 GetLandingPos()
    {
        return LandingPosition;
    }


}