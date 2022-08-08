using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_material : MonoBehaviour
{
    public Material matStart;
    public Material Level1;
    public Material Level2;
    public Material Level3;
    public Material Level4;
    public Material Level5;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = matStart;
        count = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        //change material depending on the number of placed magnets
        count += 1;
        switch (count)
        {
            case 1:
                GetComponent<Renderer>().material = Level1;
                break;
            case 2:
                GetComponent<Renderer>().material = Level2;
                break;
            case 3:
                GetComponent<Renderer>().material = Level3;
                break;
            case 4:
                GetComponent<Renderer>().material = Level4;
                break;
            case 5:
                GetComponent<Renderer>().material = Level5;
                break;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //change material depending on the number of removed magnets
        count -= 1;
        switch (count)
        {
            case 0:
                GetComponent<Renderer>().material = matStart;
                break;
            case 1:
                GetComponent<Renderer>().material = Level1;
                break;
            case 2:
                GetComponent<Renderer>().material = Level2;
                break;
            case 3:
                GetComponent<Renderer>().material = Level3;
                break;
            case 4:
                GetComponent<Renderer>().material = Level4;
                break;
        }
    }
}
