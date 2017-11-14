using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    public void Play()
    {
        print("Playing fragment on GameObject: " +gameObject.name);

        // Trigger Wwise event
        // Enable Particles
        // Lerp up and down emission on Material[0]
        // Set camera position
        // Set Moth position
        // ... Optional: Set moth animation state
    }
}