using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private LightSourceInput startLight;

    // Use this for initialization
    void Start()
    {
            if (startLight != null)
                EventBus.Instance.SetMothPosition(startLight.transform.TransformPoint(startLight.GetLandingPos()));

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))

    }
}
