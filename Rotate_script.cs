using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_script : MonoBehaviour
{
    //feel free to change the rotations speed
    [SerializeField]
    private float rotationsSpeed = 15f;

    // the planets rotate counterclockwise
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationsSpeed);
    }
}