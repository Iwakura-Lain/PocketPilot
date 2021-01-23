using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeChallenge : MonoBehaviour
{
    public Text timeDisplay;
    public Text Score;
    public GameObject ScoreScreen;
    public Statistics stats;
    private float timeRemaining;
    public float totalTime = 60;
    public bool timerIsRunning = false;
    public AudioSource tickingSource;
    private void Start()
    {
        timeRemaining = totalTime;
        Messenger.AddListener("Death", StopTimer);
        timerIsRunning = true;
    }
    void Update()
    {
        var s = "You collected {0} of 13 rings for {2} seconds";
        if (Score)
            Score.text = string.Format(s, stats.currentRingCount, stats.totalRingCount, Mathf.Round(totalTime - timeRemaining));
        if (timerIsRunning)
        {
            tickingSource.mute = Time.timeScale > 0 ;

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                StopTimer();
            }
        }

        else
        {
            tickingSource.mute = false;
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StopTimer()
    {
        timerIsRunning = false;
        ScoreScreen.SetActive(true);
        Time.timeScale = 0;

        LevelsManager.isTimeLevelCompleted = true;
    }
}
