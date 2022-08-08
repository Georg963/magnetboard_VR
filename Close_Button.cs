using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_Button : MonoBehaviour
{
    public void close()
    {
        GameObject.Find("DescriptionCanvas").GetComponent<Canvas>().enabled = false;
    }
}
