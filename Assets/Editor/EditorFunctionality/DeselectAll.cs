using UnityEditor;

public static class DeselectAll
{
    [MenuItem("Tools/Deselect All &d", false, -101)]
    static void Deselect()
    {
        Selection.activeGameObject = null; 
    }
}
