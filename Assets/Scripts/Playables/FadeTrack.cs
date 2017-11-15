using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
	[System.Serializable]
	[TrackMediaType (TimelineAsset.MediaType.Script)]
	[TrackClipType (typeof(FadePlayable))]
	[TrackBindingType (typeof(CanvasGroup))]
	[TrackColor (0.2f, 1f, 0)]
	public class FadeTrack : TrackAsset
	{
		protected override Playable CreatePlayable (PlayableGraph graph, GameObject go, TimelineClip clip)
		{
			var mixer = ScriptPlayable<FadePlayableBehaviour>.Create (graph);

			var content = go.GetComponent<PlayableDirector> ();
			var canvasGroup = content.GetGenericBinding (this) as CanvasGroup;
			var fadeAsset = clip.asset as FadePlayable;
			mixer.GetBehaviour ().canvasGroup = canvasGroup;
			mixer.GetBehaviour ().fadeType = fadeAsset.fadeType;

			clip.displayName = fadeAsset.fadeType.ToString ();
			return mixer;
		}
	}
