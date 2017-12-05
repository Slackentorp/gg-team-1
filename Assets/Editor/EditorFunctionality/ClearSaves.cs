using System.IO;
using UnityEditor;
using UnityEngine;

namespace gg_team_1.Assets.Editor.EditorFunctionality
{
    [InitializeOnLoad]
    public static class ClearSaves
    {
        static string filePath = Application.persistentDataPath + "/saved_game.stls";
        private static bool isChecked = false;

        [MenuItem("Tools/Clear Save Data")]
        static void Command()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Tools/Autoload")]
        public static void StartInspection()
        {
            isChecked = !isChecked;
            Menu.SetChecked("Tools/Autoload", isChecked);
            if(isChecked)
            {
                PlayerPrefs.SetInt("savelaod", 1);
            } else {
                PlayerPrefs.SetInt("savelaod", 0);
            }
        }
    }
}