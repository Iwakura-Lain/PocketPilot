using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baloon : MonoBehaviour
{
    public float floatStrength = 3.5f; 
         
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * floatStrength);
    }
}
