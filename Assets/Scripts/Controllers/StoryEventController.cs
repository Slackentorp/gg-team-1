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
	[SerializeField]
	List<StoryEvent> StoryEvents;

	StoryEvent currentStoryEvent;
	StoryEvent nullStoryEvent;
	Action currentCallback;
	PlayableDirector director;
	bool isPosting;

	void Awake()
	{
		director = GetComponent<PlayableDirector>();
		director.playableAsset = null;
		currentStoryEvent = nullStoryEvent;
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
		if (isPosting)
		{
			return;
		}

		try{
			StoryEvent se = StoryEvents.First(o => o.StoryEventID == StoryEvent);

			if (se.StoryEventID.Equals(StoryEvent))
			{
				currentStoryEvent = se;
				currentCallback = Callback;
				director.Stop();
				director.playableAsset = se.TimelinePlayableAsset;
				director.time = 0;
				director.initialTime = 0;
				AkSoundEngine.PostEvent(se.FragmentWwiseEvent, gameObject);
				director.Play();
				se.StoryEventGroup.SetActive(true);
				isPosting = true;
			}
		} catch (InvalidOperationException){ }

		if(!isPosting)
		{
			Debug.LogWarning("Story event did not start. Are you using the correct StoryEvent name?");
		}
	}

	private void ResetStoryEvent()
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
				currentCallback.Invoke();
			}

			currentStoryEvent = nullStoryEvent;
			currentCallback = null;
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