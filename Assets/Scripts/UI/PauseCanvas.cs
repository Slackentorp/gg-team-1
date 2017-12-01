using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseCanvas : MonoBehaviour, IPointerUpHandler
{
    [Header("Buttons")]
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Button languageButton;
    [SerializeField]
    private Button subtitleButton;
    [SerializeField]
    private Button camInverseXButton;
    [SerializeField]
    private Button camInverseYButton;

    [Header("Sliders")]
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider gammaSlider;
    [SerializeField]
    private Slider contrastSlider;

    private bool subtitlesIsOn = true;
    private bool englishLanguage;
    
    public delegate void SubtitlesButton(bool isOn);
    public static event SubtitlesButton OnSubtitleButton;

    public delegate void LanguageButton(bool isEnglish);
    public static event LanguageButton OnLanguageButton;

    // Use this for initialization
    void Start()
    {
        englishLanguage = (PlayerPrefs.GetInt("LANGUAGE") == 0 ? true : false);
        languageButton.onClick.AddListener(() => OnLanguageButtonPressed(englishLanguage));
        subtitleButton.onClick.AddListener(() => OnSubtitleButtonPressed(subtitlesIsOn));
    }



    private void SoundPress()
    {
        AkSoundEngine.PostEvent("MENUBUTTON_PRESS", this.gameObject);
    }

    private void SliderRelease()
    {
        AkSoundEngine.PostEvent("SLIDER_SFX_RELEASE", this.gameObject);
    }


    private void OnLanguageButtonPressed(bool isEnglish)
    {
        Debug.Log("Language button pressed");
        englishLanguage = !englishLanguage; 
        languageButton.GetComponentInChildren<Text>().text = "language: " + (isEnglish ? "english" : "dansk");
        if (OnLanguageButton != null)
        {
            OnLanguageButton(isEnglish);
        }
    }

    private void OnSubtitleButtonPressed(bool isOn)
    {
        subtitlesIsOn = !subtitlesIsOn;
        subtitleButton.GetComponentInChildren<Text>().text = "subtitles: " + (subtitlesIsOn ? "on" : "off");

        if (OnSubtitleButton != null)
        {
            OnSubtitleButton(isOn);
        }
    }

    private void ReturnToMenu()
    {
        Debug.Log("Should return to menu");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
