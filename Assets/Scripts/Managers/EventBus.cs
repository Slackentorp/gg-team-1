using System.Collections;
using Gamelogic.Extensions;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    [SerializeField]
    private GameObject moth;

    [SerializeField]
    private AnimationCurve lerpCurve;

    [SerializeField]
    private float timeScale = 1;

    void OnValidate()
    {
        if (timeScale < 1)
        {
            timeScale = 1;
        }
    }

    public void SetMothPosition(Vector3 position)
    {
        StartCoroutine(SmoothLerpPosition(moth, position));
    }


    IEnumerator SmoothLerpPosition(GameObject affect, Vector3 target)
    {
        Vector3 startPosition = affect.transform.position;

        float time = 0;
        while (time < lerpCurve.keys[lerpCurve.length - 1].time)
        {
            time += Time.deltaTime / timeScale;
            affect.transform.position = Vector3.Lerp(startPosition, target, lerpCurve.Evaluate(time));
            yield return null;
        }
    }
}
