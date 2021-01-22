using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLoading : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
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
