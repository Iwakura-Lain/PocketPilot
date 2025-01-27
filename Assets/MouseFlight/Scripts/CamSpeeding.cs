﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSpeeding : MonoBehaviour
{
    private Camera cam;
    public int FOVzoom;
    public int FOVzoomIn;
    private int FOVnormal;
    void Start()
    {
        cam = GetComponent<Camera>();
        FOVnormal = (int)cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cam.fieldOfView = Mathf.Lerp(FOVnormal, FOVzoom, 2);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(FOVzoom, FOVnormal,2);

        }

         if (Input.GetKey(KeyCode.S))
        {
            cam.fieldOfView = Mathf.Lerp(FOVnormal, FOVzoomIn, 2);
        }
        
    }
}
