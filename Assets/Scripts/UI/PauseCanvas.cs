using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    public delegate void GammaSlider(float value);
    public static event GammaSlider OnGammaChange;

    public delegate void ContrastSlider(float value);
    public static event ContrastSlider OnContrastChange;

    private float sfxSliderPrev;
    private float musicSliderPrev;

    // Use this for initialization
    void Start()
    {
        sfxSliderPrev = sfxSlider.value;
        musicSliderPrev = musicSlider.value;
        englishLanguage = (PlayerPrefs.GetInt("LANGUAGE") == 0 ? true : false);
        languageButton.onClick.AddListener(() => OnLanguageButtonPressed(englishLanguage));
        subtitleButton.onClick.AddListener(() => OnSubtitleButtonPressed(subtitlesIsOn));
        menuButton.onClick.AddListener(() => GoToMenu());
        sfxSlider.onValueChanged.AddListener(delegate { SFXSlider(); });
        musicSlider.onValueChanged.AddListener(delegate { MusicSlider(); });
        gammaSlider.onValueChanged.AddListener(delegate { GammaSliderChanged(); });
        contrastSlider.onValueChanged.AddListener(delegate { ContrastSliderChanged(); });


        languageButton.GetComponentInChildren<Text>().text = "language: " + (!englishLanguage ? "english" : "dansk");


        // Calls wwise events to set initial values
        SFXSlider();
        MusicSlider();
    }

    private void SoundPress()
    {
        AkSoundEngine.PostEvent("MENUBUTTON_PRESS", this.gameObject);
    }

    public void GammaSliderChanged()
    {
        if (OnGammaChange != null)
        {
            OnGammaChange(gammaSlider.value);
        }
    }

    public void ContrastSliderChanged()
    {
        if (OnContrastChange != null)
        {
            OnContrastChange(contrastSlider.value);
        }
    }

    public void SFXSlider()
    {
        if (sfxSlider.value < 0.05f)
        {
            sfxSlider.value = 0f;
            AkSoundEngine.PostEvent("SFX_MUTE", this.gameObject);
        }
        else if (sfxSliderPrev <= 0.05f && sfxSlider.value > 0.05f)
        {
            AkSoundEngine.PostEvent("SFX_UNMUTE", this.gameObject);
        }
        AkSoundEngine.SetRTPCValue("SFX_VOLUME", sfxSlider.value);
        AkSoundEngine.PostEvent("SLIDER_SFX_RELEASE", this.gameObject);
        sfxSliderPrev = sfxSlider.value;
    }

    public void MusicSlider()
    {
        if (musicSlider.value < 0.05f)
        {
            musicSlider.value = 0f;
            AkSoundEngine.PostEvent("MUSIC_MUTE", this.gameObject);
        }
        else if (musicSliderPrev <= 0.05f && musicSlider.value > 0.05f)
        {
            AkSoundEngine.PostEvent("MUSIC_UNMUTE", this.gameObject);
        }
        AkSoundEngine.SetRTPCValue("MUSIC_VOLUME", musicSlider.value);
        AkSoundEngine.PostEvent("SLIDER_MUSIC_RELEASE", this.gameObject);
        musicSliderPrev = musicSlider.value;
    }

    private void GoToMenu()
    {
        GameController.Instance.LoadingPanel.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnLanguageButtonPressed(bool isEnglish)
    {
        Debug.Log("Language button pressed");
        englishLanguage = !englishLanguage;
        languageButton.GetComponentInChildren<Text>().text = "language: " + (isEnglish ? "english" : "dansk");

        if (isEnglish)
            GameController.Instance.SetEnglish();
        else
            GameController.Instance.SetDanish();

        AkSoundEngine.PostEvent("MENUBUTTON_PRESS", this.gameObject); 

        if (OnLanguageButton != null)
        {
            OnLanguageButton(isEnglish);
        }
    }

    private void OnSubtitleButtonPressed(bool isOn)
    {
        subtitlesIsOn = !subtitlesIsOn;
        subtitleButton.GetComponentInChildren<Text>().text = "subtitles: " + (!subtitlesIsOn ? "on" : "off");

        if (OnSubtitleButton != null)
        {
            OnSubtitleButton(isOn);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.lastPress.GetComponent<Slider>())
        {
            Debug.Log("Slider");
        }
    }
}
