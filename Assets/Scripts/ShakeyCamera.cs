using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeyCamera : MonoBehaviour
{
    private Vector3 cameraPosition;
    private Vector3 goLeft = new Vector3(-1.32f, -0.98f, -1.52f);
    private Vector3 goRight = new Vector3(2.32f, -0.98f, -1.52f);
    private float time = 3f;

    void Start()
    {
        cameraPosition = transform.position;
        StartCoroutine(ShakeyCameraCo());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ShakeyCameraCo()
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            cameraPosition = transform.position;
            transform.position = Vector3.Lerp(cameraPosition,
                  goLeft, 0.01f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        elapsedTime = 0;
        while (elapsedTime < time)
        {
            cameraPosition = transform.position;
            transform.position = Vector3.Lerp(cameraPosition,
                  goRight, 0.01f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShakeyCameraCo());
        yield return null;

    }
}
