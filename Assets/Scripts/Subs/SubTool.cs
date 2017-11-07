using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SubTool : MonoBehaviour
{
    private List<string> subLines = new List<string>();
    private List<string> subtitleTimingStrings = new List<string>();
    public List<float> subtitleTimings = new List<float>();
    public List<string> subtitleText = new List<string>();
    public List<string> durationsString = new List<string>();
    public List<float> durations = new List<float>();
    public float duration;
    public int uPosition = 0;
    private int nextSubtitle = 0;
    uint g_markersPlayingID;
    private string displaySubtitles;
    private string[] fileLines;
    public Text subtitlesToShow;
    int timeSinceLastCall = 0;
    float tempTime = 0;
    // Use this for initialization
    void Start()
    {
        StartTalking();
        g_markersPlayingID = AkSoundEngine.PostEvent("Photograph_1", gameObject, (uint)AkCallbackType.AK_EnableGetSourcePlayPosition);
        
        //g_markersPlayingID = AkSoundEngine.PostEvent("TEST_EVENT", gameObject, (uint)AkCallbackType.AK_EnableGetSourcePlayPosition);
        
    }
    void Update()
    {
        
            

            //StartCoroutine(Waity());
            ShowSubs();
        }
    void StartTalking()
    {

        TextAsset temp = Resources.Load("hi") as TextAsset;
        fileLines = temp.text.Split('\n'); //split them in lines
        foreach (string line in fileLines)
        {
            subLines.Add(line);
        }

        //split into diffent elements
        for (int cnt = 0; cnt < subLines.Count; cnt++)
        {
            string[] splitTemp = subLines[cnt].Split('|');
            subtitleTimingStrings.Add(splitTemp[0]);//convert this into a float list
            subtitleTimings.Add(float.Parse(CleanTimeString(subtitleTimingStrings[cnt])));
            subtitleText.Add(splitTemp[1]);//this is the second part of the first line
            durationsString.Add(splitTemp[2]);
            durations.Add(float.Parse(CleanTimeString(durationsString[cnt])));

        }
        if (subtitleText[0] != null)
        {
            displaySubtitles = subtitleText[0];
        }

    }
    //remove all characters that are not timings
    private string CleanTimeString(string timeString)
    {
        Regex digitsOnly = new Regex(@"[^\d9(\\d+)+S]"); //@"[^\d9(\.\d+)+S]"
        return digitsOnly.Replace(timeString, "");
    }
    /// <summary>
    /// TOBIAS help required!!!
    void sound()
    {
        g_markersPlayingID = AkSoundEngine.PostEvent("TEST_EVENT", gameObject, (uint)AkCallbackType.AK_EnableGetSourcePlayPosition);
        int uPosition = 0;
        AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
        Debug.Log(uPosition);
    }
    public float a;
    public GameObject hui;
    // Now handle lip synchronization by using uPosition
    void ShowSubs()
    {
        
        if (nextSubtitle < subtitleText.Count)
        {
            int ag = 1;
            float haha = (float)ag;
            
            AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
            float timeInMs = Time.deltaTime * 1000;
            int realMs = (int)timeInMs;
            uPosition = uPosition / 100;

            //Debug.Log(Time.deltaTime);
            Debug.Log(uPosition);
            if (subtitleTimings[nextSubtitle] < (uPosition + Time.deltaTime) &&
                subtitleTimings[nextSubtitle] > (uPosition - Time.deltaTime))
            {
                hui.SetActive(true);
                Debug.Log(subtitleTimings[nextSubtitle]);
                StartCoroutine(Wait(nextSubtitle));

                subtitlesToShow.text = subtitleText[nextSubtitle];
                nextSubtitle++;
            }

            /* if (nextSubtitle < subtitleText.Count)
        {
            
            AkSoundEngine.GetSourcePlayPosition(g_markersPlayingID, out uPosition);
            float timeInMs = Time.deltaTime * 1000;
            int realMs = (int)timeInMs;
            uPosition = uPosition / 10;

            //Debug.Log(Time.deltaTime);
            Debug.Log(uPosition);
            if (subtitleTimings[nextSubtitle] < (uPosition + Time.deltaTime) &&
                subtitleTimings[nextSubtitle] > (uPosition - Time.deltaTime))
            {
                hui.SetActive(true);
                Debug.Log(subtitleTimings[nextSubtitle]);
                StartCoroutine(Wait(nextSubtitle));

                subtitlesToShow.text = subtitleText[nextSubtitle];
                nextSubtitle++;
            }*/

            /*
            if(audio.timeSamples/_RATE > subtitleTimings[nextSubtitle])
            {
                subtitlesToShow.text = subtitleText[nextSubtitle];
                nextSubtitle++;
            }

          if (subtitleTimings[nextSubtitle] == uPosition)
            {
                hui.SetActive(true);
                Debug.Log("hello");
                Debug.Log(subtitleTimings[nextSubtitle]);
                StartCoroutine(Wait(nextSubtitle));

                subtitlesToShow.text = subtitleText[nextSubtitle];
                nextSubtitle++;
            }


        tempTime += Time.deltaTime;
            if (tempTime > 0.1)
            {
                tempTime = 0;
               // ShowSubs();
            }
            */
        }

    }
   

    IEnumerator Wait(int a)
    {
        
        yield return new WaitForSeconds(durations[a]);
        hui.SetActive(false);


    }

    // Update is called once per frame
   
}


