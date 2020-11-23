using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public void MountainButton()
    {
        SceneManager.LoadScene("s2");
    }

    public void CityButton()
    {
        SceneManager.LoadScene("s1");
    }

    public void Restart()
    {
        print("click");
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