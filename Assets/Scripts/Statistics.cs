using System;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour 
{
    private int currentDestroyedCount;
    private Text text;
    private int totalCount;
    public int totalRingCount;
    public int currentRingCount;

    private void Reset()
    {
        text.text = "";
        totalCount = 0;
        totalRingCount = 0;
        currentDestroyedCount = 0;
    }

    public void Init()
    {
        Messenger.AddListener("StopLanding", Reset);
        Messenger.AddListener("OnInitChaser", AddToTotalCount);
        Messenger.AddListener("OnEnterRing", UpdateRingCount);
        Messenger.AddListener("OnDestroyedChaser", UpdateStatistics);
        text = GetComponent<Text>();
    }

    private void AddToTotalCount()
    {
        totalCount++;
    }
    private void AddToTotalRingCount()
    {
        totalRingCount++;
    }
    private void UpdateRingCount()
    {
        currentRingCount++;
    }

    private void UpdateStatistics()
    {
        currentDestroyedCount++;
        var s = "destroyed:{0} of {1} enemy jets";
        if (text)
            text.text = string.Format(s, currentDestroyedCount, totalCount);
        

        if (totalCount > 0)
        {
            Waypoint.isThereEnemiesAround = true;

            if (currentDestroyedCount == totalCount)
            {
                Waypoint.isThereEnemiesAround = false;
                Messenger.Broadcast("EnemiesAreDestroyed");
            }
        }
    }

}