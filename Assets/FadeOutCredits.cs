using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FadeOutCredits : MonoBehaviour {

    void FadeOutCredit()
    {
            SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
    }
}
