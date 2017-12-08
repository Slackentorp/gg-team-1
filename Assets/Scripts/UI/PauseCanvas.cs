using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Assets.Scripts.Managers;

public class PauseCanvas : MonoBehaviour
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

    [SerializeField]
    private Text langSetting;
    private Text subtSetting;

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

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        InputManager.pauseShown = false;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        InputManager.pauseShown = true;

        if (GameController.Instance != null)
        {
            if (SaveLoad.SaveGame(GameController.Instance))
            {
                print("Save complete.");
            }
            else
            {
                print("Save unsucessful.");
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        sfxSliderPrev = sfxSlider.value;
        musicSliderPrev = musicSlider.value;
        englishLanguage = (PlayerPrefs.GetInt("LANGUAGE", 0) == 1 ? true : false);
        subtitlesIsOn = PlayerPrefs.GetInt("SUBTITLESON", 0) == 0 ? true : false;
        musicSlider.value = PlayerPrefs.GetFloat("MUSIC", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);

        if (languageButton != null)
        {
            languageButton.onClick.AddListener(() =>
            {
                OnLanguageButtonPressed(englishLanguage);
                SoundPress();
            });
            langSetting = languageButton.GetComponentsInChildren<Text>()[1];
            langSetting.text = (englishLanguage ? "english" : "dansk");
            //OnLanguageButtonPressed(englishLanguage);
        }

        if (subtitleButton != null)

        {
            subtitleButton.onClick.AddListener(() =>
            {
                OnSubtitleButtonPressed(subtitlesIsOn);
                SoundPress();
            });
            subtSetting = subtitleButton.GetComponentsInChildren<Text>()[1];
            if (englishLanguage)
                subtSetting.text = (subtitlesIsOn ? "til" : "fra");
            else
                subtSetting.text = (subtitlesIsOn ? "on" : "off");
        }

        if (menuButton != null)
            menuButton.onClick.AddListener(() =>
            {
                GoToMenu();
                SoundPress();
            });

        sfxSlider.onValueChanged.AddListener(delegate { SFXSlider(); });
        musicSlider.onValueChanged.AddListener(delegate { MusicSlider(); });
        gammaSlider.onValueChanged.AddListener(delegate { GammaSliderChanged(); });
        contrastSlider.onValueChanged.AddListener(delegate { ContrastSliderChanged(); });




        // Calls wwise events to set initial values
        SFXSlider();
        MusicSlider();
    }

    public void SetLang(GameObject button)
    {
        englishLanguage = !englishLanguage;
        PlayerPrefs.SetInt("LANGUAGE", englishLanguage ? 1 : 0);
        button.GetComponent<Text>().text = (englishLanguage ? "english" : "dansk");
    }

    public void SoundPress()
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
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
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
        PlayerPrefs.SetFloat("MUSIC", musicSlider.value);
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
        langSetting.text = (!englishLanguage ? "english" : "dansk");
        PlayerPrefs.SetInt("LANGUAGE", englishLanguage ? 1 : 0);

        if (GameController.Instance != null)
        {
            if (isEnglish)
                GameController.Instance.SetEnglish();
            else
                GameController.Instance.SetDanish();
        }
        else
        {
            LocalizationManager localizationManager = new LocalizationManager();
            if (isEnglish)
            {
                subtSetting.text = (subtitlesIsOn ? "on" : "off");
                localizationManager.SetEnglish();
            }
            else
            {
                subtSetting.text = (subtitlesIsOn ? "til" : "fra");
                localizationManager.SetDanish(); 
            }
        }

        AkSoundEngine.PostEvent("MENUBUTTON_PRESS", this.gameObject);

        //PlayerPrefs.SetInt("LANGUAGE", isEnglish ? 0 : 1);


        if (OnLanguageButton != null)
        {
            OnLanguageButton(englishLanguage);
        }
    }

    private void OnSubtitleButtonPressed(bool isOn)
    {
        subtitlesIsOn = !subtitlesIsOn;
        if (englishLanguage)
            subtSetting.text = (subtitlesIsOn ? "til" : "fra");
        else
            subtSetting.text = (subtitlesIsOn ? "on" : "off");

        PlayerPrefs.SetInt("SUBTITLESON", subtitlesIsOn ? 0 : 1);

        if (OnSubtitleButton != null)
        {
            OnSubtitleButton(isOn);
        }
    }
}
