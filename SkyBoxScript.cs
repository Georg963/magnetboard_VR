using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
    private Material skyOne;
    private Load model;

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.Find("LoadAssetBundle").GetComponent<Load>();
        //complete the path to get the right AssetBundle
        model.path2 = "skybox";
        model.Go("skybox");

        //load Skybox from AssetBundle
        skyOne = model.myLoadedSkyBoxAssetBundle.LoadAsset<Material>("skybox");

        //assign the loaded skybox
        RenderSettings.skybox = skyOne;
    }
}
