using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magneticfield : MonoBehaviour
{
    public GameObject magnet;
    public float force = 20f;

    // Update is called once per frame
    void Update()
    {
        /*the magnets will be attracted by the magnet board 
         * change the force value to see the effect better (200 instead of 20)
         */
        GetComponent<Rigidbody>().AddForce((magnet.transform.position - transform.position) * force * Time.smoothDeltaTime);
    }
}