using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubicPiece : MonoBehaviour
{
    public Material normal;
    public Material highlighted;
    public Transform player;
    public MeshRenderer myRenderer;

    public Text holdF;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        holdF = GameObject.Find("hold f").GetComponent<Text>();
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material = normal;
    }

    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            myRenderer.material = highlighted;
            holdF.enabled = true;
        }
        else
        {
            holdF.enabled = false;
        }
    }
}
