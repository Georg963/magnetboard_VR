using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choose_planet : MonoBehaviour
{
    //List to save names of objects (Planets)
    public List<string> collisions = new List<string>();

    //add name of planet to the list when colliding
    public void OnCollisionEnter(Collision collision)
    {
        collisions.Add(collision.gameObject.name);
    }

    //delete name of planet when when the object is removed
    public void OnCollisionExit(Collision collision)
    {
        collisions.Remove(collision.gameObject.name);
    }
}