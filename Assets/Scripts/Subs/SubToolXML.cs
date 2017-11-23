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
    [XmlAttribute("dk")]
    public string dk;
    [XmlAttribute("en")]
    public string en;
    public List<string> speakers = new List<string>();
    public List<float> startingPoss = new List<float>();
    public List<string> EnglishText = new List<string>();
    public List<string> DanishText = new List<string>();
    public List<string> texts = new List<string>();
    public List<float> durations = new List<float>();

    private int nextSubtitle = 0;
    int uPosition = 0;
    public Text subtitlesToShow;
    public GameObject texty;
    uint g_markersPlayingID = 1;
    bool showSubs = false;
    private LocalizationItem language;

    public delegate void OnShow();
    public static event OnShow OnShowSubs; 

    void ShowSubs()
    {
        //check for language above this on
            if (nextSubtitle < texts.Count)
            {
                AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
                uPosition = uPosition / 100;
            
                if (startingPoss[nextSubtitle] < (uPosition + Time.deltaTime) &&
                    startingPoss[nextSubtitle] > (uPosition - Time.deltaTime))
                {
                    texty.SetActive(true);
                    StartCoroutine(Wait(nextSubtitle));

                    subtitlesToShow.text = texts[nextSubtitle];
                    if(OnShowSubs != null)
                    {
                        OnShowSubs();
                    }

                switch (speakers[nextSubtitle])
                {
                    case "M":
                        //we put this because otherwise it just reas the ID tag and doesnt go through there
                        subtitlesToShow.font = Resources.Load<Font>("/Fonts/Hashtag");
                        break;

                    case "S":
                        //we put this because otherwise it just reas the ID tag and doesnt go through there
                        subtitlesToShow.font = Resources.Load<Font>("/Fonts/Journey");

                        break;
                        /*case "E":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.font = Resources.Load<Font>("/Fonts/Courier New, regular");

                            break;

                        case "D":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.font = Resources.Load<Font>("/Fonts/Courier New, regular");
                            break;*/
                    }
                    /*
                    switch (colors[nextSubtitle])
                    {
                        case "D":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.color = Color.green;

                            break;
                        case "S":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.color = Color.green;

                            break;
                        case "E":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.color = Color.green;

                            break;
                        case "M":
                            //we put this because otherwise it just reas the ID tag and doesnt go through there
                            subtitlesToShow.color = Color.green;

                            break;
                    }*/
                        nextSubtitle++;
                }
            }
            else
            {
                startingPoss.Clear();
                texts.Clear();
                durations.Clear();
                showSubs = false;
        }
    }
   
    public void InitSubs(uint markerId, string eventName)
    {
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");
        if (language == LocalizationItem.Language.ENGLISH)
        {   XMLReader(eventName);
            g_markersPlayingID = markerId;
        }
        else { 
            g_markersPlayingID = markerId;
            string addDK = eventName;
            addDK = addDK + "DK";
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

        if (reader.ReadToDescendant("subsCollection"))
        {

            XmlReader reader1 = reader.ReadSubtree();

            while (reader1.Read())
            {
                switch (reader1.Name)
                {
                    case "startPos":
                        reader1.Read();
                        startingPos = reader1.ReadContentAsFloat();
                        startingPoss.Add(startingPos);
                        break;

                    case "duration":
                        reader1.Read();
                        duration = reader1.ReadContentAsFloat();
                        durations.Add(duration);
                        break;
                        
                    case "text":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        text = reader1.ReadContentAsString();
                        texts.Add(text);
                        break;

                    case "speaker":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        speaker = reader1.ReadContentAsString();
                        speakers.Add(speaker);
                        break;
                    
                }
            }
        }
        showSubs = true;
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

}