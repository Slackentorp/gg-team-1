using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;

public class Builder : ScriptableObject
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string appName = "MOTH";
    static string targetDir = @"C:\Users\dadiu\OneDrive - Aalborg Universitet\Team 1 GG\Builds";

    // Windows Standalone build
    public static void PerformStandaloneWindowsBuild()
    {
        string executeable = appName + ".exe";
        GenericBuild(SCENES, targetDir + "/", executeable, BuildTarget.StandaloneWindows, BuildOptions.None, BuildTargetGroup.Standalone);
    }

    // Android build
    public static void PerformAndroidBuild()
    {
        string executeable = appName + ".apk";
        GenericBuild(SCENES, targetDir + "/", executeable, BuildTarget.Android, BuildOptions.None, BuildTargetGroup.Android);
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string targetDir, string executeableName, BuildTarget buildTarget, BuildOptions buildOptions, BuildTargetGroup buildGroup)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildGroup, buildTarget);

        // Date format: DDMMYYYY_HHMMSS
        DateTime now = DateTime.Now;
        CultureInfo culture = new CultureInfo("da-DK");
        string date = now.ToString(culture);

        date = date.Replace("-", "");
        date = date.Replace(":", "");
        date = date.Replace(" ", "_");
        date = date.Trim();

        string platform = buildTarget.ToString();

        string targetDirWithDate = targetDir + date + "_" + platform;

        String[] arguments = Environment.GetCommandLineArgs();
        if(arguments.Length > 7)
        {
            targetDirWithDate += "_" + arguments[7]; 
        }
        
        targetDirWithDate += "/" + executeableName;

        string res = BuildPipeline.BuildPlayer(scenes, targetDirWithDate, buildTarget, buildOptions);
        if (res.Length > 0)
        {
            throw new Exception("BuildPlayer failure: " + res);
        }
    }
}