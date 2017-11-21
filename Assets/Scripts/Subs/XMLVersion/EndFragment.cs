using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using Gamelogic;
using Gamelogic.Extensions;

    public class EndFragment : Singleton<EndFragment>
    {
    public int uPosition = 0;
    public uint markerr;
    public string storyFragmentt;

        public void EndFragments(uint marker, string storyFragment)
    {
        markerr = marker;
        storyFragmentt = storyFragment;
        
    }
    public void TwoSecondsBeforeEnd()
    {
        AkSoundEngine.GetSourcePlayPosition(markerr, out uPosition);
        uPosition = uPosition / 10;
       
        LocalizationItem.Language language =
           (LocalizationItem.Language)PlayerPrefs.GetInt("LANGUAGE");

        Debug.Log(uPosition);
        
        if (language == LocalizationItem.Language.ENGLISH)
        {

                Debug.Log(realDuration);
                if (realDuration < (uPosition + Time.deltaTime) &&
                realDuration > (uPosition - Time.deltaTime))
            {
                    
                    Debug.Log("we are almost at the end");
            }
        }
        else if(language == LocalizationItem.Language.DANISH)
        {
            if (realDuration < (uPosition + Time.deltaTime) &&
               realDuration > (uPosition - Time.deltaTime))
            {
                Debug.Log("we are almost at the end");
            }
        }
            //fragmentIsOnn = false;
        

    }
    public int realDuration;
    public float durationn;
    public bool fragmentIsOnn = false;

        public void Durations(float duration, bool fragmentIsOn)
    {
        fragmentIsOnn = fragmentIsOn;
        durationn = duration / 10;
        realDuration = (int)durationn;
        realDuration = realDuration - 20;

        Debug.Log(realDuration);
    }
    private void Update()
    {
        if(fragmentIsOnn == true)
        { 
            TwoSecondsBeforeEnd();
        }
    }

}
