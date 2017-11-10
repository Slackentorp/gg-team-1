using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class LampEmissionSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject[] getLamps;
    [SerializeField]
    private bool[] isLitState;
    [SerializeField]
    private Material lampMaterialOn, lampMaterialOff, currentMat;
    private PuzzleChecker GC;
    private ParticleSystem particleSystemLamp;
    private bool lampParticleStatus;
    private Renderer rend;
    private bool checkLightsONOnce;
    private bool check = true;


    // Use this for initialization
    void Start()
    {
        GC = GameController.Instance.GetComponent<PuzzleChecker>();

        for (int i = 0; i < getLamps.Length; i++)
        {
            //   isLitState[i] = getLamps[i].GetComponent<LightSourceInput>().Lit;
        }
    }

    void Update()
    {
        if (GC._SolvedPuzzels[0])
        {
            if (check)
            {
                SwitchLampState();
                check = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SwitchLampState();
        }
    }

    private void SwitchLampMat()
    {

    }

    private void SwitchLampState()
    {
        isLitState = new bool[getLamps.Length];
        for (int i = 0; i < getLamps.Length; i++)
        {
            isLitState[i] = getLamps[i].GetComponent<LightSourceInput>().Lit;
            rend = getLamps[i].GetComponentsInChildren<Renderer>()[1];
            particleSystemLamp = getLamps[i].GetComponentsInChildren<ParticleSystem>()[0];

            if (getLamps[i].GetComponentsInChildren<Renderer>()[1].sharedMaterial == lampMaterialOn)    //isLitState[i])    //getLamps[i].GetComponentsInChildren<Renderer>()[1].sharedMaterial == lampMaterialOn)
            {
                // getLamps[i].GetComponent<LightSourceInput>().Lit = false;
                SwitchLampMaterial(lampMaterialOff);
                var em = particleSystemLamp.emission;
                em.enabled = false;
            }
            else if (getLamps[i].GetComponentsInChildren<Renderer>()[1].sharedMaterial == lampMaterialOff)   //isLitState[i])    //getLamps[i].GetComponentsInChildren<Renderer>()[1].sharedMaterial == lampMaterialOff)
            {
                // getLamps[i].GetComponent<LightSourceInput>().Lit = true;
                SwitchLampMaterial(lampMaterialOn);
                var em = particleSystemLamp.emission;
                em.enabled = true;
            }
        }
    }

    private void SwitchLampMaterial(Material materialStateOnOff)
    {
        currentMat = materialStateOnOff;

        rend.sharedMaterial = currentMat;
    }
}
