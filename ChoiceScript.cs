using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;

public class ChoiceScript : MonoBehaviour
{
    public GameObject Choice01;
    public GameObject Choice02;
    public GameObject Choice03;

    private List<string> questionText = new List<string>();
    private List<string> AnswerA = new List<string>();
    private List<string> AnswerB = new List<string>();
    private List<string> AnswerC = new List<string>();
    private List<string> correctAnswerID = new List<string>();
    private int ChoiceMade, index;
    private string wrong, xmlPathPattern;
    private choose_planet planet;
    private Text text;
    private TextAsset xmlRawFile;
    private XmlDocument xmlDoc;
    private XmlNodeList myNodeList;
    private Load model;

    public void Go()
    {
        ChoiceMade = 0;
        planet = GameObject.Find("DataMagnets").transform.Find("Ablageplatz").GetComponent<choose_planet>();
        model = GameObject.Find("LoadAssetBundle").GetComponent<Load>();
        GameObject.Find("QuizCanvas").transform.Find("Close").Find("Text").GetComponent<Text>().text = "";
        GameObject.Find("QuizCanvas").transform.Find("Close").GetComponent<Image>().enabled = false;
        GameObject.Find("QuizCanvas").transform.Find("Close").GetComponent<Button>().enabled = false;

        for (int i = 0; i < planet.collisions.Count; i++)
        {
            //get the right questions
            if (planet.collisions[i].Contains("Earth"))
            {
                xmlPathPattern = "//QuestionsCollection/Earth/Question";
                parseXmlFile(xmlPathPattern);
            }
            if (planet.collisions[i].Contains("Moon"))
            {
                xmlPathPattern = "//QuestionsCollection/Moon/Question";
                parseXmlFile(xmlPathPattern);
            }
            if (planet.collisions[i].Contains("Venus"))
            {
                xmlPathPattern = "//QuestionsCollection/Venus/Question";
                parseXmlFile(xmlPathPattern);
            }
        }
        startAsk();
    }

    private void startAsk()
    {
        if (ChoiceMade != 3)
        {
            //when the Answer is wrong, don't generate a new index. repeat the same Question
            if(wrong != "wrong")
            {
                generateIndex();
            }

            //enable the answers Buttons
            activateButtons();

            GameObject.Find("QuizCanvas").transform.Find("Text").GetComponent<Text>().text = "Answer the Questions";
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = questionText[index].ToString();

            GameObject.Find("QuizCanvas").transform.Find("Option01").Find("Text").GetComponent<Text>().text = AnswerA[index].ToString();
            GameObject.Find("QuizCanvas").transform.Find("Option02").Find("Text").GetComponent<Text>().text = AnswerB[index].ToString();
            GameObject.Find("QuizCanvas").transform.Find("Option03").Find("Text").GetComponent<Text>().text = AnswerC[index].ToString();

            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Image>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Image>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Repeat").Find("Text").GetComponent<Text>().text = "Repeat";
            GameObject.Find("QuizCanvas").transform.Find("Next").Find("Text").GetComponent<Text>().text = "Next";
            GameObject.Find("QuizCanvas").transform.Find("Close").Find("Text").GetComponent<Text>().text = "";
            wrong = "";
        }

        if(ChoiceMade == 3)
        {
            //disable the answers Buttons when the quiz is finished
            deactivateButtons();
            clearLists();

            GameObject.Find("QuizCanvas").transform.Find("Text").GetComponent<Text>().text = "You made it! Du hast " + ChoiceMade + " von 3 Fragen richtig beantwortet..";
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Finish";
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Image>().enabled = false;
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Image>().enabled = false;
            GameObject.Find("QuizCanvas").transform.Find("Repeat").Find("Text").GetComponent<Text>().text = "";
            GameObject.Find("QuizCanvas").transform.Find("Next").Find("Text").GetComponent<Text>().text = "";
            GameObject.Find("QuizCanvas").transform.Find("Close").Find("Text").GetComponent<Text>().text = "Schließen";
            GameObject.Find("QuizCanvas").transform.Find("Close").GetComponent<Image>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Close").GetComponent<Button>().enabled = true;
        }
    }

    public void ChoiceOption1()
    {
        deactivateButtons();

        //check if put Answer was right (in the questions File you find the ID of the correct answer)
        if (correctAnswerID[index].ToString() == "1")
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Right";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = false;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = true;
            ChoiceMade += 1;
        }
        else
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Wrong";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = false;
        }

        if (ChoiceMade == 3)
            startAsk();
    }

    public void ChoiceOption2()
    {
        deactivateButtons();

        if (correctAnswerID[index].ToString() == "2")
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Right";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = false;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = true;
            ChoiceMade += 1;
        }
        else
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Wrong";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = false;
        }

        if (ChoiceMade == 3)
            startAsk();
    }

    public void ChoiceOption3()
    {
        deactivateButtons();

        if (correctAnswerID[index].ToString() == "3")
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Right";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = false;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = true;
            ChoiceMade += 1;
        }
        else
        {
            GameObject.Find("QuizCanvas").transform.Find("Text (1)").GetComponent<Text>().text = "Wrong";
            GameObject.Find("QuizCanvas").transform.Find("Repeat").GetComponent<Button>().enabled = true;
            GameObject.Find("QuizCanvas").transform.Find("Next").GetComponent<Button>().enabled = false;
        }

        if (ChoiceMade == 3)
            startAsk();
    }

    public void repeat()
    {
        wrong = "wrong";
        startAsk();
    }

    public void Next()
    {
        activateButtons();
        startAsk();
    }

    //activate Buttons
    void activateButtons()
    {
        Choice01.SetActive(true);
        Choice02.SetActive(true);
        Choice03.SetActive(true);
    }

    //deactivate Buttons
    void deactivateButtons()
    {
        Choice01.SetActive(false);
        Choice02.SetActive(false);
        Choice03.SetActive(false);
    }

    //clear Lists
    void clearLists()
    {
        questionText.Clear();
        AnswerA.Clear();
        AnswerB.Clear();
        AnswerC.Clear();
        correctAnswerID.Clear();
    }


    //generate question's Index
    private void generateIndex()
    {
        index = Random.Range(0, questionText.Count);
    }

    //Close Button
    public void close()
    {
        GameObject.Find("QuizCanvas").GetComponent<Canvas>().enabled = false;
    }

    //parse the questions File
    private void parseXmlFile(string xmlPathPattern)
    {
        xmlRawFile = model.myLoadedQuestionsAssetBundle.LoadAsset<TextAsset>("questions");
        xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlRawFile.text));

        myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
            questionText.Add(node.Attributes[0].InnerText);
            XmlNode info1 = node.FirstChild;
            XmlNode info2 = info1.NextSibling;
            XmlNode info3 = info2.NextSibling;
            XmlNode info4 = info3.NextSibling;
            AnswerA.Add(info1.InnerText);
            AnswerB.Add(info2.InnerText);
            AnswerC.Add(info3.InnerText);
            correctAnswerID.Add(info4.InnerText);
        }
    }
}