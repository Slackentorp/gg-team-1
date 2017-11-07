using UnityEngine;
using UnityEngine.UI;

public class BuildNumberSetter : MonoBehaviour
{
    void Start()
    {
        TextAsset buildInfo = Resources.Load("buildinfo") as TextAsset;

        Text t = GetComponent<Text>();
        t.text = "Build: " + Application.version;
        if (buildInfo != null)
        {
            foreach (string s in buildInfo.text.Split('\n'))
            {
                string ss = s;
                ss = ss.Replace("\\n", "");
                if (ss.Length > 20)
                {
                    ss = ss.Substring(0, 15);
                }
                t.text += "\n" + ss;
            }
        }
        
    }
}