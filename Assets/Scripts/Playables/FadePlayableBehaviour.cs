using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

	public class FadePlayableBehaviour : PlayableBehaviour
	{
		public CanvasGroup canvasGroup;
		public FadeType fadeType;

		public enum FadeType
		{
			Fadein,
			Fadeout
		}

		public override void OnGraphStart (Playable playable)
		{
			canvasGroup.alpha = 0;
		}

		public override void OnBehaviourPlay (Playable playable, FrameData info)
		{
			canvasGroup.alpha = fadeType == FadeType.Fadeout ? 0 : 1;
		}

		public override void OnBehaviourPause (Playable playable, FrameData info)
		{
			#if UNITY_EDITOR

			var progressRate = playable.GetTime () / playable.GetDuration ();
			if (progressRate < 0.5f) {
				canvasGroup.alpha = fadeType == FadeType.Fadeout ? 0 : 1;
			} else {
				canvasGroup.alpha = fadeType == FadeType.Fadeout ? 1 : 0;
			}
			#else
				canvasGroup.alpha = fadeType == FadeType.Fadein ? 1 : 0;
			#endif
		}

		public override void PrepareFrame (Playable playable, FrameData info)
		{
			var progressRate = playable.GetTime () / playable.GetDuration ();
			canvasGroup.alpha = fadeType == FadeType.Fadeout ? (float)progressRate : (float)(1 - progressRate);
		}
	}
