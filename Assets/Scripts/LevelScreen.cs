using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;
            //  levelNumber++;
            //
            // // plane.Setup(levelNumber);
            //  levelCompleter.OpenDoor(levelNumber);
            //  if (tutorial)
            //  {
            //      tutorial.Door(levelNumber);
            //  }
            //  Messenger.Broadcast("OnCompleteLevelAction", levelNumber);
            //  gameObject.SetActive(false);
        }


    }

}
