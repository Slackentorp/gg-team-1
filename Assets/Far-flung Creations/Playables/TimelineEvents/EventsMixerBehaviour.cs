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

using UnityEngine.Playables;

public class EventsMixerBehaviour : PlayableBehaviour
{
   private float _currentTime;

   // NOTE: This function is called at runtime and edit time.  Keep that in mind when setting the values of properties.
   public override void ProcessFrame(Playable playable, FrameData info, object playerData)
   {
      /*GameObject trackBinding = playerData as GameObject;

      if (!trackBinding)
         return;*/

      int inputCount = playable.GetInputCount();

      float time = (float)playable.GetGraph().GetRootPlayable(0).GetTime();

      for (int i = 0; i < inputCount; i++)
      {
         float inputWeight = playable.GetInputWeight(i);
         ScriptPlayable<EventsPlayableBehaviour> inputPlayable = (ScriptPlayable<EventsPlayableBehaviour>)playable.GetInput(i);
         EventsPlayableBehaviour input = inputPlayable.GetBehaviour();

         if ((_currentTime <= input.ClipStartTime) && (time > input.ClipStartTime))
         {
            if (input.ClipAction_ClipStart != null) input.ClipAction_ClipStart.Invoke();
            if (input.TrackAction_ClipStart != null) input.TrackAction_ClipStart.Invoke();
         }

         if ((_currentTime <= input.ClipEndTime) && (time > input.ClipEndTime))
         {
            if (input.ClipAction_ClipEnd != null) input.ClipAction_ClipEnd.Invoke();
            if (input.TrackAction_ClipEnd != null) input.TrackAction_ClipEnd.Invoke();
         }
      }

      _currentTime = time;
   }
}
