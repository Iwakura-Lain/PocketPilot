using System;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour //TODO train in interfaces and make stats an interface
{
    private int currentDestroyedCount;
    private Text text;
    private int totalCount;

    public void Init()
    {
        PlaneMovement.OnDestroyed += YouHaveBeenCatched;
        Chaser.OnInit += AddToTotalCount;
        text = GetComponent<Text>();
        Chaser.OnDestroyed += UpdateStatistics;
    }

    private void AddToTotalCount()
    {
        totalCount++;
    }

    private void UpdateStatistics()
    {
        currentDestroyedCount++;
        var s = "You destroyed {0} of {1} enemy jets";
        if (text)
            text.text = string.Format(s, currentDestroyedCount, totalCount);

        if(currentDestroyedCount == totalCount)
            Waypoint.isThereEnemiesAround = false;
    }

    private void YouHaveBeenCatched()
    {
        if (text)
            text.text = "You were destroyed";
    }
}