using System;
using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public GameObject Hint;
    public GameObject PauseMenu;

    private void Start()
    {
        Messenger.AddListener("StopLanding", FirstPieceCollected);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }
    }

    void FirstPieceCollected()
    {
        Hint.SetActive(true);
        Time.timeScale = 0;
    }

}
