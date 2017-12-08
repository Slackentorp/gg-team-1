using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class CreditsGoToMenu : MonoBehaviour {
    
    private static bool isChecked = false;
    string filePath = "";
    private void Awake()
    {
        filePath = Application.persistentDataPath + "/saved_game.stls";
    }
    IEnumerator clickCredits()
    {
        yield return new WaitForSeconds(2);
        Command();


    SceneManager.LoadScene("MainMenu");


        
    }

     void Command()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        PlayerPrefs.DeleteAll();
    }
}
