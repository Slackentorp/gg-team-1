using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsGoToMenu : MonoBehaviour {
    
    IEnumerator clickCredits()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);

        
    }
}
