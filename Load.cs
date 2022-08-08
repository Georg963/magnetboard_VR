using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Load : MonoBehaviour
{
    public AssetBundle myLoadedTextAssetBundle;
    public AssetBundle myLoadedQuestionsAssetBundle;
    public AssetBundle myLoadedModelsAssetBundle;
    public AssetBundle myLoadedVideosAssetBundle;
    public AssetBundle myLoadedSkyBoxAssetBundle;
    public string path;
    public string path2;

    public void Go(string modelName)
    {
        LoadAssetBundle(path + path2);
        InstantiateObjectFromBundle(modelName);
    }

    void LoadAssetBundle(string bundleUrl)
    {
        //load the right AssetBundle
        switch (path2)
        {
            case "texts":
                myLoadedTextAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
                break;
            case "questions":
                myLoadedQuestionsAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
                break;
            case "models":
                myLoadedModelsAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
                break;
            case "videos":
                myLoadedVideosAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
                break;
            case "skybox":
                myLoadedSkyBoxAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
                break;
        }
    }

    void InstantiateObjectFromBundle(string assetName)
    {
        Object prefab = null;

        switch (path2)
        {
            case "texts":
                prefab = myLoadedTextAssetBundle.LoadAsset(assetName);
                Instantiate(prefab);
                break;
            case "questions":
                prefab = myLoadedQuestionsAssetBundle.LoadAsset(assetName);
                Instantiate(prefab);
                break;
            case "models":
                prefab = myLoadedModelsAssetBundle.LoadAsset(assetName);
                Instantiate(prefab);
                break;
            case "videos":
                prefab = myLoadedVideosAssetBundle.LoadAsset(assetName);
                Instantiate(prefab);
                break;
            case "skybox":
                prefab = myLoadedSkyBoxAssetBundle.LoadAsset(assetName);
                Instantiate(prefab);
                break;
        }
    }
}
