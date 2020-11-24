using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Dictionary<int, GameObject> Hints = new Dictionary<int, GameObject>();

    public GameObject waypointHint;
    public GameObject movementHint;
    public GameObject enemiesHint;
    public GameObject anyButtonHint;
    public GameObject DoorHint;

    private GameObject currentHint;
    private int currentStep = 0;
    void Start()
    {
        Hints.Add(0, movementHint);
        Hints.Add(1, waypointHint);
        Hints.Add(2, enemiesHint);
        StartCoroutine(FirstStep());
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            if(Input.anyKeyDown)
                Resume();
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;

        anyButtonHint.SetActive(true);
        if(currentHint != Hints[2])
            currentStep++;
        Time.timeScale = 0;
    }    
    
    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if(currentHint != Hints[2])
            StartCoroutine(NextStep(currentStep));

        anyButtonHint.SetActive(false);
        currentHint.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator NextStep(int num)
    {
        yield return new WaitForSeconds(3);
        if (Hints.ContainsKey(num))
        {
            Hints[num].SetActive(true);
            currentHint = Hints[num];
            Pause();
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator FirstStep()
    {
        yield return new  WaitForSeconds(1);
        movementHint.SetActive(true);
        currentHint = movementHint;
        Pause();
    }

    public void Door(int signature)
    {
        DoorHint.SetActive(true);
        currentHint = DoorHint;
        Pause();
    }
}
