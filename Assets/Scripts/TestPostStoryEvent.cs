using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

public class TestPostStoryEvent : MonoBehaviour {

	public string eventName;
	[Button]
	void PostEvent()
	{
		StoryEventController.Instance.PostStoryEvent(eventName, EndOfEvent);
	}

	private void EndOfEvent()
	{
		print("End of StoryEvent: " +eventName);
	}
}
