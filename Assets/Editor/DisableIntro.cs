using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DisableIntro
{
	private static bool isChecked = true;

	static DisableIntro()
	{
		isChecked = EditorPrefs.GetBool("ShowIntro", true);
		EditorApplication.delayCall += () =>
		{
			Menu.SetChecked("Tools/Show intro sequence", isChecked);
		};
	}

	[MenuItem("Tools/Show intro sequence", false, 0)]
	private static void DisableIntroCheck()
	{
		isChecked = !isChecked;
		EditorPrefs.SetBool("ShowIntro", isChecked);
		Menu.SetChecked("Tools/Show intro sequence", isChecked);
	}
}