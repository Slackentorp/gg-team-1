using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class LocalizationManager : EditorWindow
{
	SerializedObject so;
	private ReorderableList list;
	Vector2 scrollPos;

	[MenuItem("Edit/Localization Settings")]
	public static void ShowWindow()
	{
		GetWindow(typeof(LocalizationManager));
	}

	LocalizationItem LocalizationItemObject;

	void OnEnable()
	{
		// Load the Localization strings from the asset file
		LocalizationItem localItem = (LocalizationItem) AssetDatabase.LoadAssetAtPath(
				"Assets/Resources/LocalizationStrings.asset", (typeof(LocalizationItem))) as
			LocalizationItem;

		// Create a new asset if it doesn't exist
		if (localItem == null)
		{
			LocalizationItemObject = CreateInstance<LocalizationItem>();
			AssetDatabase.CreateAsset(LocalizationItemObject,
				"Assets/Resources/LocalizationStrings.asset");
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
		else
		{
			LocalizationItemObject = localItem;
		}
		so = new SerializedObject(LocalizationItemObject);
		list = new ReorderableList(so,
			so.FindProperty("itemList"),
			true, true, true, true);

		list.drawElementCallback =
			(Rect rect, int index, bool isActive, bool isFocused) =>
			{
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				if (index > 0)
				{
					rect.y += 10;
				}

				rect.height = 45;
				EditorGUI.PropertyField(
					new Rect(rect.x, rect.y + 10, 100,
						EditorGUIUtility.singleLineHeight * 1.25f),
					element.FindPropertyRelative("PropertyName"), GUIContent.none);
				EditorGUI.LabelField(
					new Rect(rect.x + 100, rect.y, 70, 20),
					"English");
				EditorGUI.PropertyField(
					new Rect(rect.x + 100 + 70, rect.y, 350,
						EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative("PropertyEnglish.PropertyString"),
					GUIContent.none);
				EditorGUI.LabelField(
					new Rect(rect.x + 100, rect.y + 20, 70, 20),
					"Danish");
				EditorGUI.PropertyField(
					new Rect(rect.x + 100 + 70, rect.y + 20, 350,
						EditorGUIUtility.singleLineHeight),
					element.FindPropertyRelative("PropertyDanish.PropertyString"),
					GUIContent.none);
			};
		list.drawHeaderCallback = (Rect rect) =>
		{
			EditorGUI.LabelField(rect, "Localization Strings");
		};
		list.drawElementBackgroundCallback = (rect, index, active, focused) =>
		{
			rect.height = 45;
		};
		list.elementHeightCallback = (index) =>
		{
			Repaint();
			return 45;
		};
	}

	void OnGUI()
	{
		if (so != null)
		{
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
			so.Update();
			list.DoLayoutList();
			so.ApplyModifiedProperties();
			EditorGUILayout.EndScrollView();
		}
	}

	void OnDestroy()
	{
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}