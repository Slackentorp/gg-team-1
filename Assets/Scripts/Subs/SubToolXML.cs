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
    [XmlAttribute("color")]
    public string color;
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

    public List<string> colors = new List<string>();
    public List<float> startingPoss = new List<float>();
    public List<string> EnglishText = new List<string>();
    public List<string> DanishText = new List<string>();
    public List<float> durations = new List<float>();

    private int nextSubtitle = 0;
    int uPosition = 0;
    public Text subtitlesToShow;
    public GameObject texty;
    uint g_markersPlayingID = 1;
    bool showSubs = false;

    void ShowSubs()
    {
        //check for language above this one
        if (nextSubtitle < EnglishText.Count)
        {
            AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
            float timeInMs = Time.deltaTime * 1000;
            int realMs = (int)timeInMs;
            uPosition = uPosition / 100;

            Debug.Log(uPosition);
            if (startingPoss[nextSubtitle] < (uPosition + Time.deltaTime) &&
                startingPoss[nextSubtitle] > (uPosition - Time.deltaTime))
            {
                texty.SetActive(true);
                StartCoroutine(Wait(nextSubtitle));

                subtitlesToShow.text = EnglishText[nextSubtitle];
                switch(colors[nextSubtitle])
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
                }
                nextSubtitle++;
            }
        }
        else
        {
            showSubs = false;
        }
    }

    public void InitSubs(uint markerId, string eventName)
    {
        g_markersPlayingID = markerId;
        XMLReader(eventName);

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

                    case "dk":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        dk = reader1.ReadContentAsString();
                        DanishText.Add(dk);
                        break;

                    case "en":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        en = reader1.ReadContentAsString();
                        EnglishText.Add(en);
                        break;

                    case "color":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        color = reader1.ReadContentAsString();
                        colors.Add(color);
                        break;
                        /*case "duration":
                        reader1.Read();
                        duration = reader1.ReadContentAsFloat();
                        durations.Insert(0, duration);
                        break;

                    case "startPos":
                        reader1.Read();
                        startingPos = reader1.ReadContentAsFloat();
                        startingPoss.Insert(0, startingPos);
                        break;

                    case "dk":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        dk = reader1.ReadContentAsString();
                        DanishText.Insert(0, dk);
                        break;
                    case "en":
                        reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                        en = reader1.ReadContentAsString();
                        EnglishText.Insert(0, en);
                        break;*/
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