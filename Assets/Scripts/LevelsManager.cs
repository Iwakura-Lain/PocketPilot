using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public GameObject buttonTimeLvl, buttonBDLvl;
    public static bool isMainLevelCompleted = false;
    public static bool isTimeLevelCompleted = false;
    
    public GameObject levelChoice;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (isMainLevelCompleted)
                buttonTimeLvl.SetActive(true);
            if (isTimeLevelCompleted)
                buttonBDLvl.SetActive(true);
        }
    }

    public void Play()
    {
        levelChoice.SetActive(true);
    }    
    public void Rooms()
    {
        SceneManager.LoadScene("rooms");
    }    
    public void Playground()
    {
        SceneManager.LoadScene("BirthdayParty");
    }
    public void Exploration()
    {
        SceneManager.LoadScene("TimeChallenge");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}