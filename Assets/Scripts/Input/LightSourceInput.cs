using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

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
    private Fragment[] fragments;

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
    [SerializeField]
    private int lightMapIndex;
    private int getNrOfFragments = 0;

    [SerializeField]
    private bool isActivated;

    public bool ACTIVATE;

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

    public delegate void LightMapSwitchAction(bool StateCheck, int indexNr);
    public static event LightMapSwitchAction LightMapSwitchCall;

    void OnEnable()
    {
        Fragment.FragmentCall += FragmentChecker;
    }

    void OnDisable()
    {
        Fragment.FragmentCall -= FragmentChecker;
    }

    private void Start()
    {
        //isActivated = true;
        //ACTIVATE = true; 
    }

    public void FragmentChecker()
    {
        localFragmentsState = new bool[fragments.Length];
        for (int i = 0; i < fragments.Length; i++)
        {
            localFragmentsState[i] = fragments[i].HasPlayed;
        }

        getNrOfFragments = CountArray(localFragmentsState, true);

        if (localFragmentsState.Length < 1)
        {
            return;
        }

        if (fragments.Length == 3)
        {
            if (getNrOfFragments == fragments.Length
               || getNrOfFragments == fragments.Length - 1)
            {
                LampON();
            }
        }
        else if (getNrOfFragments == fragments.Length)
        {
            LampON();
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
        LightMapSwitchCall(lampStateCheck, lightMapIndex);

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
                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;

                //LightMapSwitchCall(lampStateCheck, lightMapIndex);
            }
            else if (currentLampState == State.LAMP_ON)
            {
                AkSoundEngine.PostEvent("LAMP_ON", gameObject);
                rend.sharedMaterial = lampMaterialOn;
                var em = particleSystemLamp.emission;
                em.enabled = true;

            }
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