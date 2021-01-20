using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreen : MonoBehaviour
{
    private int levelNumber = -1;

    public LevelCompleter levelCompleter;
    public Plane plane;
    public Tutorial tutorial;

    private void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;
        }


    }

}
