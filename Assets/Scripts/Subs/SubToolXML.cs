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

    public delegate void OnShow();
    public static event OnShow OnShowSubs;

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
        { XMLReader(eventName);
            g_markersPlayingID = markerId;
        }
        else {
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
                        startingPoss.Add(startingPos + 15);
                        break;

                    case "duration":
                        reader1.Read();
                        duration = reader1.ReadContentAsFloat();
                        durations.Add(duration);
                        break;

                    case "text":
                        reader1.Read();//we put this because otherwise it just reads the ID tag and doesnt go through there
                        text = reader1.ReadContentAsString();
                        texts.Add(text);
                        break;

                    case "speaker":
                        reader1.Read();
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
                //subtitlesToShow.text = null;
                break;

            case "S":
                //we put this because otherwise it just reas the ID tag and doesnt go through there

                activeSubs.Add("<color=#00FF00>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#00FF00>" + text + "</color>");
                //subtitlesToShow.text = null;
                break;

            case "E":
                activeSubs.Add("<color=#00FFFF>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#00FFFF>" + text + "</color>");
              //  subtitlesToShow.text = null;
                break;

            case "M":
                activeSubs.Add("<color=#FF0000>" + text + "</color>");
                subtitlesToShow.text = "";
                yield return new WaitForSeconds(duration);
                activeSubs.Remove("<color=#FF0000>" + text + "</color>");
                //subtitlesToShow.text = null;
                break;
        } 
        //activeSubs.Add(text);
       
        //gets hidden way too fast than showing the last one.
    }
}

/*
               switch (speakers[nextSubtitle])
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
                       subtitlesToShow.color = Color.blue;

                       break;
                   case "M":
                       //we put this because otherwise it just reas the ID tag and doesnt go through there
                       subtitlesToShow.color = Color.red;

                       break;
               }*/


/*<subsCollection>
  <subLine>
    <startPos>5</startPos>
    <duration>2</duration>
    <text>Jeg tror gerne jeg vil have en tatovering.</text>
    <speaker>S</speaker>
  </subLine>

  <subLine>
    <startPos>27</startPos>
    <duration>1</duration>
    <text>Seriøst?</text>
    <speaker>M</speaker>
  </subLine>

  <subLine>
    <startPos>40</startPos>
    <duration>3</duration>
    <text>Ja, ja seriøst. En natsværmer, sådan... Her. </text>
    <speaker>S</speaker>
  </subLine>

  <subLine>
    <startPos>77</startPos>
    <duration>5</duration>
    <text> En natsværmer?</text>
    <speaker>M</speaker>
  </subLine>

  <subLine>
    <startPos>90</startPos>
    <duration>3</duration>
    <text> Synes du hellere det skal være en sommerfugl?</text>
    <speaker>S</speaker>
  </subLine>

  <subLine> 
    <startPos>124</startPos>
    <duration>3</duration>
    <text>Nej! Nej jeg kan godt lide natsværmeren.</text>
    <speaker>M</speaker>
  </subLine>

  <subLine>
    <startPos>160</startPos>
    <duration>5</duration>
    <text>Du kan kun lide det jeg siger du kan lide.</text>
    <speaker>S</speaker>
  </subLine>

  <subLine>
    <startPos>178</startPos>
    <duration>2</duration>
    <text> Nå virkelig? Kom her!</text>
    <speaker>M</speaker>
  </subLine>

  <subLine>
    <startPos>215</startPos>
    <duration>5</duration>
    <text>(Begge griner)</text>
    <speaker>E</speaker>
  </subLine>
  
  <subLine>
    <startPos>265</startPos>
    <duration>6</duration>
    <text>(Begge griner)</text>
    <speaker>E</speaker>
  </subLine>



    <subLine>
      <startPos>56</startPos>
      <duration>5</duration>
      <text>Veto.</text>
      <speaker>M</speaker>
    </subLine>
    
    <subLine>
      <startPos>66</startPos>
      <duration>1</duration>
      <text> Hvad? Ej overvej lige hvor stort det er, du vetoer halvdelen af verden.</text>
      <speaker>S</speaker>
    </subLine>
    
    <subLine>
      <startPos>120</startPos>
      <duration>2.9</duration>
      <text>Så længe du ikke vetoer den anden halvdel, så klarer vi den jo nok.</text>
      <speaker>M</speaker>
    </subLine>
</subsCollection>*/
