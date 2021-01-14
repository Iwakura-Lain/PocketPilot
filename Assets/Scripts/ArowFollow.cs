using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArowFollow : MonoBehaviour
{
    public Transform ObjectToDisplay;


    void Update()
    {
        //position of camera equal to position of player
        transform.position = ObjectToDisplay.position;

        transform.rotation = Quaternion.Euler(90f, ObjectToDisplay.eulerAngles.y, 0f);
    }
}
