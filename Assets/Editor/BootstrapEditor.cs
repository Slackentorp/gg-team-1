using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BootstrapEditor
{
    [MenuItem("Tools/Enable Bootstrap")]
    private static void EnableBootstrap()
    {
        EditorPrefs.SetBool("BootstrapEnabled", true);
    }

    [MenuItem("Tools/Disable Bootstrap")]
    private static void DisableBootstrap()
    {
        EditorPrefs.SetBool("BootstrapEnabled", false);
    }

    [MenuItem("Tools/Enable Bootstrap", true)]
    private static bool ValidateDisabled()
    {
        return !EditorPrefs.GetBool("BootstrapEnabled");
    }

    [MenuItem("Tools/Disable Bootstrap", true)]
    private static bool ValidateEnabled()
    {
        return EditorPrefs.GetBool("BootstrapEnabled");
    }
}