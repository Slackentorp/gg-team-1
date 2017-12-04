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

    private GameObject currentMenu;
    private SpriteRenderer[] puzzleSprites;
    private Text[] menuTexts;

    AsyncOperation bootstrapLoad;
    AsyncOperation soundScapeLoad;
    AsyncOperation apartmentLoad;

    // Use this for initialization
    void Start()
    {
        bootstrapLoad =
    SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
        soundScapeLoad =
            SceneManager.LoadSceneAsync("SoundScape", LoadSceneMode.Additive);
        apartmentLoad =
            SceneManager.LoadSceneAsync("Apartment", LoadSceneMode.Additive);

        bootstrapLoad.allowSceneActivation = false;
        soundScapeLoad.allowSceneActivation = false;
        apartmentLoad.allowSceneActivation = false;

        continueButton.onClick.AddListener(() => StartGame(1));
        newGameButton.onClick.AddListener(() => StartGame(0));
        tapToStartButton.onClick.AddListener(() => StartGame(0));

        // Don't show Continue Button if there is nothing to continue
        if(!SaveLoad.SavegameExists())
        {
            continueButton.gameObject.SetActive(false);
        }

        puzzleSprites = puzzleContainer.GetComponentsInChildren<SpriteRenderer>();
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


    private void ShowMenu()
    {
        if (false)
        {
            currentMenu = firstMenu;
        }
        else
        {
            currentMenu = mainMenu;
        }

        menuTexts = currentMenu.GetComponentsInChildren<Text>();
        currentMenu.SetActive(true);

    }

    private void StartGame(int saveload)
    {
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
        print("Triggered load scenes");
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
