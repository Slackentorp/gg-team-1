using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EasyButtons;

namespace Team1.UI
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField]
        private string sceneToLoad;
        [SerializeField]
        private bool additively; 

        private Button button;

        private void LoadSceneButton(string sceneName, bool additive = false)
        {
            SceneManager.LoadScene(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
            BootstrapManager.Instance.ChangeLevelScene(sceneName);
        }

        void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(() => LoadSceneButton(sceneToLoad, additively));
        }
    }
}
