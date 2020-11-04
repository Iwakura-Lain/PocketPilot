using UnityEngine;
using UnityEngine.UI;

public class Statistics: MonoBehaviour //TODO train in interfaces and make stats an interface
{
    Text text;
    float currentDestroyedCount = 0;
    float totalCount = 0;
    void Start()
    {
        text = GetComponent<Text>();
        Chaser.OnDestroyed += UpdateStatistics;
        PlaneMovement.OnDestroyed += YouHaveBeenCatched;
        Chaser.OnInit += AddToTotalCount;
    }

    void AddToTotalCount()
    {
        totalCount++;
    }

    void UpdateStatistics()
    {
        currentDestroyedCount++;
        string s = "You destroyed {0} of {1} enemy jets";
        if(text)
            text.text = string.Format(s, currentDestroyedCount, totalCount);
    }

    void YouHaveBeenCatched()
    {
        if (text)
            text.text = "You were destroyed";
    }
}
