using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BootstrapEditor
{
    [MenuItem("Tools/Use Bootstraper", false, 0)]
    private static void UseBootstrap()
    {
        Menu.SetChecked("Tools/Use Bootstraper", EditorPrefs.GetBool("BoostrapEnabled", true));
        EditorPrefs.SetBool("BoostrapEnabled", !EditorPrefs.GetBool("BootstrapEnabled", true)); 
    }
}