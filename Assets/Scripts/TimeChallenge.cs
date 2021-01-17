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
    public float timeRemaining = 60;
    public bool timerIsRunning = false;
    
    private void Start()
    {
        timerIsRunning = true;
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Time.timeScale = 0;
                timeRemaining = 0;
                timerIsRunning = false;
                ScoreScreen.SetActive(true);
                var s = "You collected {0} of {1} rings for {2}";
                if (Score)
                    Score.text = string.Format(s, stats.currentRingCount, stats.totalRingCount, timeRemaining);
            }
        }
    }
    
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
