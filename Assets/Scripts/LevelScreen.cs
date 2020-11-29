using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    private int levelNumber = -1;

    public LevelCompleter levelCompleter;
    public Plane plane;
    public Tutorial tutorial;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelNumber++;

           // plane.Setup(levelNumber);
            levelCompleter.OpenDoor(levelNumber);
            if (tutorial)
            {
                tutorial.Door(levelNumber);
            }
            Messenger.Broadcast("OnCompleteLevelAction", levelNumber);
            gameObject.SetActive(false);
        }


    }

}
