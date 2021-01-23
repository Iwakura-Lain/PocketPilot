using System;
using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public GameObject Hint;
    public GameObject PauseMenu;
    public GameObject Loading;
    public Animator[] doors;

    private void Start()
    {
        Messenger.AddListener("StopLanding", FirstPieceCollected);
        Messenger.AddListener("EnemiesAreDestroyed", OpenDoors);
        Loading = GameObject.Find("Loading");
    }
    private void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Loading)
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
