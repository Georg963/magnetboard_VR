using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using System.IO;
using System.Xml;
using System.Linq;

public class produce_effect : MonoBehaviour
{
    private VideoClip videoClip;
    private static List<string> collisionsList = new List<string>();
    private choose_planet planet;
    private ChoiceScript choice;
    private Load model;
    private Text text;
    private TextAsset xmlRawFile;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        model = GameObject.Find("LoadAssetBundle").GetComponent<Load>();
        planet = GameObject.Find("DataMagnets").transform.Find("Ablageplatz").GetComponent<choose_planet>();
        text = GameObject.Find("Canvas").transform.Find("ScrollArea").Find("TextContainer").Find("Text").GetComponent<Text>();
        choice = GameObject.Find("Quiz Controller").GetComponent<ChoiceScript>();
    }


    public void OnCollisionEnter(Collision collision)
    {
        collisionsList.Add(collision.gameObject.name);
        collisionsList = Enumerable.ToList(Enumerable.Distinct(collisionsList));

        //check which displayMagnet has collided with the the middle board (storage place)

        if (collisionsList.Contains("3D") && collisionsList.Contains("Ablageplatz"))
        {
            //complete the path to get the models AssetBundle
            model.path2 = "models";
            for (int i = 0; i < planet.collisions.Count; i++)
            {
                if (planet.collisions[i].Contains("Earth"))
                {
                    model.Go("Earth");
                    GameObject.Find("Models").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = "Erde";
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Moon"))
                {
                    model.Go("Moon");
                    GameObject.Find("Models").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = "Mond";
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Venus"))
                {
                    model.Go("Venus");
                    GameObject.Find("Models").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = "Venus";
                    collisionsList.Clear();
                }
            }
        }

        if (collisionsList.Contains("Video") && collisionsList.Contains("Ablageplatz"))
        {
            GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().enabled = true;
            model.path2 = "videos";
            for (int i = 0; i < planet.collisions.Count; i++)
            {
                if (planet.collisions[i].Contains("Earth"))
                {
                    model.Go("Flug über die Erde mit der ISS");
                    //assign the video
                    videoClip = model.myLoadedVideosAssetBundle.LoadAsset<VideoClip>("Flug über die Erde mit der ISS");
                    //displaying the video's name in front of the tv
                    GameObject.Find("TV").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = videoClip.name;
                    //assign the video to the Video Player Component in the plane
                    GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().clip = videoClip;
                    //enable the plane
                    GameObject.Find("TV").transform.Find("Display").GetComponent<MeshRenderer>().enabled = true;
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Moon"))
                {
                    model.Go("Flug über den Mond");
                    videoClip = model.myLoadedVideosAssetBundle.LoadAsset<VideoClip>("Flug über den Mond");
                    GameObject.Find("TV").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = videoClip.name;
                    GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().clip = videoClip;
                    GameObject.Find("TV").transform.Find("Display").GetComponent<MeshRenderer>().enabled = true;
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Venus"))
                {
                    model.Go("Venus _ National Geographic");
                    videoClip = model.myLoadedVideosAssetBundle.LoadAsset<VideoClip>("Venus _ National Geographic");
                    GameObject.Find("TV").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = videoClip.name;
                    GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().clip = videoClip;
                    GameObject.Find("TV").transform.Find("Display").GetComponent<MeshRenderer>().enabled = true;
                    collisionsList.Clear();
                }
            }
        }

        if (collisionsList.Contains("Text") && collisionsList.Contains("Ablageplatz"))
        {
            model.path2 = "texts";
            for (int i = 0; i < planet.collisions.Count; i++)
            {
                if (planet.collisions[i].Contains("Earth"))
                {
                    model.Go("texts");
                    //putting the text in textAsset to parse it
                    xmlRawFile = model.myLoadedTextAssetBundle.LoadAsset<TextAsset>("texts");
                    text.enabled = true;
                    text.text = parseXmlFile(xmlRawFile.text, "Earth");
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Moon"))
                {
                    model.Go("texts");
                    xmlRawFile = model.myLoadedTextAssetBundle.LoadAsset<TextAsset>("texts");
                    text.enabled = true;
                    text.text = parseXmlFile(xmlRawFile.text, "Mond");
                    collisionsList.Clear();
                }
                if (planet.collisions[i].Contains("Venus"))
                {
                    model.Go("texts");
                    xmlRawFile = model.myLoadedTextAssetBundle.LoadAsset<TextAsset>("texts");
                    text.enabled = true;
                    text.text = parseXmlFile(xmlRawFile.text, "Venus");
                    collisionsList.Clear();
                }
            }
        }

        if (collisionsList.Contains("Quiz") && collisionsList.Contains("Ablageplatz"))
        {
            GameObject.Find("QuizCanvas").GetComponent<Canvas>().enabled = true;
            model.path2 = "questions";
            model.Go("questions");

            //start the Go function in the ChoiceScript Script after loading the questions AssetBundle
            choice.Go();
            collisionsList.Clear();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        collisionsList = Enumerable.ToList(Enumerable.Distinct(collisionsList));

        //check which magnet has been removed
        if (collision.gameObject.name == "3D")
        {
            if (model.myLoadedModelsAssetBundle)
            {
                //unload the AssetBundle
                model.myLoadedModelsAssetBundle.Unload(true);

                for (int i = 0; i < planet.collisions.Count; i++)
                {
                    if (planet.collisions[i].Contains("Moon"))
                        Destroy(GameObject.Find("Moon(Clone)")); //destroy the cloded game Object
                    if (planet.collisions[i].Contains("Earth"))
                        Destroy(GameObject.Find("Earth(Clone)")); //destroy the cloded game Object
                    if (planet.collisions[i].Contains("Venus"))
                        Destroy(GameObject.Find("Venus(Clone)")); //destroy the cloded game Object
                }
            }
            //clear the List
            collisionsList.Clear(); 

            //clear the text Area above the Planet
            GameObject.Find("Models").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = "";
        }

        if (collision.gameObject.name == "Video")
        {
            if (model.myLoadedVideosAssetBundle)
            {
                model.myLoadedVideosAssetBundle.Unload(true);
                GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().clip = null;
            }
            collisionsList.Clear();
            GameObject.Find("TV").transform.Find("Display").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("TV").transform.Find("Display").GetComponent<VideoPlayer>().enabled = false;
            GameObject.Find("TV").transform.Find("TitleCanvas").Find("Text").GetComponent<Text>().text = "";
        }

        if (collision.gameObject.name == "Text")
        {
            if (model.myLoadedTextAssetBundle)
            {
                model.myLoadedTextAssetBundle.Unload(true);
            }
            collisionsList.Clear();
            GameObject.Find("Canvas").transform.Find("ScrollArea").Find("TextContainer").Find("Text").GetComponent<Text>().enabled = false;
        }

        if (collision.gameObject.name == "Quiz")
        {
            if (model.myLoadedQuestionsAssetBundle)
            {
                model.myLoadedQuestionsAssetBundle.Unload(true);
            }
            collisionsList.Clear();
            GameObject.Find("QuizCanvas").GetComponent<Canvas>().enabled = false;
        }
    }


    //parsing the text file
    public string parseXmlFile(string xmlData, string PlanetName)
    {
        string totVal = "";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        //check which Planet we have
        if (PlanetName == "Earth")
        {
            string xmlPathPattern = "//texts/object1";
            XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

            foreach (XmlNode node in myNodeList)
            {
                XmlNode info1 = node.FirstChild;
                XmlNode info2 = info1.NextSibling;

                totVal += info1.InnerXml + info2.InnerXml;
            }
        }

        if (PlanetName == "Mond")
        {
            string xmlPathPattern = "//texts/object2";
            XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

            foreach (XmlNode node in myNodeList)
            {
                XmlNode info1 = node.FirstChild;
                XmlNode info2 = info1.NextSibling;

                totVal += info1.InnerXml + info2.InnerXml;
            }
        }

        if (PlanetName == "Venus")
        {
            string xmlPathPattern = "//texts/object3";
            XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

            foreach (XmlNode node in myNodeList)
            {
                XmlNode info1 = node.FirstChild;
                XmlNode info2 = info1.NextSibling;

                totVal += info1.InnerXml + info2.InnerXml;
            }
        }
        return totVal;
    }
}
