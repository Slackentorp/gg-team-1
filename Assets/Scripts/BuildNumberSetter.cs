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
                int i = ss.IndexOf(':');
                ss = ss.Replace("\\n", "");
                if (ss.Length - i > 8)
                {
                    ss = ss.Substring(0, Mathf.Min(i + 8, ss.Length));
                }
                t.text += "\n" + ss;
            }
        }
        
    }
}