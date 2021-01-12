﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RubicPiece : MonoBehaviour, IInteractable
{
    public Material normal;
    public Material highlighted;
    public Transform player;
    public MeshRenderer myRenderer;
    public Image progressBar;

    public Text holdF;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        holdF = GameObject.Find("hold f").GetComponent<Text>();
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material = normal;
        progressBar = GameObject.Find("progress").GetComponent<Image>();
    }

    void Update()
    {
        if ((int) Vector3.Distance(player.position, transform.position) < 5)
        {
            if (Inventory.Full)
            {
                holdF.text = "You can carry only one item at once!";
                holdF.enabled = true;
            }
            else
            {
                holdF.text = "Hold F";
                myRenderer.material = highlighted;
                holdF.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Interact();
                }
            }
        }
        else
        {
            holdF.enabled = false;
        }
        
    }

     public void Interact()
    {
        progressBar.DOFillAmount(1, 1).OnComplete(() =>
        {
            progressBar.fillAmount = 0;
            Inventory.Full = true;
            Destroy(gameObject);
        });
    }
}
