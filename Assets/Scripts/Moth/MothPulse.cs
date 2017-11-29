using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothPulse : MonoBehaviour
{
    private Material curMaterial;
    private Renderer rend;
    [SerializeField]
    private float mothStartEmission = 10f;
    [SerializeField]
    private float mothRangeEmission = 5f;
    [SerializeField]
    private Shader mothShader;

    private void Start()
    {
        GetMaterial();
    }
    private void GetMaterial()
    {
        rend = GetComponent<Renderer>();
        if(rend != null){
            rend.material.shader = mothShader;
        }
    }

    private void PulseEffect()
    {
        if(rend == null) return;
        int rtpcType = (int)RTPCValue_type.RTPCValue_Global;
        float volume;

        AkSoundEngine.GetRTPCValue("DIALOGUE_VOLUME", null, 0, out volume, ref rtpcType);
        volume = ((volume + 48f + mothStartEmission) / 48f) * mothRangeEmission;
        rend.material.SetFloat("_EmissionIntensity", volume);
    }
    private void Update()
    {
        PulseEffect();
    }
}
