using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    [SerializeField]
    private GameObject instantiatedObject;
    [SerializeField]
    private int instantiatedCounter = 0;
    [SerializeField]
    private int minFps = 30;
    [SerializeField]
    private float fps;
    private float offset = 0;

    void Start()
    {
        //Component[] filters = GetComponentsInChildren(typeof(MeshFilter));
        //int tris = 0;
        //int verts = 0;
        //foreach (MeshFilter f in filters)
        //{
        //    tris += f.sharedMesh.triangles.Length / 3;
        //    verts += f.sharedMesh.vertexCount;
        //}
        //Debug.Log(name + " triangles = " + tris + " vertices = " + verts);
    }

    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        //print(fps);
        if (fps > minFps)
        {
            instantiatedCounter++;
            offset += 0.1f;
            Instantiate(instantiatedObject, new Vector3(offset, 0, 0), Quaternion.identity);

            Component[] filters = GetComponentsInChildren(typeof(MeshFilter));
            int tris = 0;
            int verts = 0;
            foreach (MeshFilter f in filters)
            {
                tris += f.sharedMesh.triangles.Length / 3;
                verts += f.sharedMesh.vertexCount;
            }
        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        Rect rect2 = new Rect(0, 30, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        string text2 = string.Format("instantiatedCounter: {0:0}", instantiatedCounter);
        GUI.Label(rect, text, style);
        GUI.Label(rect2, text2, style);
    }
}

//using UnityEngine;
//using System.Collections;
 
//public class PrintPolyCount : MonoBehaviour
//{

//    // Use this for initialization
//    void Start()
//    {
//        Component[] filters = GetComponentsInChildren(typeof(MeshFilter));
//        int tris = 0;
//        int verts = 0;
//        foreach (MeshFilter f in filters)
//        {
//            tris += f.sharedMesh.triangles.Length / 3;
//            verts += f.sharedMesh.vertexCount;
//        }
//        Debug.Log(name + " triangles = " + tris + " vertices = " + verts);
//    }

//}
