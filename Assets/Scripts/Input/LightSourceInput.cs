using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

/// <summary>
/// Touch input specifically for light sources
/// </summary>
public class LightSourceInput : MonoBehaviour, ITouchInput
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

    void OnEnable()
    {
        Fragment.FragmentCall += FragmentChecker;
    }

    void OnDisable()
    {
        Fragment.FragmentCall -= FragmentChecker;
    }

    //private void Update()
    //{
    //    FragmentChecker();
    //}

    public void FragmentChecker()
    {
        for (int i = 0; i < localFragmentsState.Length; i++)
        {
            localFragmentsState[i] = fragments[i].HasPlayed;
        }

        if (!localFragmentsState[0] && !localFragmentsState[1] && !localFragmentsState[2])
        {
            LampOFF();
        }
        else if (localFragmentsState[0] || localFragmentsState[1] || localFragmentsState[2])
        {
            if(localFragmentsState[0] && localFragmentsState[1])
            {
                LampON();
            }
            else if(localFragmentsState[1] && localFragmentsState[2])
            {
                LampON();
            }
            else
            {
                LampFlickering();
            }
        }

        LightSourceCall();
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
        LightSwitch(currentLampState);
    }
    private void LampFlickering()
    {
        currentLampState = State.LAMP_FLICKERING;
        LightSwitch(currentLampState);
    }
    private void LampON()
    {
        currentLampState = State.LAMP_ON;
        LightSwitch(currentLampState);
    }

    private void LightSwitch(State currentLampState)
    {
        if (GetComponentInChildren<Renderer>() != null &&
           GetComponentInChildren<ParticleSystem>() != null)
        {
            rend = GetComponentsInChildren<Renderer>()[0];
            particleSystemLamp = GetComponentsInChildren<ParticleSystem>()[0];

            if (currentLampState == State.LAMP_OFF)
            {
                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;
            }
            else if (currentLampState == State.LAMP_FLICKERING)
            {
                AkSoundEngine.PostEvent("LAMP_FLICKERING", gameObject);
                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;
            }
            else if (currentLampState == State.LAMP_ON)
            {
                AkSoundEngine.PostEvent("LAMP_ON", gameObject);
                rend.sharedMaterial = lampMaterialOn;
                var em = particleSystemLamp.emission;
                em.enabled = true;
                isActivated = true;
            }
        }
    }

    public void OnTap(Touch finger)
    {
        if (IsLit)
        {
            EventBus.Instance.SetMothPosition(transform.TransformPoint(LandingPosition));
        }
    }

    public void OnTouchDown(Touch finger, Vector3 worldPos)
    {
    }

    public void OnTouchUp(Touch finger)
    {
    }

    public void OnToucHold(Touch finger, Vector3 worldPos)
    {
    }

    public void OnTouchExit()
    {
    }

    public void OnSwipe(Touch finger, TouchDirection direction)
    {
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