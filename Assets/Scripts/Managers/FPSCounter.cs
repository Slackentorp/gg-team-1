using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    [SerializeField]
    private float fps;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(10, 10, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 70;
        style.normal.textColor = new Color(1f, .6f, .6f, 1.0f);
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        GUI.Label(rect, text, style);
    }
}
