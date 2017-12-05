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

[Serializable]
public class EventsPlayableBehaviour : PlayableBehaviour
{
   public string ClipEventHandler_ClipStart;
   public string ClipEventHandler_ClipEnd;
   public string TrackEventHandler_ClipStart;
   public string TrackEventHandler_ClipEnd;
   public bool EnableTrackEvents;
   public bool EnableClipEvents;
   public bool InvokeEventsInEditMode;
   public GameObject TargetObject;
   public GameObject TrackTargetObject;

   public float ClipStartTime;
   public float ClipEndTime;

   private Action _clipAction_ClipStart;
   private Action _clipAction_ClipEnd;
   private Action _trackAction_ClipStart;
   private Action _trackAction_ClipEnd;

   private string _clipEventHandler_ClipStart;
   private string _clipEventHandler_ClipEnd;
   private string _trackEventHandler_ClipStart;
   private string _trackEventHandler_ClipEnd;

   public override void OnGraphStart(Playable playable)
   {
   }

   public override void OnGraphStop(Playable playable)
   {
   }

   public override void PrepareFrame(Playable playable, FrameData info)
   {
      UpdateDelegates();
      base.PrepareFrame(playable, info);
   }

   public Action ClipAction_ClipStart
   {
      get
      {
         return _clipAction_ClipStart;
      }
   }

   public Action ClipAction_ClipEnd
   {
      get
      {
         return _clipAction_ClipEnd;
      }
   }

   public Action TrackAction_ClipStart
   {
      get
      {
         return _trackAction_ClipStart;
      }
   }

   public Action TrackAction_ClipEnd
   {
      get
      {
         return _trackAction_ClipEnd;
      }
   }

   private void UpdateDelegates()
   {
      bool enableByMode = Application.isPlaying || InvokeEventsInEditMode;

      UpdateDelegate(EnableClipEvents && enableByMode, ref _clipEventHandler_ClipStart, ClipEventHandler_ClipStart, TargetObject, ref _clipAction_ClipStart);
      UpdateDelegate(EnableClipEvents && enableByMode, ref _clipEventHandler_ClipEnd, ClipEventHandler_ClipEnd, TargetObject, ref _clipAction_ClipEnd);
      UpdateDelegate(EnableTrackEvents && enableByMode, ref _trackEventHandler_ClipStart, TrackEventHandler_ClipStart, TrackTargetObject, ref _trackAction_ClipStart);
      UpdateDelegate(EnableTrackEvents && enableByMode, ref _trackEventHandler_ClipEnd, TrackEventHandler_ClipEnd, TrackTargetObject, ref _trackAction_ClipEnd);
   }

   private void UpdateDelegate(bool enable, ref string currentCallbackMethodName, string newCallbackMethodName, GameObject targetObject, ref Action targetDelegate)
   {
      if ((!enable) || string.IsNullOrEmpty(newCallbackMethodName) || (newCallbackMethodName.ToLower() == "none"))
      {
         targetDelegate = null;
         return;
      }

      if ((currentCallbackMethodName != newCallbackMethodName) && (!string.IsNullOrEmpty(newCallbackMethodName)))
      {
         currentCallbackMethodName = newCallbackMethodName;

         int splitIndex = newCallbackMethodName.LastIndexOf('.');
         string typeName = newCallbackMethodName.Substring(0, splitIndex);
         string methodName = newCallbackMethodName.Substring(splitIndex + 1, newCallbackMethodName.Length - (splitIndex + 1));

         if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(methodName))
         {
            throw new Exception("Unable to parse callback method: " + newCallbackMethodName);
         }

         MonoBehaviour targetBehaviour = null;

         foreach (var behaviour in targetObject.GetComponents<MonoBehaviour>())
         {
            if (typeName == behaviour.GetType().ToString())
            {
               targetBehaviour = behaviour;
               break;
            }
         }

         if (targetBehaviour == null)
         {
            throw new Exception("Unable to find target behaviour: " + typeName);
         }

         targetDelegate = (Action)Delegate.CreateDelegate(typeof(Action), targetBehaviour, methodName);
      }
   }
}
