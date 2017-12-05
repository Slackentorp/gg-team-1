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

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

[TrackColor(0.4448276f, 0f, 1f)]
[TrackClipType(typeof(EventsPlayable))]
[TrackBindingType(typeof(GameObject))]
public class EventsTrack : TrackAsset
{
   public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
   {
      var director = go.GetComponent<PlayableDirector>();
      var trackTargetObject = director.GetGenericBinding(this) as GameObject;

      foreach (var clip in GetClips())
      {
         var playableAsset = clip.asset as EventsPlayable;

         if (playableAsset)
         {
            playableAsset.TrackTargetObject = trackTargetObject;
            playableAsset.ClipStartTime = (float)clip.start;
            playableAsset.ClipEndTime = (float)clip.end;
         }
      }

      var scriptPlayable = ScriptPlayable<EventsMixerBehaviour>.Create(graph, inputCount);
      return scriptPlayable;
   }
}
