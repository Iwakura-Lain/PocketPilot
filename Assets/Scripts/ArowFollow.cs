using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArowFollow : MonoBehaviour
{
    public  Transform ObjectToDisplay; //camera following position (Transform) of player (player linked to actual player)
    public Vector3 offset; //Vector3 stores 3 float numbers
    void Update()
    {   //position of camera equal to position of player
        transform.position = ObjectToDisplay.position + offset;
    }
}

