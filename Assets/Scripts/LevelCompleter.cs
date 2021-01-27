using System;
using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public GameObject Hint;
    public GameObject PauseMenu;
    public GameObject Loading;
    public Animator[] doors;
    public AudioSource[] audiosources;

    private void Start()
    {
        audiosources = FindObjectsOfType<AudioSource>();
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
        
        foreach (var source in audiosources)
        {
            source.enabled = Time.timeScale > 0;
        }
    }

    void FirstPieceCollected()
    {
        Hint.SetActive(true);
        Time.timeScale = 0;
    }

}
