using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public Animator[] doors;
    public Transform[] waypointNew;
    public GameObject Hint;
    public GameObject LevelCompleteImage;

    public GameObject EndOfDemoScreen;
    private void Start()
    {
        Messenger.AddListener("StopLanding", FirstPieceCollected);
        Messenger.AddListener<int>("OnCompleteLevelAction", OpenDoor);
    }

    public void OpenDoor(int level)
    {
        LevelCompleteImage.SetActive(false);
        if (doors.Length == level)
        {
            EndOfDemoScreen.SetActive(true);
        }
        else
        {
            doors[level].enabled = true;
            
            Messenger.Broadcast("NewTarget", waypointNew[level]);
        }
    }
    void FirstPieceCollected()
    {
        Hint.SetActive(true);
    }
    
    void LevelFinished()
    {
        LevelCompleteImage.SetActive(true);
    }

}
