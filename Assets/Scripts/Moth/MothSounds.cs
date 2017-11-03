using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a temporary class which keeps track of the variables
/// and state of the moth, regarding its sounds and sound events. 
/// </summary>
public class MothSounds : MonoBehaviour
{
    [SerializeField, Tooltip("The distance multiplier for the dubbler distance. ")]
    private float distMultiplier = 10f; 
    private Transform camTransform;
    private MothBehaviour moth; 

    // Use this for initialization
    void Start()
    {
        //HACK: This is temporary. 
        camTransform = GameObject.FindObjectOfType<Camera>().transform; 
        moth = GetComponent<MothBehaviour>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (camTransform == null)
            camTransform = GameObject.FindObjectOfType<Camera>().transform;

        AkSoundEngine.SetRTPCValue("MOTH_SPEED", moth.MothSpeed);
        float dist2Cam = Vector3.Distance(this.transform.position, camTransform.position) * distMultiplier;
        AkSoundEngine.SetRTPCValue("MOTH_DOPPLER",Mathf.Max(50 - dist2Cam, -50)); 
    }
}
