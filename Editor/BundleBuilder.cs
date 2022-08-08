using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BundleBuilder : Editor
{
   [MenuItem("Asset/ Build AssetBundles")]
   static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\georg\Desktop\AssetBundle", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
