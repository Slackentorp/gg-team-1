using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using UnityEngine.UI;
using Gamelogic;
using Gamelogic.Extensions;

[XmlRoot("subsCollection")]
public class SubToolXML : Singleton<SubToolXML>
{
    [XmlArray("subsLine")]
    [XmlArrayItem("subsId")]
    List<string> subsToLoad2 = new List<string>();
    [XmlAttribute("speaker"), HideInInspector]
    public string speaker;
    [XmlAttribute("text"), HideInInspector]
    public string text;
    [XmlAttribute("startPos"), HideInInspector]
    public float startingPos;
    [XmlAttribute("duration"), HideInInspector]
    public float duration;

    public Text subtitlesToShow;
    public GameObject SubtitleContainer;
    bool showSubs = false;
    bool subtitlesIsOn = true; 

    List<string> activeSubs = new List<string>();

    private Dictionary<char, string> characterColor = new Dictionary<char, string>();

    private struct SubInfo
    {
        public char color;
        public string text;
        public float startTime;
        public float duration;
    };

    private Dictionary<int, SubInfo> subtitles;

    private const float secPerWord = 0.375f;

    public delegate void OnShow();
    public static event OnShow OnShowSubs;

    private void OnEnable()
    {
        PauseCanvas.OnSubtitleButton += ShowSubtitles; 
    }

    private void OnDisable()
    {
        PauseCanvas.OnSubtitleButton -= ShowSubtitles;
    }

    private void Awake()
    {
        characterColor.Add('D', "FFFFFF");
        characterColor.Add('M', "7A98A5");
        characterColor.Add('S', "F2CEAD");
        characterColor.Add('E', "FFFFFF");
    }

    public void InitSubs(uint markerId, string eventName)
    {
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");
        if (language == LocalizationItem.Language.ENGLISH)
        {
            XMLReader(eventName);
        }
        else
        {
            string addDK = eventName;
            addDK = addDK + "_da";
            XMLReader(addDK);
        }
    }

    void XMLReader(string eventName)
    {
        if (subtitles != null)
            subtitles.Clear();

        if (activeSubs != null)
            activeSubs.Clear();

        subtitles = new Dictionary<int, SubInfo>();

        List<char> character = new List<char>();
        List<float> startingPosition = new List<float>();
        List<string> lines = new List<string>();
        List<float> textDuration = new List<float>();

        TextAsset subText = Resources.Load<TextAsset>(eventName);

        if (subText == null)
        {
            return;
        }

        XmlTextReader reader = new XmlTextReader(new StringReader(subText.text));

        if (reader.ReadToDescendant("subsCollection"))
        {
            XmlReader subReader = reader.ReadSubtree();

            while (subReader.Read())
            {
                switch (subReader.Name)
                {
                    case "startPos":
                        subReader.Read();
                        startingPos = subReader.ReadContentAsFloat();
                        startingPosition.Add(startingPos + 15f);
                        break;
                    case "duration":
                        subReader.Read();
                        duration = subReader.ReadContentAsFloat();
                        textDuration.Add(duration);
                        break;
                    case "text":
                        subReader.Read();//we put this because otherwise it just reads the ID tag and doesnt go through there
                        text = subReader.ReadContentAsString();
                        lines.Add(text);
                        break;
                    case "speaker":
                        subReader.Read();
                        speaker = subReader.ReadContentAsString();
                        character.Add(speaker[0]);
                        break;
                }
            }

            for (int j = 0; j < lines.Count; j++)
            {
                SubInfo tmp = new SubInfo();
                int index = j;
                tmp.color = character[index];
                tmp.text = lines[j];
                tmp.duration = textDuration[j];
                tmp.startTime = startingPosition[j];
                subtitles.Add(j, tmp);
            }

            subReader.Close();
        }

        showSubs = true;

        character.Clear();
        startingPosition.Clear();
        lines.Clear();
        textDuration.Clear();

        reader.Close();
        StartCoroutine(DisplaySubtitles());
    }

    private void Update()
    {
        if (!showSubs || !subtitlesIsOn)
        {
            return;
        }

        subtitlesToShow.text = "";

        foreach (var item in activeSubs)
        {
            subtitlesToShow.text += item;

            if (activeSubs.IndexOf(item) != activeSubs.Count - 1)
            {
                subtitlesToShow.text += "\n";
            }
        }

        SubtitleContainer.SetActive(subtitlesToShow.text.Length > 1);
    }

    IEnumerator DisplaySubtitles()
    {
        SubtitleContainer.SetActive(true);

        float prev = 0;

        foreach (var item in subtitles)
        {
            yield return new WaitForSeconds((item.Value.startTime - prev) / 10);
            prev = item.Value.startTime;
            string text = AddColorToText(characterColor[item.Value.color], item.Value.text);
            activeSubs.Add(text);
            StartCoroutine(RemoveSubtitles(text, item.Value.duration));
        }
    }

    IEnumerator RemoveSubtitles(string key, float duration)
    {
        if (activeSubs.Contains(key))
        {
            yield return new WaitForSeconds(duration);
            activeSubs.Remove(key);
        }
    }

    private void ShowSubtitles(bool show)
    {
        subtitlesIsOn = show; 
    }

    private string AddColorToText(string color, string text)
    {
        return "<color=#" + color + ">" + text + "</color>";
    }
}


