using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OverlayBlendEffectManager : MonoBehaviour
{

    #region Variables
    public Shader curShader;
    public Texture2D blendTexture;
    public Texture2D perlinNoise;
    public Texture2D perlinNoise2;
    public float blendOpacity = 1.0f;
    public float perlinOpacity = 1.0f;
    public float fadeSpeed = 1.0f;
    private Material curMaterial;

    public float scratchesYSpeed = 10.0f;
    public float scratchesXSpeed = 10.0f;
    private float randomValue;
    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion

    private void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        if (!curShader && !curShader.isSupported)
        {
            enabled = false;
        }
    }

    private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetTexture("_BlendTex", blendTexture);
            material.SetTexture("_perlinTex", perlinNoise);
            material.SetTexture("_perlinTex2", perlinNoise2);
            material.SetFloat("_Opacity", blendOpacity);
            material.SetFloat("_perlinOpacity", perlinOpacity);
            material.SetFloat("_RandomValue", randomValue);
            //material.SetFloat("_FadeValue", fadeSpeed);
            material.SetFloat("_FadeValue,", fadeSpeed);

            Graphics.Blit(sourceTexture, destTexture, material);
        }
        if (blendTexture)
        {
            material.SetTexture("_ScratchesTex", blendTexture);
            material.SetFloat("_ScratchesYSpeed", scratchesYSpeed);
            material.SetFloat("_ScratchesXSpeed", scratchesXSpeed);
        }
        if (perlinNoise)
        {
            material.SetTexture("_perlinTex", perlinNoise);
        }
        if (perlinNoise2)
        {
            material.SetTexture("_perlinTex2", perlinNoise2);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    void Update()
    {
        blendOpacity = Mathf.Clamp(blendOpacity, 0.0f, 1.0f);
        perlinOpacity = Mathf.Clamp(perlinOpacity, 0.0f, 1.0f);

        randomValue = Random.Range(-1f, 1f);

    }

    private void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }

}

