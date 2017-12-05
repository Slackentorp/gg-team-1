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
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EventsPlayableBehaviour))]
public class TimelineEventsDrawer : PropertyDrawer
{
   private List<string> _eventHandlerListStart = new List<string> { "None" };

   public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
   {
      int fieldCount = 1;
      return fieldCount * EditorGUIUtility.singleLineHeight;
   }

   public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
   {
      SerializedProperty clipEventHandlerStartProperty = property.FindPropertyRelative("ClipEventHandler_ClipStart");
      SerializedProperty clipEventHandlerEndProperty = property.FindPropertyRelative("ClipEventHandler_ClipEnd");
      SerializedProperty trackEventHandlerStartProperty = property.FindPropertyRelative("TrackEventHandler_ClipStart");
      SerializedProperty trackEventHandlerEndProperty = property.FindPropertyRelative("TrackEventHandler_ClipEnd");
      SerializedProperty enableTrackEventsProperty = property.FindPropertyRelative("EnableTrackEvents");
      SerializedProperty enableClipEventsProperty = property.FindPropertyRelative("EnableClipEvents");
      SerializedProperty invokeEventsInEditModeProperty = property.FindPropertyRelative("InvokeEventsInEditMode");

      EventsPlayable clip = property.serializedObject.targetObject as EventsPlayable;
      GameObject gameObject = clip.TargetObject.Resolve(property.serializedObject.context as IExposedPropertyTable);

      bool hasEvents;

      EditorGUILayout.Space();

      EditorGUILayout.HelpBox("Call event handlers on the GameObject bound to the track.", MessageType.Info);

      enableTrackEventsProperty.boolValue = EditorGUILayout.BeginToggleGroup("Track Events", enableTrackEventsProperty.boolValue);

      if (enableTrackEventsProperty.boolValue)
      {
         if (clip.TrackTargetObject == null)
         {
            EditorGUILayout.HelpBox("There is currently no GameObject bound to the track. Drag a GameObject from the scene into the field to the left of the track.", MessageType.Warning);
         }

         hasEvents = AddMethodsPopup("At Clip Start", trackEventHandlerStartProperty, clip.TrackTargetObject);
         AddMethodsPopup("At Clip End", trackEventHandlerEndProperty, clip.TrackTargetObject);

         if (!hasEvents)
         {
            EditorGUILayout.HelpBox("Unable to find any event handlers. The target GameObject must have at least one MonoBehaviour with one or more public parameterless methods to serve as event handlers.", MessageType.Warning);
         }
      }

      EditorGUILayout.EndToggleGroup();

      EditorGUILayout.Space();
      EditorGUILayout.Space();

      EditorGUILayout.HelpBox("Call event handlers on the GameObject bound to this clip.", MessageType.Info);

      enableClipEventsProperty.boolValue = EditorGUILayout.BeginToggleGroup("Clip Events", enableClipEventsProperty.boolValue);

      if (enableClipEventsProperty.boolValue)
      {
         if (clip.TrackTargetObject == null)
         {
            EditorGUILayout.HelpBox("There is currently no GameObject bound to this clip. Drag a GameObject from the scene into the Target Object field.", MessageType.Warning);
         }

         hasEvents = AddMethodsPopup("At Clip Start", clipEventHandlerStartProperty, gameObject);
         AddMethodsPopup("At Clip End", clipEventHandlerEndProperty, gameObject);

         if (!hasEvents)
         {
            EditorGUILayout.HelpBox("Unable to find any event handlers. The target GameObject must have at least one MonoBehaviour with one or more public parameterless methods to serve as event handlers.", MessageType.Warning);
         }
      }

      EditorGUILayout.EndToggleGroup();

      EditorGUILayout.Space();
      EditorGUILayout.Space();

      EditorGUILayout.HelpBox("Call event handlers while in Edit Mode, by dragging the time marker in the Timeline window.", MessageType.Info);

      invokeEventsInEditModeProperty.boolValue = EditorGUILayout.BeginToggleGroup("Invoke Events in Edit Mode", invokeEventsInEditModeProperty.boolValue);

      if (invokeEventsInEditModeProperty.boolValue)
      {
         EditorGUILayout.HelpBox("PLEASE NOTE! CHANGES MADE TO THE SCENE FROM WITHIN YOUR EVENT HANDLERS WILL BE PERSISTED!", MessageType.Warning);
      }

      EditorGUILayout.EndToggleGroup();
   }

   private bool AddMethodsPopup(string label, SerializedProperty property, GameObject gameObject)
   {
      if (gameObject == null)
      {
         return false;
      }

      MonoBehaviour[] behaviours = gameObject.GetComponents<MonoBehaviour>();

      var callbackMethodsEnumarable = behaviours.SelectMany(
         x => x.GetType().GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)).Where(
         x => (x.ReturnType == typeof(void)) && (x.GetParameters().Length == 0)).Select(
         x => x.DeclaringType.ToString() + "." + x.Name);

      if (callbackMethodsEnumarable.Count() == 0)
      {
         property.stringValue = string.Empty;
         return false;
      }

      string[] callbackMethods = _eventHandlerListStart.Concat(callbackMethodsEnumarable).ToArray();

      int index = Array.IndexOf(callbackMethods, property.stringValue);

      index = EditorGUILayout.Popup(label, index, callbackMethods);

      if (index >= 0)
      {
         property.stringValue = callbackMethods[index];
      }

      return true;
   }
}
