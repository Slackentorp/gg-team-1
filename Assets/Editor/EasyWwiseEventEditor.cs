using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EasyWwiseEvent))]
public class EasyWwiseEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EasyWwiseEvent scriptTarget = (EasyWwiseEvent)target;

        scriptTarget.ChosenType = (EasyWwiseEvent.EasyWWiseEventType)EditorGUILayout.EnumPopup("Trigger Event In: ", scriptTarget.ChosenType);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("WWise Event ID: ", GUILayout.Width(150));
        scriptTarget.WwiseEventID = EditorGUILayout.TextField("", scriptTarget.WwiseEventID);
        EditorGUILayout.EndHorizontal();
    }
}