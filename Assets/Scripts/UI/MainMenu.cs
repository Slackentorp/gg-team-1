using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private float fadeDelay = 0.7f;
    [SerializeField]
    private float fadeTime = 2f;

    [Header("Opening panels")]
    [SerializeField]
    private GameObject languageSelect;
    [SerializeField]
    private Button dkButton;
    [SerializeField]
    private Button enButton;
    [SerializeField]
    private GameObject headphonesPanel;

    [Header("Buttons")]
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button tapToStartButton;


    [Header("GameObjects")]
    [SerializeField]
    private GameObject firstMenu;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject puzzleContainer;
    [SerializeField]
    private SpriteRenderer bgImage;
    [SerializeField]
    private MenuPuzzle puzzle;

    private GameObject currentMenu;
    private SpriteRenderer[] puzzleSprites;
    private Text[] menuTexts;
    private LocalizationManager localizationManager;

    AsyncOperation bootstrapLoad;
    AsyncOperation soundScapeLoad;
    AsyncOperation apartmentLoad;
    AsyncOperation storyEventsLoad;

    private void OnEnable()
    {
        AkSoundEngine.PostEvent("GAME_START", this.gameObject);
        MenuPuzzle.OnFinished += OnPuzzleSolved;
    }

    private void OnDisable()
    {
        MenuPuzzle.OnFinished -= OnPuzzleSolved;
    }

#if UNITY_EDITOR
    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("Bootstrap");
        SceneManager.UnloadSceneAsync("SoundScape");
        SceneManager.UnloadSceneAsync("Apartment");
    }
#endif

    // Use this for initialization
    void Start()
    {
        localizationManager = new LocalizationManager();

        bootstrapLoad =
    SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
        soundScapeLoad =
            SceneManager.LoadSceneAsync("SoundScape", LoadSceneMode.Additive);
        storyEventsLoad =
            SceneManager.LoadSceneAsync("StoryEvents", LoadSceneMode.Additive);
        apartmentLoad =
            SceneManager.LoadSceneAsync("Apartment", LoadSceneMode.Additive);

        bootstrapLoad.allowSceneActivation = false;
        soundScapeLoad.allowSceneActivation = false;
        apartmentLoad.allowSceneActivation = false;
        storyEventsLoad.allowSceneActivation = false;

        continueButton.onClick.AddListener(() => StartGame(1));
        newGameButton.onClick.AddListener(() => StartGame(0));
        tapToStartButton.onClick.AddListener(() => StartGame(0));

        dkButton.onClick.AddListener(() =>
        {
            SetLang(0);
            localizationManager.SetDanish();
        });

        enButton.onClick.AddListener(() =>
        {
            SetLang(1);
            localizationManager.SetEnglish();
        });

        // Don't show Continue Button if there is nothing to continue
        if (!SaveLoad.SavegameExists())
        {
            continueButton.gameObject.SetActive(false);
            languageSelect.SetActive(true);
        }
        else
        {
            StartCoroutine(HeadphonesCanvas(true));
        }

        languageSelect.transform.parent.gameObject.SetActive(true);

        puzzleSprites = puzzleContainer.GetComponentsInChildren<SpriteRenderer>();
    }


    private void StartPuzzle()
    {
        puzzle.InitializePuzzle();
    }

    private void OnPuzzleSolved()
    {
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadGame());
        }
    }

    private void SetLang(int lang)
    {
        languageSelect.SetActive(false);
        StartCoroutine(HeadphonesCanvas(false));
    }


    IEnumerator HeadphonesCanvas(bool toPuzzle)
    {
        headphonesPanel.SetActive(true);
        yield return new WaitForSeconds(2f);

        StartPuzzle();

        headphonesPanel.transform.parent.gameObject.SetActive(false);
        headphonesPanel.SetActive(false);
        AkSoundEngine.PostEvent("MAINMENU_OPEN", this.gameObject);
    }

    private void ShowMenu()
    {
        if (!SaveLoad.SavegameExists())
        {
            currentMenu = firstMenu;
        }
        else
        {
            currentMenu = mainMenu;
        }

        continueButton.enabled = true;
        newGameButton.enabled = true;
        menuTexts = currentMenu.GetComponentsInChildren<Text>();
        currentMenu.SetActive(true);

    }

    private void StartGame(int saveload)
    {
        continueButton.enabled = false;
        newGameButton.enabled = false;
        tapToStartButton.enabled = false;
        firstMenu.SetActive(false);

        AkSoundEngine.PostEvent("MAINMENU_START_GAME", this.gameObject);
        PlayerPrefs.SetInt("saveload", saveload);
        StartCoroutine(LoadGame());
    }

    IEnumerator LoadGame()
    {
        StartCoroutine(FadeMenuText());
        yield return new WaitForSeconds(fadeDelay);
        StartCoroutine(FadeTitlePuzzle());
        yield return new WaitForSeconds(fadeDelay);
        yield return FadeBackground();
        StartCoroutine(LoadScenes());
    }

    IEnumerator FadeTitlePuzzle()
    {
        float t = 0f;

        Color tmpColor = puzzleSprites[0].color;

        while (t < 1f)
        {
            tmpColor.a = 1f - t;
            for (int i = 0; i < puzzleSprites.Length; i++)
            {
                puzzleSprites[i].color = tmpColor;
            }

            t += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeMenuText()
    {

        float t = 0f;

        Color tmpColor = menuTexts[0].color;

        while (t < 1f)
        {
            tmpColor.a = 1f - t;
            for (int i = 0; i < menuTexts.Length; i++)
            {
                menuTexts[i].color = tmpColor;
            }

            t += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeBackground()
    {
        float t = 0f;
        Color tmpColor = bgImage.color;

        while (t < 1f)
        {
            tmpColor.a = 1f - t;
            bgImage.color = tmpColor;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }

        tmpColor.a = 0f;
        bgImage.color = tmpColor;
    }

    IEnumerator LoadScenes()
    {
        SceneManager.sceneLoaded += ApartmentLoaded;

        apartmentLoad.allowSceneActivation = true;
        bootstrapLoad.allowSceneActivation = true;
        soundScapeLoad.allowSceneActivation = true;
        storyEventsLoad.allowSceneActivation = true;
        yield return null;
    }

    private void ApartmentLoaded(Scene scene, LoadSceneMode mode)
    {
        Scene appartment = SceneManager.GetSceneByName("Apartment");
        if (scene != appartment)
        {
            return;
        }

        SceneManager.SetActiveScene(appartment);
    }
}
