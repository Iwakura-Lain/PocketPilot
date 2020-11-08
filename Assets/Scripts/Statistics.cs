using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour //TODO train in interfaces and make stats an interface
{
    private float currentDestroyedCount;
    private Text text;
    private float totalCount;

    private void Start()
    {
        text = GetComponent<Text>();
        Chaser.OnDestroyed += UpdateStatistics;
        PlaneMovement.OnDestroyed += YouHaveBeenCatched;
        Chaser.OnInit += AddToTotalCount;
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
    }

    private void YouHaveBeenCatched()
    {
        if (text)
            text.text = "You were destroyed";
    }
}