using System.Collections;
using Gamelogic.Extensions;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    [SerializeField]
    private GameObject moth;

    public void SetMothPosition(Vector3 position)
    {
        moth.SendMessage("SetMothPosition", position);
    }

    public void TriggerStoryBite(BasicBiteTrigger bite)
    {
        if (bite.FixedCamera)
        {
            GameObject.FindObjectOfType<CameraController>().SendMessage("SetStoryTarget", bite.FixedCamPos);
        }
    }
}
