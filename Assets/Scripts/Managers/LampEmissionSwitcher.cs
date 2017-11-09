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


    // Use this for initialization
    void Start()
    {
        GC = GameController.Instance.GetComponent<PuzzleChecker>();
        //lampMaterial = new Material[lampMaterial.Length];
    }

    void Update()
    {
        if (GC._SolvedPuzzels[0])
        {
            AssignLamps();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            AssignLamps();
        }
    }

    private void SwitchLampMat()
    {

    }

    [Button]
    private void AssignLamps()
    {
        isLitState = new bool[getLamps.Length];
        for (int i = 0; i < getLamps.Length; i++)
        {
            isLitState[i] = getLamps[i].GetComponent<LightSourceInput>().Lit;
        }
        SwitchLampMaterial();
    }

    private void SwitchLampMaterial()
    {

        for (int i = 0; i < getLamps.Length; i++)
        {
            currentMat = lampMaterialOn;
            Renderer rend = getLamps[i].GetComponentsInChildren<Renderer>()[1];
            //print("FUCK; " + getLamps[i].GetComponentsInChildren<Renderer>().Length);
            rend.sharedMaterial = currentMat;
        }
    }
}
