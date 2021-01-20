using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoading : MonoBehaviour
{
    public bool UseTimer;
    void Start()
    {
        Time.timeScale = 0;
        
        if (UseTimer)
            StartCoroutine(LoadForTime());
    }

    void OnLoaded()
    {
        Time.timeScale = 1;
        Destroy(transform.parent.gameObject);
    }

    IEnumerator LoadForTime()
    {
        yield return new WaitForSecondsRealtime(7);
        OnLoaded();
    }
}
