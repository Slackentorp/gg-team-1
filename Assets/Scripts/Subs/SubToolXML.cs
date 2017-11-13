using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using UnityEngine.UI;

[XmlRoot("subsCollection")]
public class SubToolXML : MonoBehaviour {
    [XmlArray("subsLine")]
    [XmlArrayItem("subsId")]
    List<string> subsToLoad2 = new List<string>();
    [XmlAttribute("id")]
    public int subsId;
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

    public List<int> ids = new List<int>();
    public List<float> startingPoss = new List<float>();
    public List<string> EnglishText = new List<string>();
    public List<string> DanishText = new List<string>();
    public List<float> durations = new List<float>();
    
    private int nextSubtitle = 0;
    int uPosition = 0;
    public Text subtitlesToShow;
    public GameObject texty;
    uint g_markersPlayingID;

    void ShowSubs()
    {

        if (nextSubtitle < EnglishText.Count)
        {
            

            AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
            float timeInMs = Time.deltaTime * 1000;
            int realMs = (int)timeInMs;
            uPosition = uPosition / 100;

            //Debug.Log(Time.deltaTime);
            Debug.Log(uPosition);
            if (startingPoss[nextSubtitle] < (uPosition + Time.deltaTime) &&
                startingPoss[nextSubtitle] > (uPosition - Time.deltaTime))
            {
                texty.SetActive(true);
                Debug.Log(startingPoss[nextSubtitle]);
                StartCoroutine(Wait(nextSubtitle));

                subtitlesToShow.text = EnglishText[nextSubtitle];
                nextSubtitle++;
            }
        }
    }
    void XMLReader()
    {
        {
            TextAsset subText = Resources.Load<TextAsset>("XMLFile1");
            XmlTextReader reader = new XmlTextReader(new StringReader(subText.text));

            if (reader.ReadToDescendant("subsCollection"))
            {

                XmlReader reader1 = reader.ReadSubtree();

                while (reader1.Read())
                {

                    Debug.Log(reader1.Name);
                    switch (reader1.Name)
                    {
                        case "subsId":
                            reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                            subsId = reader1.ReadContentAsInt();
                            ids.Insert(0, subsId);
                            break;
                        case "duration":
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
                            break;
                            
                            /*
                            case "subsId":
                                reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                                subsId = reader1.ReadContentAsInt();
                                ids.Insert(0, subsId);
                                break;

                            case "startPos":
                                reader1.Read();
                                startingPos = reader1.ReadContentAsFloat();
                                startingPoss.Insert(0, startingPos);
                                break;

                            case "duration":
                                reader1.Read();
                                duration = reader1.ReadContentAsFloat();
                                durations.Insert(0, duration);
                                break;

                            case "text":
                                reader1.Read();
                                text = reader1.ReadContentAsString();
                                subtitleText.Insert(0, text);
                                break;
                                <subs>
        <subsId>1</subsId>
        <startPos>2</startPos>
        <duration>3</duration>
        <text>!!!!!!!!!!!</text>
      </subs>
    </subsEnglish>*/
                    }
                }

            }
        }


    }
    void Start()
    {
        XMLReader();
        g_markersPlayingID = AkSoundEngine.PostEvent("Photograph_1", gameObject, (uint)AkCallbackType.AK_EnableGetSourcePlayPosition);
        
    }
    private void Update()
    {
        
        ShowSubs();
    }
    IEnumerator Wait(int a)
    {

        yield return new WaitForSeconds(durations[a]);
        texty.SetActive(false);


    }
   
}