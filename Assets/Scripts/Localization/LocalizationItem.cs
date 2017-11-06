using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationItem : ScriptableObject {
	[Serializable]
	public enum Language {
		DANISH,
		ENGLISH
	};

	[Serializable]
	public struct LocalizationProperty {
		public Language Language;
		public string PropertyString;
	}

	[Serializable]
	public struct LocalizationString {
		public string PropertyName;
		public LocalizationProperty PropertyDanish;
		public LocalizationProperty PropertyEnglish;
	}

	public List<LocalizationString> itemList = new List<LocalizationString>();
}