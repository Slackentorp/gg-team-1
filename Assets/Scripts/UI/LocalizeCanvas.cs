using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Localizes canvas text to a string by type LocalizationPropertyName
/// </summary>
[RequireComponent(typeof(Text))]
[ExecuteInEditMode]
public class LocalizeCanvas : MonoBehaviour
{
	[SerializeField, Tooltip("The name of the localization string.")]
	private string LocalizationPropertyName;

    void OnEnable()
    {
        LocalizationManager.OnChangeLanguage += SetupLanguage;
    }

    void OnDisable()
    {
        LocalizationManager.OnChangeLanguage -= SetupLanguage;
    }

    /// <summary>
    /// Find property in LocalizationStrings by LocalizationPropertyName
    /// </summary>
    void SetupLanguage()
	{
		LocalizationItem LocalizationObject = Resources.Load("LocalizationStrings") as LocalizationItem;

		if (!string.IsNullOrEmpty(LocalizationPropertyName) &&
		    LocalizationObject != null)
		{
			LocalizationItem.LocalizationString item =
				LocalizationObject.itemList.Find(FindProperty);

			if (LocalizationProxy.Language == LocalizationItem.Language.DANISH)
			{
				GetComponent<Text>().text = item.PropertyDanish.PropertyString;
			}
			else if (LocalizationProxy.Language == LocalizationItem.Language.ENGLISH)
			{
				GetComponent<Text>().text = item.PropertyEnglish.PropertyString;
			}
		}
	}

	private bool FindProperty(LocalizationItem.LocalizationString ls)
	{
		if (ls.PropertyName == LocalizationPropertyName)
		{
			return true;
		}
		return false;
	}
}
