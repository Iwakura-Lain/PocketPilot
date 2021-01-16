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

    private void Update()//todo remove
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            Destroy(transform.parent.gameObject);
        }
    }
}
