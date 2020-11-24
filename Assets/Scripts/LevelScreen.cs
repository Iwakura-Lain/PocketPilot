using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    private int levelNumber = -1;
    public delegate void OnCompleteLevel(int level);
    public event OnCompleteLevel OnCompleteLevelAction;

    public LevelCompleter levelCompleter;
    public Plane plane;
    public Tutorial tutorial;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelNumber++;

            plane.Setup(levelNumber);
            levelCompleter.OpenDoor(levelNumber);
            if (tutorial)
            {
                tutorial.Door(levelNumber);
            }
            
            if (OnCompleteLevelAction != null)
                OnCompleteLevelAction(levelNumber);
            
            gameObject.SetActive(false);
        }


    }

}
