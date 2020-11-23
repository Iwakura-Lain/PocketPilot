using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public Animator[] doors;
    public GameObject LevelCompleteImage;
    
    private void Start()
    {
        Waypoint.OnFinishLanding += LevelFinished;
        LevelScreen.OnCompleteLevelAction += OpenDoor;
    }

    void OpenDoor(int level)
    {
        LevelCompleteImage.SetActive(false);
        doors[level].enabled = true;
//TODO add trigger collider to spawn enemies in next room
    }

    void LevelFinished()
    {
        LevelCompleteImage.SetActive(true);
    }

}
