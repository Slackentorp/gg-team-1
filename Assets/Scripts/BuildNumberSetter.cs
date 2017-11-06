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
            foreach (var s in buildInfo.text.Split('\n'))
            {
                string ss = s;
                if (s.Length > 20)
                {
                    ss = s.Substring(0, 7);
                }
                t.text += "\n" +ss;
            }
        }
        
    }
}