using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a temporary class which keeps track of the variables
/// and state of the moth, regarding its sounds and sound events. 
/// </summary>
public class MothSounds
{
    [SerializeField, Tooltip("The distance multiplier for the dubbler distance. ")]
    private float distMultiplier = 10f; 
    private Transform camTransform;
    private readonly MothBehaviour mothBehaviour;
    private readonly Transform mothTransform;

    public MothSounds(Transform cameraTransform, MothBehaviour mb, Transform mothTransform)
    {
        camTransform = cameraTransform;
        mothBehaviour = mb;
        this.mothTransform = mothTransform;
    }

    public void UpdateMothSounds()
    {
        if (camTransform == null)
            camTransform = GameObject.FindObjectOfType<Camera>().transform;

        AkSoundEngine.SetRTPCValue("MOTH_SPEED", mothBehaviour.MothSpeed);
        float dist2Cam = Vector3.Distance(mothTransform.position, camTransform.position) * distMultiplier;
        AkSoundEngine.SetRTPCValue("MOTH_DOPPLER",Mathf.Max(50 - dist2Cam, -50)); 
    }
}
