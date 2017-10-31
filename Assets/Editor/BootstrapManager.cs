
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class BootstrapLoader
{
    static string bootstrapSceneName = "Bootstrap";
    static BootstrapLoader()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.Log("Editor is playing");
            bool bootstrapLoaded = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == bootstrapSceneName)
                {
                    bootstrapLoaded = true;
                }
            }

            if (!bootstrapLoaded)
            {
                EditorApplication.update += Load;
            }
        }
    }

    static void Load()
    {
        EditorApplication.LoadLevelAdditiveInPlayMode("Assets/Scenes/"+ bootstrapSceneName + ".unity");
        Debug.Log("Bootstrap scene not loaded. Adding to active scenes.");
        EditorApplication.update -= Load;
    }
}