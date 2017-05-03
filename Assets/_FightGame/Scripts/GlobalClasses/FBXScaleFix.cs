using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FBXScaleFix : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        ModelImporter modelImporter = (ModelImporter)assetImporter;

        if (assetPath.Contains("@"))
        {
            modelImporter.importMaterials = false;
        }
    }
}