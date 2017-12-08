using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BootstrapEditor
{
    public static bool isActive = true;
    static BootstrapEditor()
    {
        UseBootstrap(); 
    }


    [MenuItem("Tools/Use Bootstraper", false, 0)]
    private static void UseBootstrap()
    {
        Menu.SetChecked("Tools/Use Bootstraper", isActive);
        isActive = !isActive; 
        //EditorPrefs.SetBool("BootstrapEnabled", !isActive);
    }
}