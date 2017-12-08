using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIAudioSettings : MonoBehaviour
{
    private Button muteButton; 
    private Slider levelSlider;

    private bool isMuted;
    private float level;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Sprite muted;
    [SerializeField]
    private Sprite unMuted;
    [SerializeField]
    private string audioType;


    void Start()
    {
        muteButton = GetComponentInChildren<Button>();
        levelSlider = GetComponentInChildren<Slider>(); 

        muteButton.onClick.AddListener(() => Mute(false)); 
        levelSlider.onValueChanged.AddListener(delegate { SliderChange();  });
        Mute(true); 
    }

    public void SliderChange()
    {
        AkSoundEngine.SetRTPCValue(audioType + "_VOLUME", levelSlider.value);
    }

    public void Mute(bool setup)
    {
        if(!setup)
        {
            isMuted = !isMuted; 
        }

        if (isMuted)
        {
            AkSoundEngine.PostEvent(audioType + "_MUTE", this.gameObject);
            icon.sprite = muted; 
        }
        else
        {
            AkSoundEngine.PostEvent(audioType + "_UNMUTE", this.gameObject);
            icon.sprite = unMuted; 
        }
    }
}
