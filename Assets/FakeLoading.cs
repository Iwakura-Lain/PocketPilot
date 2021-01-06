using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoading : MonoBehaviour
{
    public Image progress;
    
    void Start()
    {
        Time.timeScale = 0;
        FillImage();
    }

    void FillImage()
    {
        progress.DOFillAmount(1, 5).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
