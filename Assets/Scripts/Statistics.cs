﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour //TODO train in interfaces and make stats an interface
{
    private int currentDestroyedCount;
    private Text text;
    private int totalCount;
    public int totalRingCount;
    private int currentRingCount;

    private void Reset()
    {
        text.text = "";
        totalCount = 0;
        totalRingCount = 0;
        currentDestroyedCount = 0;
    }

    public void Init()
    {
        PlaneMovement.OnDestroyed += YouHaveBeenCatched;
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
    private void UpdateRingCount()
    {
        currentRingCount++;
        var s = "{0} of {1}";
        if (text)
            text.text = string.Format(s, currentRingCount, totalRingCount);
    }

    private void UpdateStatistics()
    {
        currentDestroyedCount++;
        var s = "You destroyed {0} of {1} enemy jets";
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

    private void YouHaveBeenCatched()
    {
        if (text)
            text.text = "You were destroyed";
    }
}