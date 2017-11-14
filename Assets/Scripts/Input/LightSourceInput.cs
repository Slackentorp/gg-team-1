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
    private GameObject[] getFragments;

    [SerializeField]
    private Material lampMaterialOn, lampMaterialOff;
    private ParticleSystem particleSystemLamp;
    private bool lampParticleStatus;
    private Renderer rend;
    // private currentLightmap;
    private State currentLampState;
    private bool[] localFragmentsState = new bool[3];

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

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F4))
        //{
        //    LampOFF();
        //}
        //if (Input.GetKeyDown(KeyCode.F5))
        //{
        //    LampON();
        //}
        //if (Input.GetKeyDown(KeyCode.F6))
        //{
        //    LampFlickering();
        //}
        FragmentChecker();
    }

    public void FragmentChecker()
    {
        for (int i = 0; i < localFragmentsState.Length; i++)
        {
            //print("local: " + localFragmentsState.Length);
            //print("frag: " + getFragments.Length);
            localFragmentsState[i] = getFragments[i].GetComponent<Fragment>().HasPlayed;
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
                rend.sharedMaterial = lampMaterialOff;
                var em = particleSystemLamp.emission;
                em.enabled = false;
            }
            else if (currentLampState == State.LAMP_ON)
            {
                rend.sharedMaterial = lampMaterialOn;
                var em = particleSystemLamp.emission;
                em.enabled = true;
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