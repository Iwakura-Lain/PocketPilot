using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public GameObject levelChoice;
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