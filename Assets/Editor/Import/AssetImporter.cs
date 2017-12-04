using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetImporter : AssetPostprocessor
{

    private void OnPreprocessModel()
    {
        var importer = assetImporter as ModelImporter;

        if (importer != null)
        {
            if (importer.isReadable == false)
            {
                importer.isReadable = false;
                importer.importMaterials = true;
                importer.animationType = ModelImporterAnimationType.None;
                importer.SaveAndReimport();
            }
        }
    }

    private void OnPostProcessModel(GameObject import)
    {
        import.isStatic = true;
        import.transform.position = Vector3.zero;
        import.transform.rotation = Quaternion.identity;
    }

    private void OnPreProcessTexture()
    {
        var importer = assetImporter as TextureImporter;

        if (importer != null)
        {
            if (importer.isReadable == false)
            {
                importer.isReadable = false;
                importer.alphaIsTransparency = true;
                importer.maxTextureSize = 512;
                importer.compressionQuality = (int) TextureCompressionQuality.Normal;
                importer.SaveAndReimport();
            }
        }
    }

}

public class CompressTextures
{
    [MenuItem("Assets/Compress Textures")]
    static void Execute()
    {
        string folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        string[] paths = { folderPath };
        var guids = AssetDatabase.FindAssets("t:texture2D", paths);
        int total = guids.Length;
        int progress = 0;

        foreach (var item in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(item);
            if (string.IsNullOrEmpty(path)) continue;

            TextureImporter importer = (TextureImporter) TextureImporter.GetAtPath(path);
            importer.isReadable = false;
            importer.alphaIsTransparency = true;
            importer.maxTextureSize = 512;
            importer.textureCompression = TextureImporterCompression.CompressedHQ;

            if (EditorUtility.DisplayCancelableProgressBar(
                    "Compressing Textures",
                    path.Replace(folderPath, ""),
                    (float)progress / (float)total))
            {
                break;
            }
            importer.SaveAndReimport();
            progress++;
        }

        EditorUtility.ClearProgressBar();
    }
}