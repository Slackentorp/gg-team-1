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
    [XmlAttribute("speaker")]
    public string speaker;
    [XmlAttribute("text")]
    public string text;
    [XmlAttribute("startPos")]
    public float startingPos;
    [XmlAttribute("duration")]
    public float duration;


    public List<string> speakers = new List<string>();
    public List<float> startingPoss = new List<float>();
    public List<string> texts = new List<string>();
    public List<float> durations = new List<float>();

    private int nextSubtitle = 0;
    int uPosition = 0;
    public Text subtitlesToShow;
    public GameObject texty;
    uint g_markersPlayingID = 1;
    bool showSubs = false;
    private LocalizationItem language;


    private Dictionary<char, string> characterColor = new Dictionary<char, string>();
    private Dictionary<string, float> currentSubtitles;

    private const float secPerWord = 0.375f;

    public delegate void OnShow();
    public static event OnShow OnShowSubs;

    private void Awake()
    {
        currentSubtitles = new Dictionary<string, float>();
        characterColor.Add('D', "FFFFFF");
        characterColor.Add('S', "FF0000");
        characterColor.Add('E', "00FF00");
        characterColor.Add('M', "0000FF");
    }

    private void ShowSubtitles()
    {

    }

    void ShowSubs()
    {
        //check for language above this on
        if (nextSubtitle < texts.Count)
        {
            AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
            uPosition = uPosition / 100;
            // Debug.Log(uPosition+15);
            if (startingPoss[nextSubtitle] < (uPosition + Time.deltaTime) &&
                startingPoss[nextSubtitle] > (uPosition - Time.deltaTime))
            {
                StartCoroutine(DisplaySubs(texts[nextSubtitle], durations[nextSubtitle], speakers[nextSubtitle]));

                foreach (var item in activeSubs)
                {
                    subtitlesToShow.text += item;
                }

                //subtitlesToShow.text = texts[nextSubtitle];
                //StartCoroutine(Wait(nextSubtitle));

                if (OnShowSubs != null)
                {
                    OnShowSubs();
                }
                nextSubtitle++;

            }
        }
        else
        {
            startingPoss.Clear();
            texts.Clear();
            durations.Clear();
            speakers.Clear();
            showSubs = false;
            texty.SetActive(false);
        }
    }

    public void InitSubs(uint markerId, string eventName)
    {
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");
        if (language == LocalizationItem.Language.ENGLISH)
        {
            XMLReader(eventName);
            g_markersPlayingID = markerId;
        }
        else
        {
            g_markersPlayingID = markerId;
            string addDK = eventName;
            //addDK = addDK + "DK";
            addDK = addDK + "_da";
            XMLReader(addDK);
        }
    }
    void XMLReader(string eventName)
    {
        TextAsset subText = Resources.Load<TextAsset>(eventName);
        if (subText == null)
        {
            return;
        }

        XmlTextReader reader = new XmlTextReader(new StringReader(subText.text));

        Debug.Log(reader.GetAttribute("subsCollection"));

        if (reader.ReadToDescendant("subsCollection"))
        {

            XmlReader subReader = reader.ReadSubtree();
            
            while (subReader.Read())
            {
                //print("Read " + subReader.);
                //currentSubtitles.Add(subReader.GetAttribute("text"), 0f);
                //Debug.Log(subReader.Name);

                if (subReader.Name == "startPos")
                {
                    print("Hello " + subReader.ReadContentAsFloat());
                }
                switch (subReader.Name)
                {
                    case "startPos":
                        subReader.Read();
                        startingPos = subReader.ReadContentAsFloat();
                        startingPoss.Add(startingPos + 15);
                        break;

                    case "duration":
                        subReader.Read();
                        duration = subReader.ReadContentAsFloat();
                        durations.Add(duration);
                        break;

                    case "text":
                        subReader.Read();//we put this because otherwise it just reads the ID tag and doesnt go through there
                        text = subReader.ReadContentAsString();
                        texts.Add(text);
                        break;

                    case "speaker":
                        subReader.Read();
                        speaker = subReader.ReadContentAsString();
                        speakers.Add(speaker);
                        break;
                }
            }

            subReader.Close();

        }
        showSubs = true;


        reader.Close();
    }

    private void Update()
    {
        if (!showSubs)
        {
            return;
        }

        ShowSubs();

    }
    IEnumerator Wait(int a)
    {

        yield return new WaitForSeconds(durations[a]);
        texty.SetActive(false);
    }


    [SerializeField]
    List<string> activeSubs = new List<string>();
    IEnumerator DisplaySubs(string text, float duration, string speaker)
    {
        texty.SetActive(true);
        switch (speaker)
        {
            case "D":
                activeSubs.Add("<color=#FF0000>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#FF0000>" + text + "</color>");
                break;

            case "S":
                //we put this because otherwise it just reas the ID tag and doesnt go through there

                activeSubs.Add("<color=#00FF00>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#00FF00>" + text + "</color>");
                break;

            case "E":
                activeSubs.Add("<color=#00FFFF>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#00FFFF>" + text + "</color>");
                break;

            case "M":
                activeSubs.Add("<color=#FF0000>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#FF0000>" + text + "</color>");
                break;
        }


        //gets hidden way too fast than showing the last one.
    }

    private void AddSubtitle(string text)
    {
        activeSubs.Add(text);
    }

    private void RemoveSubtitle(string text)
    {
        if (activeSubs.Contains(text))
        {
            activeSubs.Remove(text);
        }
    }

    private string AddColorToText(string color, string text)
    {
        return "<color=#" + color + ">" + text + "</color>";
    }
}