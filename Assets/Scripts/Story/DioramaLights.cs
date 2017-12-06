using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DioramaLights : MonoBehaviour
{
    [SerializeField]
    private int LightMapIndex;

    [SerializeField]
    private int StoryEvenIndex;


    public delegate void DioramaLightsAction(bool StateCheck, bool flickCheck, int indexNr);
    public static event DioramaLightsAction DioramaLightsCall;

    private void OnEnable()
    {
        StoryEventController.StoryEventLightCall += LightMapSwitchDiorama;
    }
    private void OnDisable()
    {
        StoryEventController.StoryEventLightCall -= LightMapSwitchDiorama;
    }

    void LightMapSwitchDiorama(int dioramaIndex)
    {
        if (dioramaIndex == StoryEvenIndex)
        {
            StartCoroutine(LightSwitchDelay());
        }
    }

    private IEnumerator LightSwitchDelay()
    {
        yield return new WaitForSeconds(6f);
        AkSoundEngine.PostEvent("STORYEVENT_ON", gameObject);
        DioramaLightsCall(true, false, LightMapIndex);
        yield return null;
    }
}
