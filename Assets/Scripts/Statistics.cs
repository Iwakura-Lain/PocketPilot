using System;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour //TODO train in interfaces and make stats an interface
{
    private int currentDestroyedCount;
    private Text text;
    private int totalCount;

    private void Reset()
    {
        totalCount = 0;
        currentDestroyedCount = 0;
    }

    public void Init()
    {
        var waypoint = GameObject.FindObjectOfType<Waypoint>();

        PlaneMovement.OnDestroyed += YouHaveBeenCatched;
        waypoint.OnFinishLanding += Reset;
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
        

        if (totalCount > 0)
        {
            Waypoint.isThereEnemiesAround = true;
            
            if(currentDestroyedCount == totalCount)
                Waypoint.isThereEnemiesAround = false;
        }
    }

    private void YouHaveBeenCatched()
    {
        if (text)
            text.text = "You were destroyed";
    }
}