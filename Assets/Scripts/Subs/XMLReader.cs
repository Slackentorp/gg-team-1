using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

[XmlRoot("subsCollection")]
public class XMLReader : MonoBehaviour
{
    [XmlArray("subs")]
    [XmlArrayItem("subsId")]
    Dictionary<string, Sub> subsToLoad = new Dictionary<string, Sub>();
    List<string> subsToLoad2 = new List<string>();
    List<Sub> subsToLoad3 = new List<Sub>();
    public List<Sub> subs = new List<Sub>();
    [XmlAttribute("id")]
    public int id;
    [XmlAttribute("text")]
    public string text;

    [XmlAttribute("startPos")]
    public float startingPos;
    [XmlAttribute("duration")]
    public float duration;
   
    public List<int> ids = new List<int>();
    public List<float> startingPoss = new List<float>();
    public List<string> subtitleText = new List<string>();
    public List<float> durations = new List<float>();
    
    void YouWillGoFarMyBoy()
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
                            id = reader1.ReadContentAsInt();
                            ids.Insert(0,id);
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
                    }
                }
            
        }
            }

    void YouWillWriteXMLSubs()
    {
        {

            string filepath = Application.dataPath + @"/Data/gamexmldata.xml";
            XmlDocument xmlDoc = new XmlDocument();

            if (File.Exists(filepath))
            {
                xmlDoc.Load(filepath);

                XmlElement elmRoot = xmlDoc.DocumentElement;

               // elmRoot.RemoveAll(); // remove all inside the transforms node.

                XmlElement elmNew = xmlDoc.CreateElement("subsCollection"); // create the rotation node.

                XmlElement elmSubs = xmlDoc.CreateElement("subs"); // create the x node.
                // apply to the node text the values of the variable.

                XmlElement rotation_X = xmlDoc.CreateElement("x"); // create the x node.
                rotation_X.InnerText = x;

                XmlElement rotation_Y = xmlDoc.CreateElement("y"); // create the y node.
                rotation_Y.InnerText = y; // apply to the node text the values of the variable.

                XmlElement rotation_Z = xmlDoc.CreateElement(" "); // create the z node.
                rotation_Z.InnerText = z; // apply to the node text the values of the variable.

                elmNew.AppendChild(rotation_X); // make the rotation node the parent.
                elmNew.AppendChild(rotation_Y); // make the rotation node the parent.
                elmNew.AppendChild(rotation_Z); // make the rotation node the parent.
                elmRoot.AppendChild(elmNew); // make the transform node the parent.

                xmlDoc.Save(filepath); // save file.
            }
        }

    }

    // Use this for initialization
    void Start()
    {
        YouWillGoFarMyBoy();

    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*
public static SubsContainer Load(string path)
{
    TextAsset _xml = Resources.Load<TextAsset>(path);

    XmlSerializer serializer = new XmlSerializer(typeof(SubsContainer));
    StringReader reader = new StringReader(_xml.text);

    SubsContainer subs = serializer.Deserialize(reader) as SubsContainer;

    reader.Close();

    return subs;
}












    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

[XmlRoot("subsCollection")]
public class XMLReader : MonoBehaviour
{
    [XmlArray("subs")]
    [XmlArrayItem("subsId")]
    Dictionary<string, Sub> subsToLoad = new Dictionary<string, Sub>();
    List<string> subsToLoad2 = new List<string>();
    List<Sub> subsToLoad3 = new List<Sub>();
    public List<Sub> subs = new List<Sub>();
    [XmlAttribute("id")]
    public int id;
    [XmlAttribute("text")]
    public string text;

    [XmlAttribute("startPos")]
    public float startingPos;
    [XmlAttribute("duration")]
    public float duration;
   
    public List<int> ids = new List<int>();
    public List<float> startingPoss = new List<float>();
    public List<string> subtitleText = new List<string>();
    public List<float> durations = new List<float>();
    
    void YouWillGoFarMyBoy()
    {
        TextAsset subText = Resources.Load<TextAsset>("XMLFile1");
        XmlTextReader reader = new XmlTextReader(new StringReader(subText.text));
        int county = 0;
        if (reader.ReadToDescendant("subsCollection"))
        {
            if (reader.ReadToDescendant("subs"))//roams around until it goes to the end of the tag
            {
                XmlReader reader1 = reader.ReadSubtree();

                while (reader1.Read())
                {

                    Debug.Log(reader1.Name);
                    switch (reader1.Name)
                    {
                        case "subsId":
                            reader1.Read();//we put this because otherwise it just reas the ID tag and doesnt go through there
                            id = reader1.ReadContentAsInt();
                            ids.Insert(0,id);
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
                    }
                }
            }
        }
            }


    // Use this for initialization
    void Start()
    {
        YouWillGoFarMyBoy();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
/*
public static SubsContainer Load(string path)
{
    TextAsset _xml = Resources.Load<TextAsset>(path);

    XmlSerializer serializer = new XmlSerializer(typeof(SubsContainer));
    StringReader reader = new StringReader(_xml.text);

    SubsContainer subs = serializer.Deserialize(reader) as SubsContainer;

    reader.Close();

    return subs;
}
*/


