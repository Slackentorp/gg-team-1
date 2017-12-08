using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(PlayableDirector))]
public class StoryEventController : Singleton<StoryEventController>
{
    public static bool isMuted;

    [SerializeField]
    List<StoryEvent> StoryEvents;

    StoryEvent currentStoryEvent;
    StoryEvent nullStoryEvent;
    Action currentCallback;
    PlayableDirector director;
    public bool isPosting;

    private GameObject[] outroObjects;
    public delegate void StoryEventLightAction(int index);
    public static event StoryEventLightAction StoryEventLightCall;

    void OnEnable()
    {
        SceneManager.sceneLoaded += HandleApartmentLoad;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleApartmentLoad;
    }

    void HandleApartmentLoad(Scene scene, LoadSceneMode mode)
    {
        Scene appartment = SceneManager.GetSceneByName("Apartment");
        if (scene == appartment && outroObjects.Length == 0)
        {
            outroObjects = GameObject.FindGameObjectsWithTag("OutroObject");
            foreach (var item in outroObjects)
            {
                item.SetActive(false);
            }
        }
    }

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.playableAsset = null;
        currentStoryEvent = nullStoryEvent;

        outroObjects = GameObject.FindGameObjectsWithTag("OutroObject");
        foreach (var item in outroObjects)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPosting)
        {
            if (!director.playableGraph.IsValid())
            {
                isPosting = false;
                ResetStoryEvent();
                if (GameController.Instance.CinemaBars.gameObject.activeInHierarchy)
                {
                    GameController.Instance.CinemaBars.SetTrigger("Up");
                }
            }
        }
    }

    public void PostStoryEvent(string StoryEvent, Action Callback)
    {
//        print("StoryEvent System: isPosting: " +isPosting +" - isMuted: " +isMuted);
        if (isPosting || isMuted)
        {
            return;
        }

        GameController.Instance.CinemaBars.gameObject.SetActive(true);
        SubToolXML.Instance.InitSubs(StoryEvent);

        try
        {
            StoryEvent se = StoryEvents.First(o => o.StoryEventID == StoryEvent);

            if (se.StoryEventID.Equals(StoryEvent))
            {
                print("Starting StoryEvent: " + StoryEvent);
                currentStoryEvent = se;
                currentCallback = Callback;
                if (StoryEvent.Equals("STORYEVENT_1"))
                {
                    Debug.Log("STORYEVENT_1");
                    StoryEventLightCall(1);
                }
                if (StoryEvent.Equals("STORYEVENT_2"))
                {
                    Debug.Log("STORYEVENT_2");
                    StoryEventLightCall(2);
                }
                if (StoryEvent.Equals("STORYEVENT_3"))
                {
                    Debug.Log("STORYEVENT_3");
                    StoryEventLightCall(3);
                }
                else if (StoryEvent.Equals("STORYEVENT_END"))
                {
                    HandleEnd();
                }

                if (GameController.Instance != null)
                {
                    Invoke(() => GameController.Instance.SetState(new PauseGameState(GameController.Instance)), Time.deltaTime);
                }

                director.Stop();
                director.playableAsset = se.TimelinePlayableAsset;
                director.time = 0;
                director.initialTime = 0;

                AkSoundEngine.PostEvent(se.FragmentWwiseEvent, gameObject);
                director.Play();

                se.StoryEventGroup.SetActive(true);
                isPosting = true;

                // Save state in playerprefs
                string lastChar = StoryEvent[StoryEvent.Length - 1].ToString();
                int storyEventNumber = -1;
                int.TryParse(lastChar, out storyEventNumber);
                if (storyEventNumber > -1)
                {
                    PlayerPrefs.SetInt("SE_REACHED", storyEventNumber);
                }
            }
        }
        catch (InvalidOperationException e)
        {
            print(e.Message);
        }

        if (!isPosting)
        {
            Debug.LogWarning("Story event did not start. Are you using the correct StoryEvent name?");
        }
    }

    public void ResetStoryEvent()
    {
        if (currentStoryEvent.StoryEventGroup != null)
        {
            print("Resetting story event system");
            director.Stop();
            director.playableAsset = null;
            currentStoryEvent.StoryEventGroup.SetActive(false);
            AkSoundEngine.StopAll(gameObject);
            if (GameController.Instance != null)
            {
                GameController.Instance.SetState(new RunState(GameController.Instance));
            }
            if (currentCallback != null)
            {
                if (currentStoryEvent.StoryEventID.Equals("STORYEVENT_3"))
                {
                    Invoke(() => HandleGamePlayPointOfNoReturn(), Time.deltaTime);
                }
                if (currentStoryEvent.StoryEventID.Equals("STORYEVENT_4"))
                {
                    GameController.instance.InvokeWwisePointOfNoReturn();
                }
                //if (currentStoryEvent.StoryEventID.Equals("STORYEVENT_END"))
                //{
                //    GameObject outroParent = GameObject.FindGameObjectWithTag("OutroParent");
                //    outroParent.GetComponent<PlayableDirector>().Play();
                //    foreach (var item in outroObjects)
                //    {
                //        item.SetActive(false);
                //    }
                //}
                currentCallback.Invoke();
            }

            currentStoryEvent = nullStoryEvent;
            currentCallback = null;
        }
    }

    private void HandleGamePlayPointOfNoReturn()
    {
        GameController.Instance.SetState(new PointOfNoReturnState(GameController.instance));
    }
    private void HandleEnd()
    {
        print("Handling end");
        GameObject outroParent = GameObject.FindGameObjectWithTag("OutroParent");
        outroParent.GetComponent<PlayableDirector>().Play();
        StartCoroutine(WaitingForAnimationToStop());

        foreach (var item in outroObjects)
        {
            item.SetActive(true);
        }
    }
    [SerializeField]
    private GameObject EndSceneFadeout;
    public  Animator _anim;
    IEnumerator WaitingForAnimationToStop()
    {
        EndSceneFadeout = GameObject.FindGameObjectWithTag("FadeOutCredits");
        _anim = EndSceneFadeout.GetComponent<Animator>();        
        
        Debug.Log("IT'S TOTALLY HAPPENING");
        yield return new WaitForSeconds(0);
        Debug.Log("ALMOST THERE 40 SECONDS");
         yield return new WaitForSeconds(40);

        _anim.SetBool("GameIsFinished", true);
        // EndSceneFadeout.SetActive(true);
    }



    [System.Serializable]
    public struct StoryEvent
    {
        [Tooltip("Name of the event the game will use to start this story event")]
        public string StoryEventID;
        [Tooltip("Name of the Wwise event to play while the story event is playing")]
        public string FragmentWwiseEvent;
        [Tooltip("A .playable file which defines the actual Timeline animations to play")]
        public PlayableAsset TimelinePlayableAsset;
        [Tooltip("The GameObject that holds all GameObjects neccesary for the story event. Should follow the \"StoryEvent Template\" prefab")]
        public GameObject StoryEventGroup;
    }

}