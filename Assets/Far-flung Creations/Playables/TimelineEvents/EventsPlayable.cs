/***********************************************
 * Copyright © Far-Flung Creations Ltd.
 * Author: Marius George
 * Date: 25 October 2017
 * Email: marius@farflunggames.com	
 * DISCLAIMER: THE SOURCE CODE IN THIS FILE IS PROVIDED “AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL FAR-FLUNG CREATIONS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE
 * GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) SUSTAINED BY YOU OR A THIRD
 * PARTY, HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SAMPLE CODE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
**********************************************/

using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class EventsPlayable : PlayableAsset, ITimelineClipAsset
{
   public EventsPlayableBehaviour template = new EventsPlayableBehaviour();
   public ExposedReference<GameObject> TargetObject;

   public GameObject TrackTargetObject { get; set; }
   public float ClipStartTime { get; set; }
   public float ClipEndTime { get; set; }

   public ClipCaps clipCaps
   {
      get { return ClipCaps.None; }
   }

   public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
   {
      var playable = ScriptPlayable<EventsPlayableBehaviour>.Create(graph, template);
      EventsPlayableBehaviour clone = playable.GetBehaviour();
      clone.TargetObject = TargetObject.Resolve(graph.GetResolver());
      clone.ClipStartTime = ClipStartTime;
      clone.ClipEndTime = ClipEndTime;
      clone.TrackTargetObject = TrackTargetObject;
      return playable;
   }
}
