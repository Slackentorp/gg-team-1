using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gg_team_1.Assets.Editor.EditorFunctionality
{
    public class LoadApartment
    {
        static AsyncOperation apartmentLoad;

        [MenuItem("Tools/Load Apartment")]
        public static void Load()
        {
            apartmentLoad = SceneManager.LoadSceneAsync("Apartment", LoadSceneMode.Additive);
            apartmentLoad.allowSceneActivation = true;
            while(!apartmentLoad.isDone)
            {
                EditorUtility.DisplayProgressBar("Hold On", "Loading Apartment", apartmentLoad.progress);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("Apartment"));
        }
    }
}