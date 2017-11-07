using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable IteratorMethodResultIsIgnored

/// <summary>
/// Tracks and updates scripts that depend on localization.
/// </summary>
public class LocalizationManager
{
    public delegate void ChangeLanguage();
    public static event ChangeLanguage OnChangeLanguage;

    public LocalizationManager()
    {
        LocalizationItem.Language setting =
            (LocalizationItem.Language) PlayerPrefs.GetInt("LANGUAGE");
        Debug.Log("Setting is: " +setting);
        if (setting == LocalizationItem.Language.DANISH)
        {
            SetDanish();
        }
        else if (setting == LocalizationItem.Language.ENGLISH)
        {
            SetEnglish();
        }
    }

	public void SetDanish()
	{
		PlayerPrefs.SetInt("LANGUAGE", (int) LocalizationItem.Language.DANISH);
	    LocalizationProxy.Language = LocalizationItem.Language.DANISH;
        PlayerPrefs.Save();
	    if (OnChangeLanguage != null)
        {
	        OnChangeLanguage.Invoke();
	    }
    }

	public void SetEnglish()
	{
	    PlayerPrefs.SetInt("LANGUAGE", (int) LocalizationItem.Language.ENGLISH);
	    LocalizationProxy.Language = LocalizationItem.Language.ENGLISH;
        PlayerPrefs.Save();

	    if (OnChangeLanguage != null)
	    {
	        OnChangeLanguage.Invoke();
	    }
    }
}

public static class LocalizationProxy
{
	public static LocalizationItem.Language Language;
}