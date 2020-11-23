using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreen : MonoBehaviour
{
    private int levelNumber = -1;
    public delegate void OnCompleteLevel(int level);
    public static event OnCompleteLevel OnCompleteLevelAction;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            levelNumber++;
            if (OnCompleteLevelAction != null)
                OnCompleteLevelAction(levelNumber);
            
            gameObject.SetActive(false);
        }
    }

}
