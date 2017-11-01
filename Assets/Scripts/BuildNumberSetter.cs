using UnityEngine;
using UnityEngine.UI;

public class BuildNumberSetter : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = "Build: " + Application.version;
    }
}