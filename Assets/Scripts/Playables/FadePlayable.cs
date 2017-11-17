using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// A behaviour that is attached to a playable
public class FadePlayable : PlayableAsset
{
    public FadePlayableBehaviour.FadeType fadeType;

		public CanvasGroup canvasGroup{ get; set; }

		public override Playable CreatePlayable (PlayableGraph graph, GameObject go)
		{
			var playable = ScriptPlayable<FadePlayableBehaviour>.Create (graph);
			playable.GetBehaviour ().fadeType = fadeType;

			return playable;
		}

		public ClipCaps clipCaps {
			get {
				return ClipCaps.None;
			}
		}
}
