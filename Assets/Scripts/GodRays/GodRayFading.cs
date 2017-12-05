using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodRayFading : MonoBehaviour
{
    private Camera mainCam;
    private float distanceLightRay;
    private Color godrayAlpha;
    private Renderer rend;
    private float distanceInterpolation;
    private Color newAlpha;

    [SerializeField]
    private float fadeDistance = 2f;
    [SerializeField]
    private float maxAlpha = .14f;

    void Start()
    {
        mainCam = Camera.main;
        rend = GetComponent<Renderer>();
        newAlpha = rend.material.GetColor("_TintColor");
    }

    void Update()
    {
        LightRayDistanceCheck();
    }

    private void LightRayDistanceCheck()
    {
        distanceLightRay = (this.transform.position - mainCam.transform.position).magnitude;
        if (distanceLightRay < fadeDistance)
        {
            distanceInterpolation = (distanceLightRay - .7f) * maxAlpha / fadeDistance;

            if(distanceInterpolation <= 0)
            {
                return;
            }

            newAlpha.a = distanceInterpolation;
            rend.material.SetColor("_TintColor", newAlpha);
        }
    }

}
