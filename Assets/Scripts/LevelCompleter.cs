using UnityEngine;

public class LevelCompleter : MonoBehaviour
{
    public Animator[] doors;
    public Transform[] waypointNew;
    public GameObject LevelCompleteImage;
    
    public delegate void NewTarget(Transform t);
    public static event NewTarget NewTargetAction;

    public GameObject EndOfDemoScreen;
    private void Start()
    {
        var waypoint = FindObjectOfType<Waypoint>();
        var LevelScreen = Resources.FindObjectsOfTypeAll<LevelScreen>();
        waypoint.OnFinishLanding += LevelFinished;
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
            if (NewTargetAction != null)
                NewTargetAction(waypointNew[level]);
        }
    }

    void LevelFinished()
    {
        LevelCompleteImage.SetActive(true);
    }

}
