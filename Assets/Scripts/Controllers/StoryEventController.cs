using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class StoryEventController : Singleton<StoryEventController>
{
	public bool isMuted;

	[SerializeField]
	List<StoryEvent> StoryEvents;

	StoryEvent currentStoryEvent;
	StoryEvent nullStoryEvent;
	Action currentCallback;
	PlayableDirector director;
	bool isPosting;

	private GameObject[] outroObjects;

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
			}
		}
	}

	public void PostStoryEvent(string StoryEvent, Action Callback)
	{
		if (isPosting || isMuted)
		{
			return;
		}

		try{
			StoryEvent se = StoryEvents.First(o => o.StoryEventID == StoryEvent);

			if (se.StoryEventID.Equals(StoryEvent))
			{
				currentStoryEvent = se;
				currentCallback = Callback;
				if(StoryEvent.Equals("STORYEVENT_3"))
				{
					HandlePointOfNoReturn();
				} else if(StoryEvent.Equals("STORYEVENT_END"))
				{
					HandleEnd();
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
				if(storyEventNumber > -1)
				{
					PlayerPrefs.SetInt("SE_REACHED", storyEventNumber);
				}
			}
		} catch (InvalidOperationException e){ 
			print(e.Message);
		}

		if(!isPosting)
		{
			Debug.LogWarning("Story event did not start. Are you using the correct StoryEvent name?");
		}
	}

	public void ResetStoryEvent()
	{
		if(currentStoryEvent.StoryEventGroup != null)
		{
			print("Resetting story event system");
			director.Stop();
			director.playableAsset = null;
			currentStoryEvent.StoryEventGroup.SetActive(false);
			AkSoundEngine.StopAll(gameObject);
			if(currentCallback != null)
			{
				if(currentStoryEvent.StoryEventID.Equals("STORYEVENT_4"))
				{
					GameController.instance.InvokePointOfNoReturn();
				}
				if(currentStoryEvent.StoryEventID.Equals("STORYEVENT_END"))
				{
					GameObject outroParent = GameObject.FindGameObjectWithTag("OutroParent");
					outroParent.GetComponent<PlayableDirector>().Play();
					foreach (var item in outroObjects)
					{
						item.SetActive(true);
					}
				}
				currentCallback.Invoke();
			}

			currentStoryEvent = nullStoryEvent;
			currentCallback = null;
		}
	}

	private void HandlePointOfNoReturn()
	{
		GameController.Instance.SetState(new PointOfNoReturnState(GameController.instance));
	}
	private void HandleEnd()
	{
		print("Handling end");
		GameObject outroParent = GameObject.FindGameObjectWithTag("OutroParent");
		outroParent.GetComponent<PlayableDirector>().Play();
		foreach (var item in outroObjects)
		{
			item.SetActive(true);
		}
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